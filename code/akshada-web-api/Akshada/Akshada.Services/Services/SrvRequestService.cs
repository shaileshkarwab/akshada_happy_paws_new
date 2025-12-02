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
    public class SrvRequestService : ISrvRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SrvRequestService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool AddCustomerPetWalkingServiceRequest(string customerRowId, string petRowId, DTO_WalkingServiceRequest saveEntity)
        {

            try
            {
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                var customerDB = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId);
                if (customerDB == null)
                {
                    throw new Exception("Failed to find the customer details");
                }

                var petDB = this.unitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == petRowId);
                if (petDB == null)
                {
                    throw new Exception("Failed to find the customer pet details");
                }

                var serviceSystemDB = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == saveEntity.ServiceSystem.RowId);
                if (serviceSystemDB == null)
                {
                    throw new Exception("Failed to find the details for service system");
                }

                var frequencySystemDB = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == saveEntity.FrequencySystem.RowId);
                if (frequencySystemDB == null)
                {
                    throw new Exception("Failed to find the details for frequency system");
                }


                var existingRecord = this.unitOfWork.WalkingServiceRequestRepo.Any(c => c.FromDate <= DateTimeHelper.GetDate( saveEntity.ToDate)
                && c.ToDate >= DateTimeHelper.GetDate(saveEntity.FromDate)
                && c.PetId == petDB.Id
                && c.CustomerId == customerDB.Id
                && c.IsActive == true
                );
                if (existingRecord)
                {
                    throw new Exception("There is already a record existing for the selected date range");
                }
                var dbWalkingServiceRequest = new WalkingServiceRequest
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    CustomerId = customerDB.Id,
                    PetId = petDB.Id,
                    ServiceSystemId = serviceSystemDB.Id,
                    FrequencySystemId = frequencySystemDB.Id,
                    FromDate = DateTimeHelper.GetDate(saveEntity.FromDate),
                    ToDate = DateTimeHelper.GetDate(saveEntity.ToDate),
                    IsActive = true,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    RegularDayRate = saveEntity.RegularDayRate,
                    SpecialDayRate = saveEntity.SpecialDayRate,
                    IsChargedMonthly = saveEntity.IsChargedMonthly
                };
                foreach (var m in saveEntity.WalkingServiceRequestDays.Where(c => c.IsSelected == true).ToList())
                {
                    var dbWalkingRequestDays = new WalkingServiceRequestDay
                    {
                        RowId = System.Guid.NewGuid().ToString(),
                        IsSelected = m.IsSelected.Value,
                        WeekDayName = m.WeekDayName
                    };
                    foreach (var sch in m.WalkingServiceRequestDaySchedules)
                    {
                        dbWalkingRequestDays.WalkingServiceRequestDaySchedules.Add(new WalkingServiceRequestDaySchedule
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            FromTime = Convert.ToDateTime(sch.FromTime),
                            ToTime = Convert.ToDateTime(sch.ToTime)
                        });
                    }
                    dbWalkingServiceRequest.WalkingServiceRequestDays.Add(dbWalkingRequestDays);
                }
                this.unitOfWork.WalkingServiceRequestRepo.Add(dbWalkingServiceRequest);
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

        public bool AddWalkingServiceRequest(DTO_WalkingRecord walkingRecord)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];

                var customerDB = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == walkingRecord.Customer.RowId);
                if (customerDB == null)
                {
                    throw new Exception("Failed to find the customer details");
                }

                var petDB = this.unitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == walkingRecord.Pet.RowId);
                if (petDB == null)
                {
                    throw new Exception("Failed to find the customer pet details");
                }

                var selectedWalkingServiceRequest = walkingRecord.WalkingServiceRequestServices.FirstOrDefault(c => c.Selected == true);
                if (selectedWalkingServiceRequest == null)
                {
                    throw new Exception("Faild to get the selected walking request record");
                }

                var serviceRequestDB = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == selectedWalkingServiceRequest.WalkingServiceMasterId);
                if (serviceRequestDB == null)
                {
                    throw new Exception("Faild to get the details for the selected walking service request");
                }

                var serviceRequestDayDB = this.unitOfWork.WalkingServiceRequestDaysRepo.FindFirst(c => c.RowId == selectedWalkingServiceRequest.WalkingServiceDayMasterId);
                if (serviceRequestDayDB == null)
                {
                    throw new Exception("Faild to get the details for the selected walking day service request");
                }

                var serviceRequestDayScheduleDB = this.unitOfWork.WalkingServiceRequestDayScheduleRepo.FindFirst(c => c.RowId == selectedWalkingServiceRequest.WalkingServiceDayScheduleMasterId);
                if (serviceRequestDayScheduleDB == null)
                {
                    throw new Exception("Faild to get the details for the selected walking day schedule service request");
                }

                var dbServiceOfferedUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == walkingRecord.ServiceOfferedByUser.RowId);
                if (dbServiceOfferedUser == null)
                {
                    throw new Exception("Faild to get the details for the service offered by user");
                }

                var walkingService = new WalkingServiceRecord
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    ServiceOfferedDate = DateTimeHelper.GetDate(walkingRecord.ServiceOfferedDate),
                    FromTime = DateTimeHelper.ConverDateTimeToTime(walkingRecord.FromTime),
                    ToTime = DateTimeHelper.ConverDateTimeToTime(walkingRecord.ToTime),
                    CustomerId = customerDB.Id,
                    PetId = petDB.Id,
                    WalkingServiceMasterId = serviceRequestDB.Id,
                    WalkingServiceDayMasterId = serviceRequestDayDB.Id,
                    WalkingServiceDayScheduleMasterId = serviceRequestDayScheduleDB.Id,
                    Remarks = walkingRecord.Remarks,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    ServiceOfferedByUserId = dbServiceOfferedUser.Id,
                };

                foreach(var img in walkingRecord.WalkingServiceRecordImages.Where(c=>!string.IsNullOrEmpty(c.ImageName)))
                {
                    var imageTypeSystemParam = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == img.UploadImageSystemParam.RowId);
                    if (imageTypeSystemParam == null)
                    {
                        throw new Exception("Failed to get the image system parameter for file upoad");
                    }


                    walkingService.WalkingServiceRecordImages.Add(new WalkingServiceRecordImage { 
                        RowId = System.Guid.NewGuid().ToString(),
                        ImageUploadSystemId = imageTypeSystemParam.Id,
                        ImageName = img.ImageName,
                        Lattitude = img.Lattitude.Value,
                        Longitude = img.Longitude.Value,
                        RecordTime = DateTimeHelper.ConvertTimeStringToDate( img.RecordTime)
                    });
                }
                this.unitOfWork.WalkingServiceRecordRepo.Add(walkingService);
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

        public bool AssignUserToPetWalkingServiceSchedule(string customerRowId, string petRowId, string serviceRequestId, List<DTO_WalkingServiceRequestDayScheduleAssignedToUser> walkingServiceRequestDayScheduleAssignedToUsers)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the customer");
                }

                var dbPet = this.unitOfWork.CustomerRepository.ReteriveCustomerPetInformation(petRowId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the details for the customer pet");
                }

                var dbWalkingService = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == serviceRequestId);
                if (dbWalkingService == null)
                {
                    throw new Exception("Failed to get the details for the customer pet walking service request");
                }

                foreach (var m in walkingServiceRequestDayScheduleAssignedToUsers)
                {
                    var userDB = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == m.UserId);
                    if (userDB == null)
                    {
                        throw new Exception("Failed to get the details for the user");
                    }

                    var dbWalkingSchedule = this.unitOfWork.WalkingServiceRequestDayScheduleRepo.FindFirst(w => w.RowId == m.WalkingServiceRequestDayScheduleId);
                    if (dbWalkingSchedule == null)
                    {
                        throw new Exception("Failed to get the details for the walking schedule");
                    }

                    if (!string.IsNullOrEmpty(m.RowId))
                    {
                        var dbAssignedUserForSchedule = this.unitOfWork.WalkingScheduleAssignUserRepo.FindFirst(c => c.RowId == m.RowId);
                        if (dbAssignedUserForSchedule == null)
                        {
                            throw new Exception("Failed to get the assign user details for the selected schedule");
                        }
                        dbAssignedUserForSchedule.UpdatedBy = userID;
                        dbAssignedUserForSchedule.UpdatedAt = System.DateTime.Now;
                        dbAssignedUserForSchedule.UserId = userDB.Id;
                        this.unitOfWork.WalkingScheduleAssignUserRepo.Update(dbAssignedUserForSchedule);
                    }
                    else
                    {
                        this.unitOfWork.WalkingScheduleAssignUserRepo.Add(new EFCore.DbModels.WalkingServiceRequestDayScheduleAssignedToUser
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            WalkingServiceRequestDayScheduleId = dbWalkingSchedule.Id,
                            UserId = userDB.Id,
                            CreatedAt = DateTime.Now,
                            CreatedBy = userID,
                            UpdatedBy = userID,
                            UpdatedAt = DateTime.Now,
                            WalkingServiceRequestMasterId = dbWalkingService.Id
                        });
                    }
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

        public bool ChangeUserAndAssignNewTime(string customerRowId, string petRowId, string serviceRequestId, DTO_ChangeTimeOrUserForRequest changeTimeOrUserForRequest)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the selected customer");
                }

                var dbPet = this.unitOfWork.CustomerRepository.ReteriveCustomerPetInformation(petRowId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the details for the customer pet");
                }

                var dbWalkingRequest = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == serviceRequestId);
                if (dbWalkingRequest == null)
                {
                    throw new Exception("Failed to get the details for the walking service request");
                }

                var dbWalkingRequestDay = this.unitOfWork.WalkingServiceRequestDaysRepo.FindFirst(c => c.RowId == changeTimeOrUserForRequest.WalkingServiceRequestDayRowId);
                if (dbWalkingRequest == null)
                {
                    throw new Exception("Failed to get the details for the walking service request day");
                }


                var dbWalkingRequestDaySchedule = this.unitOfWork.WalkingServiceRequestDayScheduleRepo.FindFirst(c => c.RowId == changeTimeOrUserForRequest.WalkingServiceRequestDayScheduleRowId);
                if (dbWalkingRequestDaySchedule == null)
                {
                    throw new Exception("Failed to get the details for the walking service request day schedule");
                }


                var dbUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == changeTimeOrUserForRequest.NewUserId);
                if (dbUser == null)
                {
                    throw new Exception("Failed to get the details for the new user");
                }

                this.unitOfWork.NewUserAssignToWalkingSrvRepo.Add(new EFCore.DbModels.NewUserAssignToWalkingService
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    AssignDate = DateTime.ParseExact(changeTimeOrUserForRequest.AssignDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    CustomerId = dbCustomer.Id,
                    PetId = dbPet.CustomerPets.FirstOrDefault().Id,
                    WalkingRequestMasterId = dbWalkingRequest.Id,
                    WalkingRequestDayMasterId = dbWalkingRequestDay.Id,
                    WalkingRequestScheduleMasterId = dbWalkingRequestDaySchedule.Id,
                    UserId = dbUser.Id,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    Remarks = changeTimeOrUserForRequest.Remarks,
                    ChangeFromTime = Convert.ToDateTime(changeTimeOrUserForRequest.ChangeFromTime),
                    ChangeToTime = Convert.ToDateTime(changeTimeOrUserForRequest.ChangeToTime)
                });
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

        public bool DeleteChangeUserAndAssignNewTime(string assignedRequestId)
        {
            try
            {
                var dbAssignWalkingRequest = this.unitOfWork.NewUserAssignToWalkingSrvRepo.FindFirst(c=>c.RowId == assignedRequestId);
                if (dbAssignWalkingRequest == null)
                {
                    throw new Exception("Failed to get the details for the assigned request");
                }
                this.unitOfWork.NewUserAssignToWalkingSrvRepo.Remove(dbAssignWalkingRequest);
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

        public bool DeleteCustomerPetWalkingServiceRequest(string serviceRowId)
        {
            try
            {
                var dbServiceRequest = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == serviceRowId, "WalkingServiceRequestDays,WalkingServiceRequestDays.WalkingServiceRequestDaySchedules");
                var success = this.unitOfWork.WalkingServiceRequestRepo.RemoveIncludingChildren("WalkingServiceRequestDays,WalkingServiceRequestDays.WalkingServiceRequestDaySchedules", p => p.Id == dbServiceRequest.Id);
                if (success)
                {
                    this.unitOfWork.Complete();
                }
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

        public bool DeleteGoogleGormRequests(string requestRowId)
        {
            try
            {
                var dbGoogleFormRequest = this.unitOfWork.GoogleFormSubmissionRepository.FindFirst(c => c.RowId == requestRowId);
                if(dbGoogleFormRequest == null)
                {
                    throw new Exception("Failed to get the details for the requested google form");
                }
                this.unitOfWork.GoogleFormSubmissionRepository.Remove(dbGoogleFormRequest);
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

        public bool DeleteGoogleServiceRequests(string rowId)
        {
            try
            {
                var dbWebService = this.unitOfWork.WebsiteServiceRepository.FindFirst(c => c.RowId == rowId);
                if(dbWebService == null)
                {
                    throw new Exception("Failed to get the details for the selected web service request");
                }
                this.unitOfWork.WebsiteServiceRepository.Remove(dbWebService);
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

        public List<DTO_WalkingServiceRequestServices> GetActiveWalkingServiceSlotsForCustomerIdPetId(string customerID, string petId, string selectedDate)
        {
            try
            {
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerID);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the selected customer");
                }

                var dbPet = this.unitOfWork.CustomerRepository.ReteriveCustomerPetInformation(petId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the details for the customer pet");
                }

                var pet = dbPet.CustomerPets.FirstOrDefault(c => c.RowId == petId);
                var walkingServiceRequests = this.unitOfWork.WalkingServiceRequestRepo.GetAllWithInclude(c => c.CustomerId == dbCustomer.Id 
                && c.PetId == pet.Id
                && c.IsActive == true, includeProperties: "WalkingServiceRequestDays, WalkingServiceRequestDays.WalkingServiceRequestDaySchedules");

                var serviceOfferedDte = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var serviceOfferedDays = new List<DTO_WalkingServiceRequestServices>();
                foreach (var request in walkingServiceRequests.Where(d => serviceOfferedDte >= d.FromDate && serviceOfferedDte <= d.ToDate))
                {
                    foreach (var day in request.WalkingServiceRequestDays.Where(d => d.IsSelected == true && d.WeekDayName == serviceOfferedDte.DayOfWeek.ToString()))
                    {
                        foreach (var sch in day.WalkingServiceRequestDaySchedules)
                        {
                            serviceOfferedDays.Add(new DTO_WalkingServiceRequestServices
                            {
                                DayName = day.WeekDayName,
                                FromTime = sch.FromTime.ToString(),
                                Selected = false,
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
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public List<DTO_WalkingServiceRequest> GetCustomerServiceRequests(string customerRowId, string petRowId)
        {
            var walkingServiceRequests = this.unitOfWork.ServiceRequestRepository.GetCustomerServiceRequests(customerRowId, petRowId);
            var response = this.mapper.Map<List<DTO_WalkingServiceRequest>>(walkingServiceRequests);
            return response;
        }

        public DTO_WalkingServiceRequest GetDetailsForWalkingService(string customerRowId, string petRowId, string serviceRequestRowId)
        {
            var serviceRequest = this.unitOfWork.ServiceRequestRepository.GetDetailsForWalkingService(customerRowId, petRowId, serviceRequestRowId);
            var response = this.mapper.Map<DTO_WalkingServiceRequest>(serviceRequest);
            return response;
        }

        public PagedList<DTO_GoogleFormRawData> GetGoogleGormRequests(DTO_FilterAndPaging filterAndPagings)
        {
            var requestQuery = this.unitOfWork.GoogleFormSubmissionRepository.GetAllWithInclude(x=>x.PetId == null & x.CustomerId == null,  includeProperties: "Customer,Pet");
            var pagedList = PagedList<GoogleFormSubmission>.ToPagedList(requestQuery, filterAndPagings.PageParameter.PageNumber, filterAndPagings.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_GoogleFormRawData>>(pagedList);
            return new PagedList<DTO_GoogleFormRawData>(response,pagedList.TotalCount,pagedList.CurrentPage,pagedList.PageSize);
        }

        public PagedList<DTO_ReciveFormSubmission> GetGoogleServiceRequests(DTO_FilterAndPaging filterAndPagings)
        {
            var requestQuery = this.unitOfWork.WebsiteServiceRepository.GetActiveWebRequest();
            var pagesList = PagedList<WebsiteService>.ToPagedList(requestQuery, filterAndPagings.PageParameter.PageNumber, filterAndPagings.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_ReciveFormSubmission>>(pagesList);
            return new PagedList<DTO_ReciveFormSubmission>(response, pagesList.TotalCount, pagesList.CurrentPage, pagesList.PageSize);
        }

        public PagedList<DTO_WalkingServiceRequestQuery> GetPetServiceDetails(DTO_FilterAndPaging filterAndPagings)
        {
            var queryBasedOnDate = this.unitOfWork.ServiceRequestRepository.GetPetServiceDetails(filterAndPagings);

            //remove the date filter
            filterAndPagings.DateFilters= null;
            var query = queryBasedOnDate.ApplyAdvanceFilters(filterAndPagings);

            var responsePagedList = PagedList<WalkingServiceRequestQuery>.ToPagedList(query,filterAndPagings.PageParameter.PageNumber,filterAndPagings.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_WalkingServiceRequestQuery>>(responsePagedList);
            return new PagedList<DTO_WalkingServiceRequestQuery>(response, responsePagedList.TotalCount, responsePagedList.CurrentPage, responsePagedList.PageSize);
            
            
            //var serviceRequest = this.unitOfWork.ServiceRequestRepository.GetPetServiceDetails(searchCriteria);

            //var allDates = Enumerable.Range(0, (searchCriteria.selectedToDate.Value - searchCriteria.selectedFromDate.Value).Days + 1)
            //.Select(i => searchCriteria.selectedFromDate.Value.AddDays(i))
            //.ToList();

            //var response = this.mapper.Map<List<DTO_CustomerPetForActivity>>(serviceRequest);

            //var finalResponse = (
            //        from r in response
            //        from d in allDates
            //        select new DTO_CustomerPetForActivity
            //        {
            //            AreaLocationSystem = r.AreaLocationSystem,
            //            AssignedTo = r.AssignedTo,
            //            FromDate = r.FromDate,
            //            ToDate = r.ToDate,
            //            FromTime = r.AssignRequestId == null ? r.FromTime : r.NewUserAssignToWalkingServices.FirstOrDefault().ChangeFromTime.Value,
            //            ToTime = r.AssignRequestId == null ? r.ToTime : r.NewUserAssignToWalkingServices.FirstOrDefault().ChangeToTime.Value,
            //            Pet = r.Pet,
            //            RowId = r.RowId,
            //            ServiceDate = d,
            //            ServiceFrequency = r.ServiceFrequency,
            //            ServiceSystem = r.ServiceSystem,
            //            FromTimeStr = r.AssignRequestId == null ? r.FromTime.ToString("yyyy-MM-dd hh:mm:ss tt") : r.NewUserAssignToWalkingServices.FirstOrDefault().ChangeFromTime.Value.ToString("yyyy-MM-dd hh:mm:ss tt"),
            //            ToTimeStr = r.AssignRequestId == null ? r.ToTime.ToString("yyyy-MM-dd hh:mm:ss tt") : r.NewUserAssignToWalkingServices.FirstOrDefault().ChangeToTime.Value.ToString("yyyy-MM-dd hh:mm:ss tt"),
            //            AssignedToUser = r.AssignRequestId == null ? r.AssignedToUser : r.NewUserAssignToWalkingServices.FirstOrDefault().UserLookUp,
            //            CustomerRowId = r.CustomerRowId,
            //            SelectedUser = r.AssignRequestId == null ? r.SelectedUser : r.NewUserAssignToWalkingServices.FirstOrDefault().User,
            //            WalkingServiceRequestDayScheduleId = r.WalkingServiceRequestDayScheduleId,
            //            WalkingServiceRequestDaysId = r.WalkingServiceRequestDaysId,
            //            AssignRequestId = r.AssignRequestId,
            //            Remarks = r.AssignRequestId == null ? string.Empty : r.NewUserAssignToWalkingServices.FirstOrDefault().Remarks
            //        }
            //    ).ToList();

            //return finalResponse;


        }

        public PagedList<DTO_WalingServiceList> GetWalkingServiceRequests(DTO_FilterAndPaging filterAndPagings)
        {
            var response = this.unitOfWork.ServiceRequestRepository.GetWalkingServiceRequests().ApplyAdvanceFilters(filterAndPagings);
            var pagedList = PagedList<WalkingServiceRequest>.ToPagedList(response, filterAndPagings.PageParameter.PageNumber, filterAndPagings.PageParameter.PageSize);
            var requests = this.mapper.Map<List<DTO_WalingServiceList>>(pagedList);
            return new PagedList<DTO_WalingServiceList>(requests,pagedList.TotalCount,pagedList.CurrentPage,pagedList.PageSize);
        }

        public bool UpdateChangeUserAndAssignNewTime(string customerRowId, string petRowId, string serviceRequestId, string assignedRequestId, DTO_ChangeTimeOrUserForRequest changeTimeOrUserForRequest)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the selected customer");
                }

                var dbPet = this.unitOfWork.CustomerRepository.ReteriveCustomerPetInformation(petRowId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the details for the customer pet");
                }

                var dbWalkingRequest = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == serviceRequestId);
                if (dbWalkingRequest == null)
                {
                    throw new Exception("Failed to get the details for the walking service request");
                }

                var dbWalkingRequestDay = this.unitOfWork.WalkingServiceRequestDaysRepo.FindFirst(c => c.RowId == changeTimeOrUserForRequest.WalkingServiceRequestDayRowId);
                if (dbWalkingRequest == null)
                {
                    throw new Exception("Failed to get the details for the walking service request day");
                }


                var dbWalkingRequestDaySchedule = this.unitOfWork.WalkingServiceRequestDayScheduleRepo.FindFirst(c => c.RowId == changeTimeOrUserForRequest.WalkingServiceRequestDayScheduleRowId);
                if (dbWalkingRequestDaySchedule == null)
                {
                    throw new Exception("Failed to get the details for the walking service request day schedule");
                }


                var dbUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == changeTimeOrUserForRequest.NewUserId);
                if (dbUser == null)
                {
                    throw new Exception("Failed to get the details for the new user");
                }

                var dbAssignWalkingRequest = this.unitOfWork.NewUserAssignToWalkingSrvRepo.FindFirst(c => c.RowId == assignedRequestId);
                if (dbAssignWalkingRequest == null)
                {
                    throw new Exception("Failed to get the details for the assigned request");
                }
                dbAssignWalkingRequest.ChangeFromTime = Convert.ToDateTime(changeTimeOrUserForRequest.ChangeFromTime);
                dbAssignWalkingRequest.ChangeToTime = Convert.ToDateTime(changeTimeOrUserForRequest.ChangeToTime);
                dbAssignWalkingRequest.Remarks = changeTimeOrUserForRequest.Remarks;
                dbAssignWalkingRequest.UserId = dbUser.Id;
                dbAssignWalkingRequest.UpdatedAt = System.DateTime.Now;
                dbAssignWalkingRequest.UpdatedBy = userID;
                this.unitOfWork.NewUserAssignToWalkingSrvRepo.Update(dbAssignWalkingRequest);
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

        public bool UpdateCustomerPetWalkingServiceRequest(string customerRowId, string petRowId, string serviceRequestId, DTO_WalkingServiceRequest updateEntity)
        {
            var response = this.unitOfWork.ServiceRequestRepository.UpdateCustomerPetWalkingServiceRequest(customerRowId, petRowId, serviceRequestId, updateEntity);
            return response;
        }

        public bool UpdateWalkingServiceRequest(string walkingRequestRowId, DTO_WalkingRecord walkingRecord)
        {
            try
            {
                var dbWalkingRequest = this.unitOfWork.WalkingServiceRecordRepo.FindFirst(c => c.RowId == walkingRequestRowId);
                if (dbWalkingRequest == null)
                {
                    throw new Exception("Faild to get the walking service record");
                }
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];

                var selectedWalkingServiceRequest = walkingRecord.WalkingServiceRequestServices.FirstOrDefault(c => c.Selected == true);
                if (selectedWalkingServiceRequest == null)
                {
                    throw new Exception("Faild to get the selected walking request record");
                }

                var customerDB = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == walkingRecord.Customer.RowId);
                if (customerDB == null)
                {
                    throw new Exception("Faild to get the details for the selected customer");
                }

                var petDB = this.unitOfWork.CustomerPetsRepository.FindFirst(c => c.RowId == walkingRecord.Pet.RowId);
                if (petDB == null)
                {
                    throw new Exception("Faild to get the details for the selected customer pet");
                }

                var serviceRequestDB = this.unitOfWork.WalkingServiceRequestRepo.FindFirst(c => c.RowId == selectedWalkingServiceRequest.WalkingServiceMasterId);
                if (serviceRequestDB == null)
                {
                    throw new Exception("Faild to get the details for the selected walking service request");
                }

                var serviceRequestDayDB = this.unitOfWork.WalkingServiceRequestDaysRepo.FindFirst(c => c.RowId == selectedWalkingServiceRequest.WalkingServiceDayMasterId);
                if (serviceRequestDayDB == null)
                {
                    throw new Exception("Faild to get the details for the selected walking day service request");
                }

                var serviceRequestDayScheduleDB = this.unitOfWork.WalkingServiceRequestDayScheduleRepo.FindFirst(c => c.RowId == selectedWalkingServiceRequest.WalkingServiceDayScheduleMasterId);
                if (serviceRequestDayScheduleDB == null)
                {
                    throw new Exception("Faild to get the details for the selected walking day schedule service request");
                }

                var dbServiceOfferedUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == walkingRecord.ServiceOfferedByUser.RowId);
                if (dbServiceOfferedUser == null)
                {
                    throw new Exception("Faild to get the details for the service offered by user");
                }

                dbWalkingRequest.ServiceOfferedDate = DateTime.ParseExact(walkingRecord.ServiceOfferedDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dbWalkingRequest.FromTime = DateTime.ParseExact(walkingRecord.FromTime, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                dbWalkingRequest.ToTime = DateTime.ParseExact(walkingRecord.ToTime, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                dbWalkingRequest.CustomerId = customerDB.Id;
                dbWalkingRequest.PetId = petDB.Id;
                dbWalkingRequest.WalkingServiceMasterId = serviceRequestDB.Id;
                dbWalkingRequest.WalkingServiceDayMasterId = serviceRequestDayDB.Id;
                dbWalkingRequest.WalkingServiceDayScheduleMasterId = serviceRequestDayScheduleDB.Id;
                dbWalkingRequest.Remarks = walkingRecord.Remarks;
                dbWalkingRequest.UpdatedAt = System.DateTime.Now;
                dbWalkingRequest.UpdatedBy = userID;
                dbWalkingRequest.ServiceOfferedByUserId = dbServiceOfferedUser.Id;
                //updating or adding the images
                var walkingRecordImages = new List<WalkingServiceRecordImage>();
                foreach (var img in walkingRecord.WalkingServiceRecordImages.Where(i => !string.IsNullOrEmpty(i.ImageName)))
                {
                    var dbWalkingRecordImage = this.unitOfWork.WalkingServiceRecordImagesRepo.FindFirst(c => c.RowId == img.RowId);
                    var imageTypeSystemParam = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == img.UploadImageSystemParam.RowId);
                    if (imageTypeSystemParam == null)
                    {
                        throw new Exception("Failed to get the image system parameter for file upoad");
                    }
                    if (dbWalkingRecordImage != null)
                    {
                        dbWalkingRecordImage.ImageUploadSystemId = imageTypeSystemParam.Id;
                        dbWalkingRecordImage.ImageName = img.ImageName;
                        dbWalkingRecordImage.Lattitude = img.Lattitude.Value;
                        dbWalkingRecordImage.Longitude = img.Longitude.Value;
                        dbWalkingRecordImage.RecordTime = DateTimeHelper.ConvertTimeStringToDate(img.RecordTime);
                        this.unitOfWork.WalkingServiceRecordImagesRepo.Update(dbWalkingRecordImage);
                    }
                    else
                    {
                        dbWalkingRequest.WalkingServiceRecordImages.Add(new WalkingServiceRecordImage
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            ImageName = img.ImageName,
                            ImageUploadSystemId = imageTypeSystemParam.Id,
                            Lattitude = img.Lattitude.Value,
                            Longitude = img.Longitude.Value,    
                            RecordTime = DateTimeHelper.ConvertTimeStringToDate(img.RecordTime)
                        });
                    }
                }

                this.unitOfWork.WalkingServiceRecordRepo.Update(dbWalkingRequest);
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
    }
}
