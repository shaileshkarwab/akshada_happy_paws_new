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
using static Google.Apis.Requests.BatchRequest;

namespace Akshada.Services.Services
{
    public class WalkingRecordService : IWalkingRecordService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly GoogleMapService _mapService;
        public WalkingRecordService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, GoogleMapService _mapService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this._mapService = _mapService;
        }

        public bool DeleteWalkingServiceRecord(string rowId)
        {
            try
            {
                var dbWalkingServiceRecord = this.unitOfWork.WalkingServiceRecordRepo.GetAllWithInclude(c=>c.RowId == rowId,includeProperties: "WalkingServiceRecordImages");
                if(dbWalkingServiceRecord == null)
                {
                    throw new Exception("Failed to get the details for the walking service record");
                }
                this.unitOfWork.WalkingServiceRecordRepo.RemoveIncludingChildren(includes: "WalkingServiceRecordImages",c=>c.RowId == rowId);
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex,

                };
            }
        }

        public PagedList<DTO_WalkingRecordList> GetAllRecords(DTO_FilterAndPaging filterAndPaging)
        {
            var walkingRecordListQuery = this.unitOfWork.WalkingServiceRecordRepo.GetAllWithInclude(filter: null, null, "Customer,Pet,ServiceOfferedByUser,Pet.BreedSystem,Pet.Colour,WalkingServiceMaster.ServiceSystem").ApplyAdvanceFilters(filterAndPaging);
            var walkingRecordList = PagedList<WalkingServiceRecord>.ToPagedList(walkingRecordListQuery, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_WalkingRecordList>>(walkingRecordList);
            return new PagedList<DTO_WalkingRecordList>(response,
                walkingRecordList.TotalCount,
                walkingRecordList.CurrentPage,
                walkingRecordList.PageSize);
        }

        public DTO_WalkingRecord GetById(string rowId)
        {
            var walkingRecord = this.unitOfWork.WalkingServiceRecordRepo.FindFirst(c => c.RowId == rowId, includeProperties: "Customer,Pet,Customer.CustomerPets, WalkingServiceMaster, WalkingServiceRecordImages, WalkingServiceRecordImages.ImageUploadSystem, ServiceOfferedByUser");
            var response = this.mapper.Map<DTO_WalkingRecord>(walkingRecord);

            response.WalkingServiceRequestServices = GetTheServiceRequestDays(
                new DTO_SearchParam
                {
                    customerRowId = response.Customer.RowId,
                    petRowId = response.Pet.RowId,
                    WalkingServiceMasterId = walkingRecord.WalkingServiceMasterId,
                    WalkingServiceDayMasterId = walkingRecord.WalkingServiceDayMasterId,
                    WalkingServiceDayScheduleMasterId = walkingRecord.WalkingServiceDayScheduleMasterId,
                    ServiceOfferedDate = Convert.ToDateTime(response.ServiceOfferedDate)
                }
            );
            return response;
        }

        public bool SaveWalkingServiceRecord(string customerRowId, string petRowId, DTO_WalkingServiceRecord walkingRecord)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the customer");
                }

                var dbPet = this.unitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == petRowId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the details for the pet");
                }

                var dbWalkingRequestMst = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == walkingRecord.WalkingServiceMasterId);
                if (dbWalkingRequestMst == null)
                {
                    throw new Exception("Failed to get the details for Walking Service Request Master");
                }

                var dbWalkingRequestMstDay = this.unitOfWork.WalkingServiceRequestDaysRepo.FindFirst(c => c.RowId == walkingRecord.WalkingServiceDayMasterId);
                if (dbWalkingRequestMstDay == null)
                {
                    throw new Exception("Failed to get the details for Walking Service Request Day detail");
                }

                var dbWalkingRequestMstDaySch = this.unitOfWork.WalkingServiceRequestDayScheduleRepo.FindFirst(c => c.RowId == walkingRecord.WalkingServiceDayScheduleMasterId);
                if (dbWalkingRequestMstDaySch == null)
                {
                    throw new Exception("Failed to get the details for Walking Service Request Day detail");
                }

                var dbUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == walkingRecord.ServiceOfferedByUser.RowId);
                if (dbUser == null)
                {
                    throw new Exception("Failed to get the details for Walking Service offered by user");
                }

                var dbWalkingRecord = new WalkingServiceRecord
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    ServiceOfferedDate = DateTimeHelper.GetDate(walkingRecord.ServiceOfferedDate),
                    FromTime = DateTimeHelper.ConverDateTimeToTime(walkingRecord.FromTime),
                    ToTime = DateTimeHelper.ConverDateTimeToTime(walkingRecord.ToTime),
                    CustomerId = dbCustomer.Id,
                    PetId = dbPet.Id,
                    WalkingServiceMasterId = dbWalkingRequestMst.Id,
                    WalkingServiceDayMasterId = dbWalkingRequestMstDay.Id,
                    WalkingServiceDayScheduleMasterId = dbWalkingRequestMstDaySch.Id,
                    Remarks = walkingRecord.Remarks,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    ServiceOfferedByUserId = dbUser.Id,
                };
                //adding walking images
                foreach (var img in walkingRecord.WalkingServiceRecordImages)
                {
                    if (!string.IsNullOrEmpty(img.ImageName))
                    {
                        var dbImgeUploadType = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == img.UploadImageSystemParam.RowId);
                        if (dbImgeUploadType == null)
                        {
                            throw new Exception("Failed to get the details for the upload image type");
                        }
                        dbWalkingRecord.WalkingServiceRecordImages.Add(new WalkingServiceRecordImage
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            ImageName = img.ImageName,
                            ImageUploadSystemId = dbImgeUploadType.Id,
                            Lattitude = img.Lattitude.HasValue? img.Lattitude.Value : 0,
                            Longitude = img.Longitude.HasValue? img.Longitude.Value : 0,
                            Address = this._mapService.GetAddressFromLatLng(img.Lattitude, img.Longitude).Result,
                            RecordTime = string.IsNullOrEmpty( img.RecordTime) ? System.DateTime.Now : DateTimeHelper.ConvertTimeStringToDate( img.RecordTime)
                        });
                    }
                }
                this.unitOfWork.WalkingServiceRecordRepo.Add(dbWalkingRecord);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex

                };
            }
        }

        List<DTO_WalkingServiceRequestServices> GetTheServiceRequestDays(DTO_SearchParam searchParam)
        {

            //get the details for the service request
            var walkingServiceRequests = this.unitOfWork.WalkingServiceRequestRepo.GetAllWithInclude(
                c => c.Customer.RowId == searchParam.customerRowId
                && c.Pet.RowId == searchParam.petRowId
                , includeProperties: "WalkingServiceRequestDays, WalkingServiceRequestDays.WalkingServiceRequestDaySchedules"
                );

            var serviceOfferedDays = new List<DTO_WalkingServiceRequestServices>();
            foreach (var request in walkingServiceRequests.Where(d => searchParam.ServiceOfferedDate >= d.FromDate && searchParam.ServiceOfferedDate <= d.ToDate))
            {
                foreach (var day in request.WalkingServiceRequestDays.Where(d => d.IsSelected == true && d.WeekDayName == searchParam.ServiceOfferedDate.DayOfWeek.ToString()))
                {
                    foreach (var sch in day.WalkingServiceRequestDaySchedules)
                    {
                        serviceOfferedDays.Add(new DTO_WalkingServiceRequestServices
                        {
                            DayName = day.WeekDayName,
                            FromTime = sch.FromTime.ToString(),
                            Selected = searchParam.WalkingServiceMasterId == request.Id && searchParam.WalkingServiceDayMasterId == day.Id && searchParam.WalkingServiceDayScheduleMasterId == sch.Id ? true : false,
                            ToTime = sch.ToTime.ToString(),
                            WalkingServiceDayMasterId = day.RowId,
                            WalkingServiceMasterId = request.RowId,
                            WalkingServiceDayScheduleMasterId = sch.RowId,
                        });
                    }
                }
            }

            return serviceOfferedDays;
        }
    }
}
