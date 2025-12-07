using Akshada.API.AuthFilter;
using Akshada.API.Extensions;
using Akshada.DTO.Enums;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using static Akshada.DTO.Enums.EnumHelper;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class NotificationScheduleController : BaseController
    {
        private readonly INotificationScheduleService notificationScheduleService;
        public NotificationScheduleController(INotificationScheduleService notificationScheduleService)
        {
            this.notificationScheduleService = notificationScheduleService;
        }


        [HttpPost]
        public IActionResult SaveNotificationSchedule([FromBody] List<DTO_NotificationSchedule> notificationSchedules)
        {
            var response = this.notificationScheduleService.SaveNotificationSchedule(notificationSchedules);
            return SuccessResponse(response);
        }


        [HttpGet]
        public IActionResult GetNotificationList()
        {
            var response = this.notificationScheduleService.GetNotificationList();
            return SuccessResponse(response);
        }

        [HttpGet("{systemParamRowId}")]
        public IActionResult GetNotificationList([FromRoute] string systemParamRowId)
        {
            var response = this.notificationScheduleService.GetNotificationList(systemParamRowId);
            return SuccessResponse(response);
        }

        [HttpPut]
        public IActionResult UpdateNotificationSchedule([FromBody] List<DTO_NotificationSchedule> notificationSchedules)
        {
            var response = this.notificationScheduleService.UpdateNotificationSchedule(notificationSchedules);
            return SuccessResponse(response);
        }

        [HttpDelete("{systemParamRowId}")]
        public IActionResult DeleteNotificationList([FromRoute] string systemParamRowId)
        {
            var response = this.notificationScheduleService.DeleteNotificationList(systemParamRowId);
            return SuccessResponse(response);
        }

        [HttpDelete("notificationRowId/{notificationRowId}")]
        public IActionResult DeleteNotificationByID([FromRoute] string notificationRowId)
        {
            var response = this.notificationScheduleService.DeleteNotificationByID(notificationRowId);
            return SuccessResponse(response);
        }

        [HttpGet("get-notification-list-from-enum")]
        public IActionResult GetNotificationListFromEnum()
        {
            var response = EnumHelper.EnumToJson<NotificationEnum>();
            return SuccessResponse(response.EnumNamesValues);
        }

        [HttpPost("save-email-template")]
        public IActionResult SaveEmailTemplate([FromBody] DTO_EmailTemplateMaster emailTemplateMaster)
        {
            var response = this.notificationScheduleService.SaveEmailTemplate(emailTemplateMaster);
            return SuccessResponse(true);
        }

        [HttpGet("get-attributes")]
        public IActionResult GetAttributes()
        {
            var response = GetAllPublicMembers(typeof(DTO_EmailTemplateVariable), false);
            var op = response.Select(m => new EnumNameValue
            {
                Description = m.Name
            }).ToList();

            List<EnumNameValue> members = new List<EnumNameValue>();
            foreach (var m in op)
            {
                string className = $"Akshada.DTO.Models.DTO_{m.Description}, Akshada.DTO";
                Type type = Type.GetType(className);
                var responseClassMembers = GetAllPublicMembers(type, true);
                members.AddRange(responseClassMembers.Select(c => new EnumNameValue
                {
                    Description = $"{m.Description}.{c.Name}"
                }).ToList());

            }

            return SuccessResponse(members);
        }

        List<MemberInfo> GetAllPublicMembers(Type type, bool onlyStrings)
        {
            var result = new List<MemberInfo>();

            // Fields
            result.AddRange(type.GetFields(BindingFlags.Public | BindingFlags.Instance));

            // Properties
            var stringProps = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => !onlyStrings || p.PropertyType == typeof(string))
            .ToList();
            result.AddRange(stringProps);

            // Nested types
            foreach (var nested in type.GetNestedTypes(BindingFlags.Public))
            {
                result.AddRange(GetAllPublicMembers(nested, onlyStrings));
            }

            return result;
        }



        [HttpPost("email-template")]
        public IActionResult GetEmailTemplates([FromBody] DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.notificationScheduleService.GetEmailTemplates(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }


        [HttpGet("email-template/templateRowId/{templateRowId}")]
        public IActionResult ReteriveEmailTemplate([FromRoute] string templateRowId)
        {
            var response = this.notificationScheduleService.ReteriveEmailTemplate(templateRowId);
            return SuccessResponse(response);
        }

        [HttpPost("test-email/templateRowId/{templateRowId}")]
        public IActionResult TestEmail([FromRoute] string templateRowId, [FromBody]DTO_TestEmailTemplate testEmailTemplate)
        {
            var response = this.notificationScheduleService.TestEmail(templateRowId, testEmailTemplate);
            return SuccessResponse(response);
        }
    }
}
