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
    public class OtherServiceRequestController : BaseController
    {
        private readonly IOtherServiceRequestService otherServiceRequestService;
        public OtherServiceRequestController(IOtherServiceRequestService otherServiceRequestService) { 
            this.otherServiceRequestService = otherServiceRequestService;
        }

        [HttpPost("get-all")]
        public IActionResult GetAll([FromBody]DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.otherServiceRequestService.GetAll(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpPost("other-service-request")]
        public IActionResult OtherServiceRequest([FromBody] DTO_OtherServiceRequest OtherServiceRequest)
        {
            var response = this.otherServiceRequestService.AddOtherServiceRequest(OtherServiceRequest);
            return SuccessResponse(response);
        }

        [HttpDelete("other-service-request/otherServiceRequestID/{rowID}")]
        public IActionResult OtherServiceRequest([FromRoute] string rowID)
        {
            var response = this.otherServiceRequestService.DeleteOtherServiceRequest(rowID);
            return SuccessResponse(response);
        }

        [HttpPost("other-service-request-assign-to-user/otherServiceRequestID/{rowID}")]
        public IActionResult AssignUserToOtherServiceRequest([FromRoute] string rowID, [FromBody] DTO_AssignOtherServiceRequestUser assignOtherServiceRequestUser)
        {
            var response = this.otherServiceRequestService.AssignUserToOtherServiceRequest(rowID, assignOtherServiceRequestUser);
            return SuccessResponse(response);
        }

        [HttpDelete("other-service-request-assign-to-user/otherServiceRequestID/{rowID}")]
        public IActionResult DeleteUserToOtherServiceRequest([FromRoute] string rowID)
        {
            var response = this.otherServiceRequestService.DeleteUserToOtherServiceRequest(rowID);
            return SuccessResponse(response);
        }

        [HttpGet("other-service-request/otherServiceRequestID/{rowID}")]
        public IActionResult GetOtherServiceRequestById([FromRoute] string rowID)
        {
            var response = this.otherServiceRequestService.GetOtherServiceRequestById(rowID);
            return SuccessResponse(response);
        }

        [HttpGet("other-service-request-assigned-user/otherServiceRequestID/{rowID}/assignedRequest/{assignedRequestRowId}")]
        public IActionResult OtherServiceRequestAssignedUser([FromRoute] string rowID, [FromRoute]string assignedRequestRowId)
        {
            var response = this.otherServiceRequestService.OtherServiceRequestAssignedUser(rowID, assignedRequestRowId);
            return SuccessResponse(response);
        }

        [HttpPut("other-service-request-assigned-user/otherServiceRequestID/{rowID}/assignedRequest/{assignedRequestRowId}")]
        public IActionResult UpdateServiceRequestAssignedUser([FromRoute] string rowID, [FromRoute] string assignedRequestRowId, [FromBody]DTO_AssignOtherServiceRequestUser updateEntity)
        {
            var response = this.otherServiceRequestService.UpdateServiceRequestAssignedUser(rowID, assignedRequestRowId,updateEntity);
            return SuccessResponse(response);
        }
    }
}
