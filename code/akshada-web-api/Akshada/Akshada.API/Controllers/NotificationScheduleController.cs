using Akshada.API.AuthFilter;
using Akshada.DTO.Enums;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class NotificationScheduleController : BaseController
    {
        private readonly INotificationScheduleService notificationScheduleService;
        public NotificationScheduleController(INotificationScheduleService notificationScheduleService) {
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
        public IActionResult GetNotificationList([FromRoute]string systemParamRowId)
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
        public IActionResult DeleteNotificationByID([FromRoute]string notificationRowId)
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
    }
}
