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
        public NotificationScheduleService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public bool DeleteNotificationByID(string notificationRowId)
        {
            try
            {
                var dbNotification = this.unitOfWork.NotificationScheduleRepository.FindFirst(c=>c.RowId == notificationRowId);
                if (dbNotification == null)
                {
                    throw new Exception("Failed to get the details for the notification details");
                }
                this.unitOfWork.NotificationScheduleRepository.Remove(dbNotification);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex) {
                throw new DTO_SystemException {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public bool DeleteNotificationList(string systemParamRowId)
        {
            try
            {
                var dbNotificationSchedule = this.unitOfWork.NotificationScheduleRepository.GetAllWithInclude(c => c.NotificationEnumId == Convert.ToInt32( systemParamRowId));
                this.unitOfWork.NotificationScheduleRepository.Remove(dbNotificationSchedule.AsEnumerable());
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex) {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
        }

        public List<DTO_LookUp> GetNotificationList()
        {
            var dbNotification = this.unitOfWork.SystemParamRepository.GetAllWithInclude(c=>c.EnumId == (Int32)SystemParameterEnum.TypeOfHolidaysAndSpecialDays);
            var dtoNotificationList = this.mapper.Map<List<DTO_LookUp>>(dbNotification);
            return dtoNotificationList;
        }

        public List<DTO_NotificationSchedule> GetNotificationList(string systemParamRowId)
        {
            try
            {
                var dbNotificatons = this.unitOfWork.NotificationScheduleRepository.Find(c => c.NotificationEnumId == Convert.ToInt32( systemParamRowId));
                var response = this.mapper.Map<List<DTO_NotificationSchedule>>(dbNotificatons);
                return response;
            }
            catch (Exception ex) {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public bool SaveNotificationSchedule(List<DTO_NotificationSchedule> notificationSchedules)
        {
            
            try
            {
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                foreach (var m in notificationSchedules) {
                    var dbNotificationTypeSystem = this.unitOfWork.SystemParamRepository.FindFirst(r => r.RowId == m.NotificationTypeSystem.RowId);
                    if (dbNotificationTypeSystem == null) {
                        throw new Exception("Failed to get the details for the selected schedule type");
                    }
                    this.unitOfWork.NotificationScheduleRepository.Add(new NotificationSchedule { 
                        BeforeDays = Convert.ToInt16( m.BeforeDays),
                        CreatedAt = DateTime.Now,
                        CreatedBy = userID ,
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
            catch (Exception ex) {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
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
                            NotificationEnumId = Convert.ToInt32( m.NotificationTypeSystem.RowId),
                            RowId = System.Guid.NewGuid().ToString(),
                            ScheduleOnTime = DateTimeHelper.ConvertTimeStringToDate(m.ScheduleOnTime == null ? System.DateTime.Now.ToString("h:mm tt") : m.ScheduleOnTime),
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = userID
                        });
                    }
                    else
                    {
                        var dbNotificationSchedule = this.unitOfWork.NotificationScheduleRepository.FindFirst(c=>c.RowId == m.RowId);
                        
                        dbNotificationSchedule.UpdatedBy = userID;
                        dbNotificationSchedule.UpdatedAt = System.DateTime.Now;
                        dbNotificationSchedule.BeforeDays = Convert.ToInt16(m.BeforeDays);
                        dbNotificationSchedule.NotificationEnumId = Convert.ToInt32( m.NotificationTypeSystem.RowId);
                        dbNotificationSchedule.ScheduleOnTime = DateTimeHelper.ConvertTimeStringToDate(m.ScheduleOnTime);
                        this.unitOfWork.NotificationScheduleRepository.Update(dbNotificationSchedule);
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
