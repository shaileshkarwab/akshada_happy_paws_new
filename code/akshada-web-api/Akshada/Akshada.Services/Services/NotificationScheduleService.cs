using Akshada.DTO.Enums;
using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class NotificationScheduleService : INotificationScheduleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly ITemplateService<DTO_EmailTemplateVariable> _templateService;
        private readonly IEmailService emailService;
        public NotificationScheduleService(IEmailService emailService,IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITemplateService<DTO_EmailTemplateVariable> _templateService)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this._templateService = _templateService;
            this.emailService = emailService;
        }

        public bool DeleteNotificationByID(string notificationRowId)
        {
            try
            {
                var dbNotification = this.unitOfWork.NotificationScheduleRepository.FindFirst(c => c.RowId == notificationRowId);
                if (dbNotification == null)
                {
                    throw new Exception("Failed to get the details for the notification details");
                }
                this.unitOfWork.NotificationScheduleRepository.Remove(dbNotification);
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

        public bool DeleteNotificationList(string systemParamRowId)
        {
            try
            {
                var dbNotificationSchedule = this.unitOfWork.NotificationScheduleRepository.GetAllWithInclude(c => c.NotificationEnumId == Convert.ToInt32(systemParamRowId));
                this.unitOfWork.NotificationScheduleRepository.Remove(dbNotificationSchedule.AsEnumerable());
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

        public List<DTO_LookUp> GetNotificationList()
        {
            var dbNotification = this.unitOfWork.SystemParamRepository.GetAllWithInclude(c => c.EnumId == (Int32)SystemParameterEnum.TypeOfHolidaysAndSpecialDays);
            var dtoNotificationList = this.mapper.Map<List<DTO_LookUp>>(dbNotification);
            return dtoNotificationList;
        }

        public List<DTO_NotificationSchedule> GetNotificationList(string systemParamRowId)
        {
            try
            {
                var dbNotificatons = this.unitOfWork.NotificationScheduleRepository.Find(c => c.NotificationEnumId == Convert.ToInt32(systemParamRowId));
                var response = this.mapper.Map<List<DTO_NotificationSchedule>>(dbNotificatons);
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

        public bool SaveEmailTemplate(DTO_EmailTemplateMaster emailTemplateMaster)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var dbEmailNotificationSystemID = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == emailTemplateMaster.EmailNotificationSystem.RowId);
                if(dbEmailNotificationSystemID == null)
                {
                    throw new Exception("Failed to get details for the notification type");
                }
                var dbEmailTemplateMaster = new EmailTemplateMaster {
                    RowId = System.Guid.NewGuid().ToString(),
                    EmailNotificationSystemId = dbEmailNotificationSystemID.Id,
                    EmailEventName = emailTemplateMaster.EmailEventName,
                    EmailEventDate = DateTimeHelper.GetDateOnly( emailTemplateMaster.EmailEventDate),
                    EmailEventRepeatForEveryYear = emailTemplateMaster.EmailEventRepeatForEveryYear,
                    IsActive = emailTemplateMaster.IsActive,
                    CreatedBy = userID,
                    CreatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    HtmlEmailTemplate = emailTemplateMaster.HtmlEmailTemplate,
                    EmailSubject = emailTemplateMaster.EmailSubject
                };

                foreach (var sch in emailTemplateMaster.EmailTemplateMasterScheduleDetails) {
                    dbEmailTemplateMaster.EmailTemplateMasterScheduleDetails.Add(new EmailTemplateMasterScheduleDetail { 
                        RowId = System.Guid.NewGuid().ToString(),
                        ReminderDays = sch.ReminderDays,
                        TimeForNotification = DateTimeHelper.ConverDateTimeToTime( sch.TimeForNotification)
                    });
                }

                this.unitOfWork.EmailTemplateMasterRepository.Add(dbEmailTemplateMaster);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    Message = ex.Message,
                    SystemException = ex,
                    StatusCode = (Int32)HttpStatusCode.BadRequest
                };
            }
        }

        public bool SaveNotificationSchedule(List<DTO_NotificationSchedule> notificationSchedules)
        {

            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                foreach (var m in notificationSchedules)
                {
                    var dbNotificationTypeSystem = this.unitOfWork.SystemParamRepository.FindFirst(r => r.RowId == m.NotificationTypeSystem.RowId);
                    if (dbNotificationTypeSystem == null)
                    {
                        throw new Exception("Failed to get the details for the selected schedule type");
                    }
                    this.unitOfWork.NotificationScheduleRepository.Add(new NotificationSchedule
                    {
                        BeforeDays = Convert.ToInt16(m.BeforeDays),
                        CreatedAt = DateTime.Now,
                        CreatedBy = userID,
                        NotificationEnumId = dbNotificationTypeSystem.Id,
                        RowId = System.Guid.NewGuid().ToString(),
                        ScheduleOnTime = DateTimeHelper.ConvertTimeStringToDate(m.ScheduleOnTime),
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = userID
                    });
                }
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

        public bool UpdateNotificationSchedule(List<DTO_NotificationSchedule> notificationSchedules)
        {
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                foreach (var m in notificationSchedules)
                {
                    if (string.IsNullOrEmpty(m.RowId))
                    {
                        this.unitOfWork.NotificationScheduleRepository.Add(new NotificationSchedule
                        {
                            BeforeDays = Convert.ToInt16(m.BeforeDays),
                            CreatedAt = DateTime.Now,
                            CreatedBy = userID,
                            NotificationEnumId = Convert.ToInt32(m.NotificationTypeSystem.RowId),
                            RowId = System.Guid.NewGuid().ToString(),
                            ScheduleOnTime = DateTimeHelper.ConvertTimeStringToDate(m.ScheduleOnTime == null ? System.DateTime.Now.ToString("h:mm tt") : m.ScheduleOnTime),
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = userID
                        });
                    }
                    else
                    {
                        var dbNotificationSchedule = this.unitOfWork.NotificationScheduleRepository.FindFirst(c => c.RowId == m.RowId);

                        dbNotificationSchedule.UpdatedBy = userID;
                        dbNotificationSchedule.UpdatedAt = System.DateTime.Now;
                        dbNotificationSchedule.BeforeDays = Convert.ToInt16(m.BeforeDays);
                        dbNotificationSchedule.NotificationEnumId = Convert.ToInt32(m.NotificationTypeSystem.RowId);
                        dbNotificationSchedule.ScheduleOnTime = DateTimeHelper.ConvertTimeStringToDate(m.ScheduleOnTime);
                        this.unitOfWork.NotificationScheduleRepository.Update(dbNotificationSchedule);
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
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public PagedList<DTO_EmailTemplateList> GetEmailTemplates(DTO_FilterAndPaging filterAndPaging)
        {
            var emailTemplates = this.unitOfWork.EmailTemplateMasterRepository.GetAllWithInclude(includeProperties: "EmailNotificationSystem");
            var pagedList = PagedList<EmailTemplateMaster>.ToPagedList(emailTemplates, filterAndPaging.PageParameter.PageNumber,filterAndPaging.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_EmailTemplateList>>(pagedList);
            return new PagedList<DTO_EmailTemplateList>(response, pagedList.TotalCount, pagedList.CurrentPage, pagedList.PageSize);
        }

        public DTO_EmailTemplateMaster ReteriveEmailTemplate(string templateRowId)
        {
            var emailTemplateMaster = this.unitOfWork.EmailTemplateMasterRepository.FindFirst(c => c.RowId == templateRowId, includeProperties: "EmailNotificationSystem,EmailTemplateMasterScheduleDetails");
            var respone = this.mapper.Map<DTO_EmailTemplateMaster>(emailTemplateMaster);
            return respone;
        }

        public bool TestEmail(string templateRowId, DTO_TestEmailTemplate testEmailTemplate)
        {
            try
            {
                var emailTemplate = this.unitOfWork.EmailTemplateMasterRepository.FindFirst(c => c.RowId == templateRowId);
                var dtoEmailTempateVariable = new DTO_EmailTemplateVariable { 
                    CompanyInformation = this.mapper.Map<DTO_CompanyInformation>( this.unitOfWork.CompanyInfoRepository.GetAll().FirstOrDefault()),
                    Customer = this.mapper.Map<DTO_Customer>( this.unitOfWork.CustomerRepository.FindFirst(c=>c.RowId == testEmailTemplate.TestCustomerRowId))
                };
                string formattedTemplate = this._templateService.GetFormattedTemplate(dtoEmailTempateVariable, emailTemplate.HtmlEmailTemplate).Result;
                Task.Run(() => {
                    this.emailService.SendEmail(new DTO_SendEmail
                    {
                        EmailBody = formattedTemplate,
                        EmailRecipient = testEmailTemplate.TestEmail,
                        EmailSubject = emailTemplate.EmailSubject,
                        EmailCCRecipients = null
                    });
                }); 
                return true;
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException { 
                    Message = ex.Message,
                    SystemException = ex,
                    StatusCode = (Int32)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
