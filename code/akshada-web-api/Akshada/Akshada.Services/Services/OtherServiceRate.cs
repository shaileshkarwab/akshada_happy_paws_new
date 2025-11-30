using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class OtherServiceRate : IOtherServiceRate
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public OtherServiceRate(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public bool DeleteByIdOtherServiceRate(string otherServiceRateRowId)
        {
            try
            {
                var dbRates = this.unitOfWork.OtherServiceRateRepository.FindFirst(c=>c.RowId == otherServiceRateRowId,includeProperties: "OtherServiceRateDetails");
                if(dbRates == null)
                {
                    throw new Exception("Failed to get the details for the selected service rates");
                }
                this.unitOfWork.OtherServiceRateRepository.RemoveIncludingChildren(includes: "OtherServiceRateDetails", c => c.RowId == otherServiceRateRowId);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
        }

        public PagedList<DTO_OtherServiceRateList> GetAllOtherServiceDate(DTO_FilterAndPaging filterAndPaging)
        {
            var otherServiceList = this.unitOfWork.OtherServiceRateRepository.GetAll().OrderByDescending(c => c.EffectiveDate);
            var pagedList = PagedList<Akshada.EFCore.DbModels.OtherServiceRate>.ToPagedList(otherServiceList, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_OtherServiceRateList>>(pagedList);
            return new PagedList<DTO_OtherServiceRateList>(
                response,
                pagedList.TotalCount,
                pagedList.CurrentPage,
                pagedList.PageSize
            );
        }

        public DTO_OtherServiceRate GetByIdOtherServiceRate(string otherServiceRateRowId)
        {
            try
            {
                var dbOtherServiceRate = this.unitOfWork.OtherServiceRateRepository.GetAllWithInclude(x => x.RowId == otherServiceRateRowId, includeProperties: "OtherServiceRateDetails,OtherServiceRateDetails.ServiceSystem").FirstOrDefault();
                var response = this.mapper.Map<DTO_OtherServiceRate>(dbOtherServiceRate);
                return response;
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

        public bool SaveOtherServiceRate(DTO_OtherServiceRate otherServiceRate)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbOtherServiceRate = new Akshada.EFCore.DbModels.OtherServiceRate
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    EntryDate = DateTimeHelper.GetDate(otherServiceRate.EntryDate),
                    EffectiveDate = DateTimeHelper.GetDate(otherServiceRate.EffectiveDate),
                    IsActive = true,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = userID,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now
                };
                foreach (var rate in otherServiceRate.OtherServiceRateDetails)
                {
                    var serviceSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == rate.ServiceSystem.RowId);
                    if (serviceSystem == null)
                    {
                        throw new Exception("Failed to get the details for the selected service");
                    }
                    dbOtherServiceRate.OtherServiceRateDetails.Add(new EFCore.DbModels.OtherServiceRateDetail
                    {
                        RowId = System.Guid.NewGuid().ToString(),
                        ChargeableAmount = rate.ChargeableAmount,
                        ServiceSystemId = serviceSystem.Id
                    });
                }

                this.unitOfWork.OtherServiceRateRepository.Add(dbOtherServiceRate);
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

        public bool UpdateOtherServiceRate(string otherServiceRowId, DTO_OtherServiceRate updateEntity)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbOtherServiceRate = this.unitOfWork.OtherServiceRateRepository.FindFirst(c=>c.RowId == otherServiceRowId);
                if(dbOtherServiceRate == null)
                {
                    throw new Exception("Failed to get the other service rate details");
                }
                dbOtherServiceRate.EffectiveDate = DateTimeHelper.GetDate(updateEntity.EffectiveDate);
                dbOtherServiceRate.EntryDate = DateTimeHelper.GetDate( updateEntity.EntryDate);
                dbOtherServiceRate.IsActive = true;
                dbOtherServiceRate.UpdatedAt = System.DateTime.Now;
                dbOtherServiceRate.UpdatedBy = userID;
                
                foreach(var m in updateEntity.OtherServiceRateDetails)
                {
                    var dbServiceSystem = this.unitOfWork.SystemParamRepository.FindFirst(c=>c.RowId == m.ServiceSystem.RowId);
                    if(dbServiceSystem == null)
                    {
                        throw new Exception("Failed to get the details for the service system");
                    }
                    if (string.IsNullOrEmpty(m.RowId))
                    {
                        dbOtherServiceRate.OtherServiceRateDetails.Add(new EFCore.DbModels.OtherServiceRateDetail { 
                            ChargeableAmount = m.ChargeableAmount,
                            ServiceSystemId = dbServiceSystem.Id
                        });
                    }
                    else {
                        var otherServiceRate = this.unitOfWork.OtherServiceRateDetailRepository.FindFirst(c=>c.RowId == m.RowId);
                        if (dbServiceSystem == null)
                        {
                            throw new Exception("Failed to get the rate details");
                        }
                        otherServiceRate.ServiceSystemId = dbServiceSystem.Id;
                        otherServiceRate.ChargeableAmount = m.ChargeableAmount;
                        this.unitOfWork.OtherServiceRateDetailRepository.Update(otherServiceRate);
                    }
                }
                this.unitOfWork.Complete();
                return true;
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException { 
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
