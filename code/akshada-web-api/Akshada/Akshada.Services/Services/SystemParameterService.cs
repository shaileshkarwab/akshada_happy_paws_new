using Akshada.DTO.Enums;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class SystemParameterService : ISystemParameterService
    {
        public readonly IUnitOfWork unitOfWork;
        public readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SystemParameterService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public PagedList<DTO_SystemParameter> GetSystemParameter(DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.unitOfWork.SystemParamRepository.GetAll().ApplyAdvanceFilters(filterAndPaging);
            var pagedResponse = PagedList<SystemParameter>.ToPagedList(response, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var systemParameters = this.mapper.Map<List<DTO_SystemParameter>>(pagedResponse);
            var systemEnums = EnumHelper.EnumToJson<SystemParameterEnum>();
            foreach (var param in systemParameters)
            {
                param.EnumDesc = systemEnums.EnumNamesValues.FirstOrDefault(c => c.Id == param.EnumId).Description;
            }

            var dtoPagedList = new PagedList<DTO_SystemParameter>(
            systemParameters,
            pagedResponse.TotalCount,
            pagedResponse.CurrentPage,
            pagedResponse.PageSize
            );

            return dtoPagedList;
        }

        public DTO_SystemParameter GetSystemParameterById(string sysParamRowId)
        {
            var response = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == sysParamRowId);
            var systemParameter = this.mapper.Map<DTO_SystemParameter>(response);
            return systemParameter;
        }

        public List<DTO_SystemParameter> GetSystemParameterDataDetailsByEnumId(int enumID)
        {
            var systemParamList = this.unitOfWork.SystemParamRepository.Find(c => c.EnumId == enumID).OrderBy(c => c.ParamValue);
            var response = this.mapper.Map<List<DTO_SystemParameter>>(systemParamList);
            return response;
        }

        public List<DTO_LookUp> GetSystemParameterDetailsByEnumId(int enumID)
        {
            var systemParamList = this.unitOfWork.SystemParamRepository.Find(c => c.EnumId == enumID).OrderBy(c => c.ParamValue);
            var response = this.mapper.Map<List<DTO_LookUp>>(systemParamList);
            return response;
        }

        public List<DTO_LookUp> GetSystemParameterDetailsByEnumIds(List<int> enumID)
        {
            var systemParamList = this.unitOfWork.SystemParamRepository.Find(c => enumID.Contains(c.EnumId)).OrderBy(c => c.ParamValue);
            var response = this.mapper.Map<List<DTO_LookUp>>(systemParamList);
            return response;
        }

        bool ValidateData(DTO_SystemParameter systemParameter)
        {
            if (string.IsNullOrEmpty(systemParameter.RowId))
            {
                var checkExisting = this.unitOfWork.SystemParamRepository.Any(c => c.ParamValue == systemParameter.ParamValue);
                if (checkExisting)
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(systemParameter.RowId))
            {
                var checkExisting = this.unitOfWork.SystemParamRepository.Any(c => c.ParamValue == systemParameter.ParamValue && c.RowId != systemParameter.RowId);
                if (checkExisting)
                {
                    return false;
                }
            }
            return true;
        }


        public bool SaveSystemParameter(DTO_SystemParameter systemParameter)
        {
            try
            {
                if (!ValidateData(systemParameter))
                {
                    throw new Exception("The entered parameter value is already existing");
                }
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                if (string.IsNullOrEmpty(systemParameter.RowId))
                {
                    var dbSystemParameter = this.mapper.Map<SystemParameter>(systemParameter);
                    dbSystemParameter.UpdatedBy = userID;
                    dbSystemParameter.UpdatedAt = System.DateTime.UtcNow;
                    dbSystemParameter.CreatedBy = userID;
                    dbSystemParameter.CreatedAt = System.DateTime.UtcNow;
                    dbSystemParameter.RowId = System.Guid.NewGuid().ToString();
                    this.unitOfWork.SystemParamRepository.Add(dbSystemParameter);
                }
                else
                {
                    var dbSystemParam = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == systemParameter.RowId);
                    if (dbSystemParam == null)
                    {
                        throw new Exception("Failed to get the details for the system parameter");
                    }
                    dbSystemParam.UpdatedAt = System.DateTime.Now;
                    dbSystemParam.UpdatedBy = userID;
                    dbSystemParam.ParamValue = systemParameter.ParamValue;
                    dbSystemParam.ParamAbbrivation = systemParameter.ParamAbbrivation;
                    dbSystemParam.Identifier1 = systemParameter.Identifier1;
                    dbSystemParam.Identifier2 = systemParameter.Identifier2;
                    dbSystemParam.Status = systemParameter.Status;
                    dbSystemParam.EnumId = systemParameter.EnumId.Value;
                    this.unitOfWork.SystemParamRepository.Update(dbSystemParam);
                }
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public bool DeleteSystemParameter(string sysParameterRowId)
        {
            try
            {
                var dbSystemParameter = this.unitOfWork.SystemParamRepository.FindFirst(c=>c.RowId == sysParameterRowId);
                if(dbSystemParameter == null)
                {
                    throw new Exception("Failed to get the details of system parameter for delete");
                }
                this.unitOfWork.SystemParamRepository.Remove(dbSystemParameter);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    SystemException = ex,
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
