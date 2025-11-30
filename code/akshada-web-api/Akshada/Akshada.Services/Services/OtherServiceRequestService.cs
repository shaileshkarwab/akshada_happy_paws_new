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
    public class OtherServiceRequestService : IOtherServiceRequestService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OtherServiceRequestService(IUnitOfWork UnitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.UnitOfWork = UnitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public PagedList<DTO_OtherServiceRequest> GetAll(DTO_FilterAndPaging filterAndPaging)
        {
            var otherServiceRequestQuery = this.UnitOfWork.OtherServiceRequestRepository.GetAllWithInclude(includeProperties: "AddressLocationSystem,Customer,Pet,RequiredServiceSystem,AssignOtherServiceRequestUser,AssignOtherServiceRequestUser.AssignedToUser").ApplyAdvanceFilters(filterAndPaging);
            var pagedResponse = PagedList<OtherServiceRequest>.ToPagedList(otherServiceRequestQuery, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var list = this.mapper.Map<List<DTO_OtherServiceRequest>>(pagedResponse);
            return new PagedList<DTO_OtherServiceRequest>(
                    list,
                    pagedResponse.TotalCount,
                    pagedResponse.CurrentPage,
                    pagedResponse.PageSize
                );
        }

        public bool AddOtherServiceRequest(DTO_OtherServiceRequest otherServiceRequest)
        {
            try
            {
                var customerID = string.IsNullOrEmpty(otherServiceRequest.Customer.RowId) ? null : this.UnitOfWork.CustomerRepository.FindFirst(c => c.RowId == otherServiceRequest.Customer.RowId)?.Id;
                var petID = string.IsNullOrEmpty(otherServiceRequest.Pet.RowId) ? null : this.UnitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == otherServiceRequest.Pet.RowId)?.Id;
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var requiredServiceSystemId = this.UnitOfWork.SystemParamRepository.FindFirst(c => c.RowId == otherServiceRequest.RequiredServiceSystem.RowId).Id;
                var addressLocationID = string.IsNullOrEmpty(otherServiceRequest.AddressLocationSystem.RowId) ? null : this.UnitOfWork.SystemParamRepository.FindFirst(c => c.RowId == otherServiceRequest.AddressLocationSystem.RowId)?.Id;
                var petColourBreedId = string.IsNullOrEmpty(otherServiceRequest.AddressLocationSystem.RowId) ? null : this.UnitOfWork.SystemParamRepository.FindFirst(c => c.RowId == otherServiceRequest.PetColourBreed.RowId)?.Id;
                var petColourSystemId = string.IsNullOrEmpty(otherServiceRequest.AddressLocationSystem.RowId) ? null : this.UnitOfWork.SystemParamRepository.FindFirst(c => c.RowId == otherServiceRequest.PetColourSystem.RowId)?.Id;
                var dbOtherServiceRequest = new OtherServiceRequest
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    ServiceRequestDate = DateTime.ParseExact(otherServiceRequest.ServiceRequestDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ServiceRequiredOnDate = DateTime.ParseExact(otherServiceRequest.ServiceRequiredOnDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    CustomerId = customerID,
                    PetId = petID,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    RequiredServiceSystemId = requiredServiceSystemId,
                    CustomerName = otherServiceRequest.CustomerName,
                    CustomerAddress = otherServiceRequest.CustomerAddress,
                    AddressLocationSystemId = addressLocationID,
                    Mobile = otherServiceRequest.Mobile,
                    Email = otherServiceRequest.Email,
                    PetName = otherServiceRequest.PetName,
                    PetImage = otherServiceRequest.PetImage,
                    PetColourBreedId = petColourBreedId,
                    PetColourSystemId = petColourSystemId,
                    CustomerAddressProof = otherServiceRequest.CustomerAddressProof
                };
                this.UnitOfWork.OtherServiceRequestRepository.Add(dbOtherServiceRequest);
                this.UnitOfWork.Complete();
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

        public bool DeleteOtherServiceRequest(string rowID)
        {
            try
            {
                var dbOtherServiceRequest = this.UnitOfWork.OtherServiceRequestRepository.FindFirst(c => c.RowId == rowID);
                if (dbOtherServiceRequest == null)
                {
                    throw new Exception("Failed to get the details for the requested service");
                }
                this.UnitOfWork.OtherServiceRequestRepository.Remove(dbOtherServiceRequest);
                this.UnitOfWork.Complete();
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

        public bool AssignUserToOtherServiceRequest(string rowID, DTO_AssignOtherServiceRequestUser assignOtherServiceRequestUser)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbOtherServiceRequest = this.UnitOfWork.OtherServiceRequestRepository.FindFirst(c => c.RowId == assignOtherServiceRequestUser.OtherServiceRequestMasterId);
                if (dbOtherServiceRequest == null)
                {
                    throw new Exception("Failed to get the other service request details");
                }

                var dbAssignedToUser = this.UnitOfWork.UserRepository.FindFirst(c => c.RowId == assignOtherServiceRequestUser.AssignedToUser.RowId);
                if (dbAssignedToUser == null)
                {
                    throw new Exception("Failed to get the user details for assign request");
                }

                this.UnitOfWork.AssignOtherServiceRequestRepo.Add(new AssignOtherServiceRequestUser
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    AssignDate = System.DateTime.Now,
                    OtherServiceRequestMasterId = dbOtherServiceRequest.Id,
                    ChangedRequestDate = DateTime.ParseExact(assignOtherServiceRequestUser.ChangedRequestDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    FromTime = DateTimeHelper.ConverDateTimeToTime(assignOtherServiceRequestUser.FromTime),
                    ToTime = DateTimeHelper.ConverDateTimeToTime(assignOtherServiceRequestUser.ToTime),
                    AssignedToUserId = dbAssignedToUser.Id,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = userID,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    IsAmountToBeCollectedAfterService = assignOtherServiceRequestUser.IsAmountToBeCollectedAfterService,
                    ServiceCharge = assignOtherServiceRequestUser.ServiceCharge
                });
                this.UnitOfWork.Complete();
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

        public bool DeleteUserToOtherServiceRequest(string rowID)
        {
            try
            {
                var dbAssignedUserToRequest = this.UnitOfWork.AssignOtherServiceRequestRepo.FindFirst(c => c.RowId == rowID);
                if (dbAssignedUserToRequest == null)
                {
                    throw new Exception("Failed to get the details for user assigned to service request");
                }
                this.UnitOfWork.AssignOtherServiceRequestRepo.Remove(dbAssignedUserToRequest);
                this.UnitOfWork.Complete();
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

        public DTO_OtherServiceRequest GetOtherServiceRequestById(string rowID)
        {
            var assignUserToOtherServiceRequest = this.UnitOfWork.OtherServiceRequestRepository.FindFirst(c => c.RowId == rowID, includeProperties: "Pet,Customer,RequiredServiceSystem,AddressLocationSystem,PetColourBreed,PetColourSystem");
            var response = this.mapper.Map<DTO_OtherServiceRequest>(assignUserToOtherServiceRequest);
            return response;
        }

        public DTO_AssignOtherServiceRequestUser OtherServiceRequestAssignedUser(string rowID, string assignedRequestRowId)
        {
            try
            {
                var dbOtherRequest = this.UnitOfWork.OtherServiceRequestRepository.FindFirst(c => c.RowId == rowID);
                if(dbOtherRequest == null)
                {
                    throw new Exception("Failed to get the details for the other service request");
                }

                var dbOtherRequestAssignedUser = this.UnitOfWork.AssignOtherServiceRequestRepo.FindFirst(c => c.RowId == assignedRequestRowId 
                && c.OtherServiceRequestMasterId == dbOtherRequest.Id,includeProperties: "AssignedToUser");
                if(dbOtherRequestAssignedUser == null)
                {
                    throw new Exception("Failed to get the details for the user for other service request");
                }
                var response = this.mapper.Map<DTO_AssignOtherServiceRequestUser>(dbOtherRequestAssignedUser);
                return response;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    SystemException = ex
                };
            }

        }

        public bool UpdateServiceRequestAssignedUser(string rowID, string assignedRequestRowId, DTO_AssignOtherServiceRequestUser updateEntity)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbOtherRequest = this.UnitOfWork.OtherServiceRequestRepository.FindFirst(c => c.RowId == rowID);
                if (dbOtherRequest == null)
                {
                    throw new Exception("Failed to get the details for the other service request");
                }

                var dbOtherRequestAssignedUser = this.UnitOfWork.AssignOtherServiceRequestRepo.FindFirst(c => c.RowId == assignedRequestRowId
                && c.OtherServiceRequestMasterId == dbOtherRequest.Id, includeProperties: "AssignedToUser");
                if (dbOtherRequestAssignedUser == null)
                {
                    throw new Exception("Failed to get the details for the assigned user for other service request");
                }

                var dbUser = this.UnitOfWork.UserRepository.FindFirst(c=>c.RowId == updateEntity.AssignedToUser.RowId);
                if (dbUser == null)
                {
                    throw new Exception("Failed to get the details for the user to update");
                }

                dbOtherRequestAssignedUser.AssignDate = DateTimeHelper.GetDate(updateEntity.AssignDate);
                dbOtherRequestAssignedUser.ChangedRequestDate = DateTimeHelper.GetDate(updateEntity.ChangedRequestDate);
                dbOtherRequestAssignedUser.FromTime = DateTimeHelper.ConvertTimeStringToDate(updateEntity.FromTime);
                dbOtherRequestAssignedUser.ToTime = DateTimeHelper.ConvertTimeStringToDate(updateEntity.ToTime);
                dbOtherRequestAssignedUser.AssignedToUserId = dbUser.Id;
                dbOtherRequestAssignedUser.UpdatedAt = System.DateTime.Now;
                dbOtherRequestAssignedUser.UpdatedBy = userID;
                dbOtherRequestAssignedUser.Remarks = updateEntity.Remarks;
                dbOtherRequestAssignedUser.ServiceCharge = updateEntity.ServiceCharge;
                dbOtherRequestAssignedUser.IsAmountToBeCollectedAfterService = updateEntity.IsAmountToBeCollectedAfterService;
                this.UnitOfWork.AssignOtherServiceRequestRepo.Update(dbOtherRequestAssignedUser);
                this.UnitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    SystemException = ex
                };
            }
        }
    }
}
