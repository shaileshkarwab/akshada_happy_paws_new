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
    public class ServiceRateController : BaseController
    {
        private readonly ISrvRateService srvRateService;
        public ServiceRateController(ISrvRateService srvRateService) {
            this.srvRateService = srvRateService;
        }

        [HttpPost("get-service-rate-list")]
        public IActionResult GetServiceRateList([FromBody] DTO_FilterAndPaging filterAndPaging) {
            var response = this.srvRateService.GetServiceRateList(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpGet("reterive-service-rate/{rowID}")]
        public IActionResult ReteriveServiceRate([FromRoute]string rowID)
        {
            var response = this.srvRateService.ReteriveServiceRate(rowID);
            return SuccessResponse(response);
        }

        [HttpPost("save-service-rates")]
        public IActionResult SaveServiceRate([FromBody]DTO_ServiceRateMaster saveEntity) {
            var response = this.srvRateService.SaveServiceRate(saveEntity);
            return SuccessResponse(response);
        }

        [HttpDelete("serviceRateRowID/{rowID}")]
        public IActionResult DeleteServiceRate([FromRoute] string rowID)
        {
            var response = this.srvRateService.DeleteServiceRate(rowID);
            return SuccessResponse(response);
        }

        [HttpGet("get-walking-service-rate/service/{serviceId}/location/{locationId}/date/{date}")]
        public IActionResult GetWalkingServiceRate([FromRoute]string serviceId, [FromRoute]string locationId, [FromRoute]string date) 
        {
            var response = this.srvRateService.GetWalkingServiceRate(serviceId, locationId, date);
            return SuccessResponse(response);
        }
    }
}
