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
    public class OtherServiceRateController : BaseController
    {
        private readonly IOtherServiceRate otherService;
        public OtherServiceRateController(IOtherServiceRate otherService) {
            this.otherService = otherService;
        }

        [HttpPost("other-service-rate")]
        public IActionResult SaveOtherServiceRate([FromBody]DTO_OtherServiceRate otherServiceRate) {
            var response = this.otherService.SaveOtherServiceRate(otherServiceRate); ;
            return SuccessResponse(response);
        }

        [HttpPost("other-service-rate/get-all")]
        public IActionResult GetAllOtherServiceDate([FromBody] DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.otherService.GetAllOtherServiceDate(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpGet("other-service-rate/get-by-id/{otherServiceRateRowId}")]
        public IActionResult GetByIdOtherServiceRate([FromRoute] string otherServiceRateRowId)
        {
            var response = this.otherService.GetByIdOtherServiceRate(otherServiceRateRowId);
            return SuccessResponse(response);
        }

        [HttpDelete("other-service-rate/delete-by-id/{otherServiceRateRowId}")]
        public IActionResult DeleteByIdOtherServiceRate([FromRoute] string otherServiceRateRowId)
        {
            var response = this.otherService.DeleteByIdOtherServiceRate(otherServiceRateRowId);
            return SuccessResponse(response);
        }

        [HttpPut("other-service-rate/other-service-row-id/{otherServiceRowId}")]
        public IActionResult UpdateOtherServiceRate([FromRoute]string otherServiceRowId, [FromBody] DTO_OtherServiceRate updateEntity)
        {
            var response = this.otherService.UpdateOtherServiceRate(otherServiceRowId,updateEntity); ;
            return SuccessResponse(response);
        }
    }
}
