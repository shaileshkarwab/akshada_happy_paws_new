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
    public class OtherPetServiceController : BaseControllerAsync
    {
        private readonly IPetOtherSrvService _petOtherSrvService;
        public OtherPetServiceController(IPetOtherSrvService petOtherSrvService) {
            this._petOtherSrvService = petOtherSrvService;
        }

        [HttpPost("get-all-other-pet-services")]
        public async Task<IActionResult> GetOtherPetServices([FromBody]DTO_FilterAndPaging filterAndPaging)
        {
            var response = await _petOtherSrvService.GetOtherPetServices(filterAndPaging);
            this.AddPaginationHeader(response);
            return await SuccessResponse(response);
        }

        [HttpGet("reterive-other-pet-services/request/{otherServiceRequestId}/user/{otherServiceAssignedToRowId}")]
        public async Task<IActionResult> ReteriveOtherPetServices([FromRoute]string otherServiceRequestId, [FromRoute]string otherServiceAssignedToRowId)
        {
            var response = await _petOtherSrvService.ReteriveOtherPetServices(otherServiceRequestId, otherServiceAssignedToRowId);
            return await SuccessResponse(response);
        }

        [HttpPost("otherServiceRequest/{otherServiceRequestId}")]
        public async Task<IActionResult> SaveOtherPetServiceExecuted([FromRoute]string otherServiceRequestId, [FromBody] DTO_OtherServicesOfferedNew saveEntity)
        {
            var response = await _petOtherSrvService.SaveOtherPetServiceExecuted(otherServiceRequestId, saveEntity);
            return await SuccessResponse(response);
        }

        [HttpPut("otherServiceRequest/{otherServiceRequestId}/serviceOffered/{serviceOfferedRowId}")]
        public async Task<IActionResult> UpdateOtherPetServiceExecuted([FromRoute] string otherServiceRequestId, [FromRoute]string serviceOfferedRowId,  [FromBody] DTO_OtherServicesOfferedNew saveEntity)
        {
            var response = await _petOtherSrvService.UpdateOtherPetServiceExecuted(otherServiceRequestId, serviceOfferedRowId, saveEntity);
            return await SuccessResponse(response);
        }


        [HttpDelete("otherServiceRequest/{requestRowId}")]
        public async Task<IActionResult> DeleteOtherPetServiceExecuted([FromRoute] string requestRowId)
        {
            var response = await _petOtherSrvService.DeleteOtherPetServiceExecuted(requestRowId);
            return await SuccessResponse(response);
        }
    }
}
