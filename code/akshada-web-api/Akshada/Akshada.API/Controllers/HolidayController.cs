using Akshada.API.AuthFilter;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class HolidayController : BaseController
    {
        private readonly IHolidayService holidayService;
        public HolidayController(IHolidayService holidayService) {
            this.holidayService = holidayService;
        }

        [HttpGet("get-all/{year}")]
        public IActionResult GetAllHolidaysForYear([FromRoute]int year) 
        {
            var response = this.holidayService.GetAllHolidaysForYear(year);
            return SuccessResponse(response);
        }

        [HttpGet("get-detail-by-date/{selectedDate}")]
        public IActionResult GetDetailByDate([FromRoute] string selectedDate)
        {
            var response = this.holidayService.GetDetailByDate(selectedDate);
            return SuccessResponse(response);
        }

        [HttpDelete("{rowId}")]
        public IActionResult DeleteHoliday([FromRoute] string rowId)
        {
            var response = this.holidayService.DeleteHoliday(rowId);
            return SuccessResponse(response);
        }

        [HttpPut("{rowId}")]
        public IActionResult UpdateHoliday([FromRoute] string rowId, [FromBody]DTO_HolidaySchedule holidaySchedule)
        {
            var response = this.holidayService.UpdateHoliday(rowId, holidaySchedule);
            return SuccessResponse(response);
        }

        [HttpPost]
        public IActionResult SaveHoliday([FromBody] DTO_HolidaySchedule holidaySchedule)
        {
            var response = this.holidayService.SaveHoliday(holidaySchedule);
            return SuccessResponse(response);
        }
    }
}
