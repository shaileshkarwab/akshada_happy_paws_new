using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
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
    public class WebsiteDataService : IWebsiteDataService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISignalRService signalRService;
        public WebsiteDataService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ISignalRService signalRService)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.signalRService = signalRService;   
        }

        public async Task<bool> CaptureServiecRequest(DTO_ReciveFormSubmission reciveFormSubmission)
        {
            await this.signalRService.SendMessageAsync("SERVICE_ADDED");
            var requestData = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestData>(reciveFormSubmission.JsonContent);
            this.unitOfWork.WebsiteServiceRepository.Add(new EFCore.DbModels.WebsiteService
            {
                Body = reciveFormSubmission.Body,
                RowId = System.Guid.NewGuid().ToString(),
                JsonData = reciveFormSubmission.JsonContent,
            });
            this.unitOfWork.Complete();
            await this.signalRService.SendMessageAsync("SERVICE_ADDED");
            return true;
        }

        public async Task<RequestData> ReteriveWebServiceData(string recordID)
        {
            try
            {
                var dbWebServiceRequest = this.unitOfWork.WebsiteServiceRepository.FindFirst(c => c.RowId == recordID);
                if (dbWebServiceRequest == null)
                {
                    throw new Exception("Failed to get the details for the selected request");
                }
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestData>(dbWebServiceRequest.JsonData);
                data.RowId = dbWebServiceRequest.RowId;
                return await Task.Run(() =>
                {
                    return data;
                });
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

        public Task<bool> SaveGoogleFormSubmissionRawData(string jsonData)
        {
            try
            {
                return Task.Run(() =>
                {
                    this.unitOfWork.GoogleFormSubmissionRepository.Add(new EFCore.DbModels.GoogleFormSubmission
                    {
                        RowId = System.Guid.NewGuid().ToString(),
                        JsonData = jsonData
                    });
                    this.unitOfWork.Complete();
                    this.signalRService.SendMessageAsync("FORM_ADDED");
                    return true;
                });
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

        public Task<bool> SaveWebServiceData(string recordID, DTO_WebsiteServiceProcess websiteServiceProcess)
        {
            try
            {
                var dbWebServiceRequest = this.unitOfWork.WebsiteServiceRepository.FindFirst(c => c.RowId == recordID);
                if (dbWebServiceRequest == null)
                {
                    throw new Exception("Failed to get the details for the web service request");
                }
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                var dbNoRequestSystemParam = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == websiteServiceProcess.RequestNotParamSystem.RowId);
                var dbUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == websiteServiceProcess.AssignedToUser.RowId);
                var dbServiceSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == websiteServiceProcess.ServiceSystem.RowId);

                var webServiceProcess = new EFCore.DbModels.WebsiteServiceProcess
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    WebsiteServiceMasterId = dbWebServiceRequest.Id,
                    RequestNotParamSystemId = dbNoRequestSystemParam == null ? null : dbNoRequestSystemParam.Id,
                    CustomerName = websiteServiceProcess.CustomerName,
                    MobileContactNumber = websiteServiceProcess.MobileContactNumber,
                    ServiceDate = DateTimeHelper.GetDate(websiteServiceProcess.ServiceDate),
                    ServiceFromTime = DateTimeHelper.ConverDateTimeToTime(websiteServiceProcess.ServiceFromTime),
                    ServiceToTime = DateTimeHelper.ConverDateTimeToTime(websiteServiceProcess.ServiceToTime),
                    AssignedToUserId = dbUser == null ? null : dbUser.Id,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    Email = websiteServiceProcess.Email,
                    Remarks = websiteServiceProcess.Remarks,
                    ServiceSystemId = dbServiceSystem.Id,
                    RequestAccepted = websiteServiceProcess.RequestAccepted.Value
                };

                //add the record to other_service_request
                if (websiteServiceProcess.RequestAccepted.HasValue && websiteServiceProcess.RequestAccepted == true)
                {
                    var areaLocation = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == websiteServiceProcess.AreaLocationSystem.RowId);
                    var otherServiceRequest = new EFCore.DbModels.OtherServiceRequest
                    {
                        RowId = System.Guid.NewGuid().ToString(),
                        ServiceRequestDate = DateTimeHelper.GetDate(websiteServiceProcess.ServiceDate),
                        ServiceRequiredOnDate = DateTimeHelper.GetDate(websiteServiceProcess.ServiceDate),
                        CustomerName = websiteServiceProcess.CustomerName,
                        CustomerAddress = websiteServiceProcess.Address,
                        Mobile = websiteServiceProcess.MobileContactNumber,
                        Email = websiteServiceProcess.Email,
                        CreatedAt = System.DateTime.Now,
                        CreatedBy = userID,
                        UpdatedAt = System.DateTime.Now,
                        UpdatedBy = userID,
                        RequiredServiceSystemId = dbServiceSystem.Id,
                        AddressLocationSystemId = areaLocation == null ? null : areaLocation.Id,
                        AssignOtherServiceRequestUser = new EFCore.DbModels.AssignOtherServiceRequestUser
                        {
                            RowId = System.Guid.NewGuid().ToString(),
                            AssignDate = DateTimeHelper.GetDate(websiteServiceProcess.ServiceDate),
                            ChangedRequestDate = DateTimeHelper.GetDate(websiteServiceProcess.ServiceDate),
                            FromTime = DateTimeHelper.ConverDateTimeToTime(websiteServiceProcess.ServiceFromTime),
                            ToTime = DateTimeHelper.ConverDateTimeToTime(websiteServiceProcess.ServiceToTime),
                            AssignedToUserId = dbUser.Id,
                            CreatedAt = System.DateTime.Now,
                            CreatedBy = userID,
                            UpdatedAt = System.DateTime.Now,
                            UpdatedBy = userID,
                            Remarks = websiteServiceProcess.Remarks
                        }
                    };
                    webServiceProcess.OtherServiceRequests.Add(otherServiceRequest);
                }
                this.unitOfWork.WebsiteServiceProcessRepository.Add(webServiceProcess);
                this.unitOfWork.Complete();
                return Task.Run(() =>
                {
                    return true;
                });
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
