using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class PetOtherSrvService : IPetOtherSrvService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PetOtherSrvService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> DeleteOtherPetServiceExecuted(string requestRowId)
        {
            try
            {
                var otherServiceExecuted = this.unitOfWork.OtherServiceRepository.FindFirst(c => c.RowId == requestRowId);
                if(otherServiceExecuted == null)
                {
                    throw new Exception("Failed to get the details for the selected other service");
                }
                this.unitOfWork.OtherServiceRepository.RemoveIncludingChildren(includes: "OtherServicesOfferedImages", c => c.RowId == requestRowId);
                this.unitOfWork.Complete();
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException { 
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public async Task<PagedList<DailyOtherServiceList>> GetOtherPetServices(DTO_FilterAndPaging filterAndPaging)
        {
            var response = await this.unitOfWork.OtherServiceRepository.GetOtherPetServices(filterAndPaging);
            var filteredResponse = response.ApplyAdvanceFilters(filterAndPaging);
            var pagedList = PagedList<DailyOtherServiceList>.ToPagedList(filteredResponse, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            return pagedList;
        }

        public async Task<OtherServiceExecutionDetail> ReteriveOtherPetServices(string otherServiceRequestId, string otherServiceAssignedToRowId)
        {
            var response = await this.unitOfWork.OtherServiceRepository.ReteriveOtherPetServices(otherServiceRequestId, otherServiceAssignedToRowId);
            return response;
        }

        public Task<bool> SaveOtherPetServiceExecuted(string otherServiceRequestId, DTO_OtherServicesOfferedNew saveEntity)
        {
            try
            {
                return Task.FromResult(true);
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

        public Task<bool> UpdateOtherPetServiceExecuted(string otherServiceRequestId, string serviceOfferedRowId, DTO_OtherServicesOfferedNew saveEntity)
        {
            try
            {
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                var otherServiceRequestDB = this.unitOfWork.OtherServiceRequestRepository.FindFirst(c => c.RowId == otherServiceRequestId);
                if (otherServiceRequestDB == null)
                {
                    throw new Exception("Failed to get the details for the other service request");
                }

                var otherServiceOfferedDB = this.unitOfWork.OtherServiceRepository.FindFirst(c => c.RowId == serviceOfferedRowId);
                if (otherServiceOfferedDB == null)
                {
                    throw new Exception("Failed to get the details for the other service offered for the request");
                }

                var userDB = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == saveEntity.ServiceOfferedUser.RowId);
                if (userDB == null)
                {
                    throw new Exception("Failed to get the details for the selected user");
                }
                otherServiceOfferedDB.UpdatedAt = System.DateTime.Now;
                otherServiceOfferedDB.UpdatedBy = userID;
                otherServiceOfferedDB.ServiceOfferedUserId = userDB.Id;
                otherServiceOfferedDB.OtherServiceRequestMasterId = otherServiceRequestDB.Id;
                otherServiceOfferedDB.ServiceOfferedDate = DateTimeHelper.GetDate(saveEntity.ServiceOfferedDate);
                otherServiceOfferedDB.FromTime = DateTimeHelper.ConverDateTimeToTime(saveEntity.FromTime);
                otherServiceOfferedDB.ToTime = DateTimeHelper.ConverDateTimeToTime(saveEntity.ToTime);
                otherServiceOfferedDB.Remarks = saveEntity.Remarks;

                foreach (var img in saveEntity.OtherServicesOfferedImages.Where(m => !string.IsNullOrEmpty(m.UploadImageName)).ToList())
                {
                    var sysParamType = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == img.ImageTypeSystem.RowId);
                    if (sysParamType == null)
                    {
                        throw new Exception("Failed to get the details for the image type");
                    }
                    if (string.IsNullOrEmpty(img.RowId))
                    {
                        otherServiceOfferedDB.OtherServicesOfferedImages.Add(new OtherServicesOfferedImage
                        {
                            ImageName = img.ImageName,
                            UploadImageName = img.UploadImageName,
                            RowId = System.Guid.NewGuid().ToString(),
                            ImageTypeSystemId = sysParamType.Id
                        });
                    }
                    else
                    {
                        var imageDB = this.unitOfWork.OtherServiceImageRepo.FindFirst(c => c.RowId == img.RowId);
                        if(imageDB == null)
                        {
                            throw new Exception("Failed to get the details for the image to be updated");
                        }
                        imageDB.ImageTypeSystemId = sysParamType.Id;
                        imageDB.ImageName = img.ImageName;
                        imageDB.UploadImageName = img.UploadImageName;
                        this.unitOfWork.OtherServiceImageRepo.Update(imageDB);
                    }
                }
                this.unitOfWork.OtherServiceRepository.Update(otherServiceOfferedDB);
                this.unitOfWork.Complete();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            };
        }
    }
}
