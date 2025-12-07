using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface INotificationScheduleService
    {
        bool SaveNotificationSchedule(List<DTO_NotificationSchedule> notificationSchedules);

        List<DTO_LookUp> GetNotificationList();

        List<DTO_NotificationSchedule> GetNotificationList(string systemParamRowId);

        bool UpdateNotificationSchedule(List<DTO_NotificationSchedule> notificationSchedules);

        bool DeleteNotificationList(string systemParamRowId);

        bool DeleteNotificationByID(string notificationRowId);

        bool SaveEmailTemplate(DTO_EmailTemplateMaster emailTemplateMaster);

        PagedList<DTO_EmailTemplateList> GetEmailTemplates(DTO_FilterAndPaging filterAndPaging);

        DTO_EmailTemplateMaster ReteriveEmailTemplate(string templateRowId);

        bool TestEmail(string templateRowId, DTO_TestEmailTemplate testEmailTemplate);
    }
}
