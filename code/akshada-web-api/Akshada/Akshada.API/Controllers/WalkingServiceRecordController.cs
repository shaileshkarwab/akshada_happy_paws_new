using Akshada.API.AuthFilter;
using Akshada.API.Extensions;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class WalkingServiceRecordController : BaseController
    {
        private readonly IWalkingRecordService walkingRecordService;
        public WalkingServiceRecordController(IWalkingRecordService walkingRecordService) { 
            this.walkingRecordService = walkingRecordService;
        }

        [HttpPost("get-all")]
        public IActionResult GetAll([FromBody]DTO_FilterAndPaging filterAndPaging) {
            var response = this.walkingRecordService.GetAllRecords(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpGet("get-by-id/{rowId}")]
        public IActionResult GetById([FromRoute]string rowId)
        {
            var response = this.walkingRecordService.GetById(rowId);
            return SuccessResponse(response);
        }

        [HttpPost("customer/{customerRowId}/pet/{petRowId}")]
        public IActionResult SaveWalkingServiceRecord([FromRoute]string customerRowId, [FromRoute]string petRowId,  [FromBody] DTO_WalkingServiceRecord walkingRecord)
        {
            var response = this.walkingRecordService.SaveWalkingServiceRecord(customerRowId, petRowId,walkingRecord);
            return SuccessResponse(response);
        }

        [HttpDelete("delete-by-id/walkingService/{rowId}")]
        public IActionResult DeleteWalkingServiceRecord([FromRoute] string rowId)
        {
            var response = this.walkingRecordService.DeleteWalkingServiceRecord(rowId);
            return SuccessResponse(response);
        }
    }
}
