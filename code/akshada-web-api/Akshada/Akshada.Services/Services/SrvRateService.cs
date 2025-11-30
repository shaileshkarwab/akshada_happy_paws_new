using Akshada.DTO.Enums;
using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
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
    public class SrvRateService : ISrvRateService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private Int32 UserID;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SrvRateService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            
        }
        public PagedList<DTO_ServiceRateList> GetServiceRateList(DTO_FilterAndPaging filterAndPaging)
        {
            var queryResponse = this.unitOfWork.ServiceRateRepository.GetAllWithInclude(includeProperties: "ServiceSystem").ApplyAdvanceFilters(filterAndPaging);
            var pagedList = PagedList<ServiceRateMaster>.ToPagedList(queryResponse, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_ServiceRateList>>(pagedList);
            return new PagedList<DTO_ServiceRateList>(
                    response,
                    pagedList.TotalCount,
                    pagedList.CurrentPage,
                    pagedList.PageSize
                );
        }

        public DTO_ServiceRateMaster ReteriveServiceRate(string rowID)
        {
            var serviceRate = this.unitOfWork.ServiceRateRepository.ReteriveServiceRate(rowID);
            var serviceRateLocations = this.unitOfWork.SystemParamRepository
                .Find(c => c.EnumId == (Int32)SystemParameterEnum.Location).OrderBy(c => c.ParamValue).ToList();
            var response = new DTO_ServiceRateMaster
            {
                EffectiveDate =  serviceRate.EffectiveDate.ToDateTime(TimeOnly.MinValue).ToString(),
                EntryDate = serviceRate.EntryDate.ToDateTime(TimeOnly.MinValue).ToString(),
                IsActive = serviceRate.IsActive,
                RowId = serviceRate.RowId,
                ServiceSystem = this.mapper.Map<DTO_SystemParameter>(serviceRate.ServiceSystem)
            };
            var serviceRateDetails = (from a in serviceRateLocations
                                      join b in serviceRate.ServiceRateMasterDetails on a.Id equals b.LocationSystemId into bs
                                      from b in bs.DefaultIfEmpty()
                                      select new DTO_ServiceRateMasterDetail
                                      {
                                          IsActive = b == null? false: b.IsActive,
                                          RegularRate = b == null? 0: b.RegularRate,
                                          SpecialDayRate = b == null? 0 : b.SpecialDayRate,
                                          RowId = b== null? string.Empty: b.RowId,
                                          LocationSystemName = a.ParamValue,
                                          LocationSystem = new DTO_SystemParameter {
                                            ParamValue = a.ParamValue,
                                            RowId = a.RowId,
                                            EnumId = a.EnumId,
                                            Identifier1 = a.Identifier1,
                                            Identifier2 = a.Identifier2,
                                            ParamAbbrivation = a.ParamAbbrivation,
                                            Status = a.Status
                                          }
                                      }
                                      ).ToList();
            response.ServiceRateMasterDetails = serviceRateDetails;
            return response;
        }

        public bool SaveServiceRate(DTO_ServiceRateMaster saveEntity)
        {
            UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
            var dbSave = new EFCore.DbModels.ServiceRateMaster
            {
                CreatedAt = DateTime.Now,
                CreatedBy = this.UserID,
                EffectiveDate = DateOnly.FromDateTime(DateTimeHelper.GetDate( saveEntity.EffectiveDate)),
                EntryDate = DateOnly.FromDateTime(DateTimeHelper.GetDate( saveEntity.EntryDate)),
                IsActive = saveEntity.IsActive,
                RowId = System.Guid.NewGuid().ToString(),
                ServiceSystemId = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == saveEntity.ServiceSystem.RowId).Id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = this.UserID,
                ServiceRateMasterDetails = GetServiceRateDetails(saveEntity.ServiceRateMasterDetails)
            };
            this.unitOfWork.ServiceRateRepository.Add(dbSave);
            this.unitOfWork.Complete();
            return true;
        }

        List<ServiceRateMasterDetail> GetServiceRateDetails(List<DTO_ServiceRateMasterDetail> rateDetails) {
            List<ServiceRateMasterDetail> serviceRateMasterDetails = new List<ServiceRateMasterDetail>();
            foreach(var srv in rateDetails)
            {
                serviceRateMasterDetails.Add(new ServiceRateMasterDetail { 
                    IsActive = srv.IsActive.Value,
                    LocationSystemId = this.unitOfWork.SystemParamRepository.FindFirst(c=>c.RowId == srv.LocationSystem.RowId).Id,
                    RegularRate = srv.RegularRate.Value,
                    RowId = System.Guid.NewGuid().ToString(),
                    SpecialDayRate = srv.SpecialDayRate.Value
                });
            }
            return serviceRateMasterDetails;
        }

        public bool DeleteServiceRate(string rowID)
        {
            try
            {
                var dbServiceRate = this.unitOfWork.ServiceRateRepository.GetAllWithInclude(c => c.RowId == rowID, includeProperties: "ServiceRateMasterDetails");
                if (dbServiceRate == null) {
                    throw new Exception("Failed to get the details for the selected service rate");
                }
                this.unitOfWork.ServiceRateRepository.RemoveIncludingChildren(includes: "ServiceRateMasterDetails",c=>c.RowId == rowID);
                this.unitOfWork.Complete();
                return true;
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
        }

        public dynamic GetWalkingServiceRate(string serviceId, string locationId, string date)
        {
            var response = this.unitOfWork.ServiceRateRepository.GetWalkingServiceRate(serviceId, locationId, date);
            return response;
        }
    }
}
