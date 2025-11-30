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
    public class ServiceController : BaseController
    {
        private readonly ISrvRequestService srvRequestService;
        private readonly IWebsiteDataService websiteDataService;
        public ServiceController(ISrvRequestService srvRequestService, IWebsiteDataService websiteDataService = null)
        {
            this.srvRequestService = srvRequestService;
            this.websiteDataService = websiteDataService;   
        }

        [HttpGet("get-customer-service-requests/customer-id/{customerRowId}/pet-id/{petRowId}")]
        public IActionResult GetCustomerServiceRequests([FromRoute]string customerRowId, [FromRoute]string petRowId) 
        {
            var response = this.srvRequestService.GetCustomerServiceRequests(customerRowId, petRowId);
            return SuccessResponse(response);
        }

        [HttpGet("get-details-for-walking-service/customerRowId/{customerRowId}/petRowId/{petRowId}/serviceRequestRowId/{serviceRequestRowId}")]
        public IActionResult GetDetailsForWalkingService([FromRoute] string customerRowId, [FromRoute] string petRowId, [FromRoute] string serviceRequestRowId)
        {
            var response = this.srvRequestService.GetDetailsForWalkingService(customerRowId, petRowId, serviceRequestRowId);
            return SuccessResponse(response);
        }

        [HttpPut("update-customer-pet-walking-service-request/customerRowId/{customerRowId}/petRowId/{petRowId}/serviceRequestId/{serviceRequestId}")]
        public IActionResult UpdateCustomerPetWalkingServiceRequest([FromRoute]string customerRowId,string petRowId,string serviceRequestId, [FromBody]DTO_WalkingServiceRequest updateEntity)
        {
            var response = this.srvRequestService.UpdateCustomerPetWalkingServiceRequest(customerRowId, petRowId, serviceRequestId, updateEntity);
            return SuccessResponse(response);
        }

        [HttpPost("get-pet-service-details")]
        public IActionResult GetPetServiceDetails([FromBody] DTO_FilterAndPaging filterAndPagings)
        {
            var response = this.srvRequestService.GetPetServiceDetails(filterAndPagings);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpPost("get-walking-service-requests")]
        public IActionResult GetWalkingServiceRequests([FromBody] DTO_FilterAndPaging filterAndPagings) {
            var response = this.srvRequestService.GetWalkingServiceRequests(filterAndPagings);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpPost("assign-user-to-pet-walking-service-schedule/customerRowId/{customerRowId}/petRowId/{petRowId}/serviceRequestId/{serviceRequestId}")]
        public IActionResult AssignUserToPetWalkingServiceSchedule([FromRoute]string customerRowId, [FromRoute]string petRowId, [FromRoute]string serviceRequestId, [FromBody]List<DTO_WalkingServiceRequestDayScheduleAssignedToUser> walkingServiceRequestDayScheduleAssignedToUsers)
        {
            var response = this.srvRequestService.AssignUserToPetWalkingServiceSchedule(customerRowId, petRowId, serviceRequestId, walkingServiceRequestDayScheduleAssignedToUsers); ;
            return SuccessResponse(response);
        }

        [HttpPost("change_user_and_assign_new_time/customerRowId/{customerRowId}/petRowId/{petRowId}/serviceRequestId/{serviceRequestId}")]
        public IActionResult ChangeUserAndAssignNewTime([FromRoute] string customerRowId, [FromRoute] string petRowId, [FromRoute]string serviceRequestId,  [FromBody] DTO_ChangeTimeOrUserForRequest changeTimeOrUserForRequest)
        {
            var response = this.srvRequestService.ChangeUserAndAssignNewTime(customerRowId, petRowId, serviceRequestId, changeTimeOrUserForRequest);
            return SuccessResponse(response);
        }

        [HttpPut("change_user_and_assign_new_time/customerRowId/{customerRowId}/petRowId/{petRowId}/serviceRequestId/{serviceRequestId}/assignedRequestId/{assignedRequestId}")]
        public IActionResult UpdateChangeUserAndAssignNewTime([FromRoute] string customerRowId, [FromRoute] string petRowId, [FromRoute] string serviceRequestId, [FromRoute]string assignedRequestId,  [FromBody] DTO_ChangeTimeOrUserForRequest changeTimeOrUserForRequest)
        {
            var response = this.srvRequestService.UpdateChangeUserAndAssignNewTime(customerRowId, petRowId, serviceRequestId, assignedRequestId,changeTimeOrUserForRequest);
            return SuccessResponse(response);
        }

        [HttpDelete("change_user_and_assign_new_time/assignedRequestId/{assignedRequestId}")]
        public IActionResult DeleteChangeUserAndAssignNewTime([FromRoute] string assignedRequestId)
        {
            var response = this.srvRequestService.DeleteChangeUserAndAssignNewTime(assignedRequestId);
            return SuccessResponse(response);
        }

        [HttpPost("customer-pet-walking-service-request/customerRowId/{customerRowId}/petRowId/{petRowId}")]
        public IActionResult AddCustomerPetWalkingServiceRequest([FromRoute] string customerRowId, string petRowId, [FromBody] DTO_WalkingServiceRequest saveEntity)
        {
            var response = this.srvRequestService.AddCustomerPetWalkingServiceRequest(customerRowId, petRowId, saveEntity);
            return SuccessResponse(response);
        }

        [HttpDelete("customer-pet-walking-service-request/serviceRowId/{serviceRowId}")]
        public IActionResult DeleteCustomerPetWalkingServiceRequest([FromRoute] string serviceRowId)
        {
            var response = this.srvRequestService.DeleteCustomerPetWalkingServiceRequest(serviceRowId);
            return SuccessResponse(response);
        }


        [HttpPut("walking-service-request/walkingRequestRowId/{walkingRequestRowId}")]
        public IActionResult UpdateWalkingServiceRequest([FromRoute]string walkingRequestRowId, [FromBody]DTO_WalkingRecord walkingRecord)
        {
            var response = this.srvRequestService.UpdateWalkingServiceRequest(walkingRequestRowId, walkingRecord);
            return SuccessResponse(response);
        }

        [HttpGet("get-active-walking-service-slots-for-customer-id-pet-id/customerID/{customerID}/petId/{petId}/selectedDate/{selectedDate}")]
        public IActionResult GetActiveWalkingServiceSlotsForCustomerIdPetId([FromRoute]string customerID, [FromRoute]string petId, [FromRoute]string selectedDate) 
        {
            var response = this.srvRequestService.GetActiveWalkingServiceSlotsForCustomerIdPetId(customerID, petId, selectedDate);
            return SuccessResponse(response);
        }

        [HttpPost("walking-service-request")]
        public IActionResult AddWalkingServiceRequest([FromBody] DTO_WalkingRecord walkingRecord)
        {
            var response = this.srvRequestService.AddWalkingServiceRequest(walkingRecord);
            return SuccessResponse(response);
        }

        [HttpGet("read-service-form-submit/recordID/{recordID}")]
        public async Task<IActionResult> ReteriveWebServiceData([FromRoute] string recordID)
        {
            var data = await this.websiteDataService.ReteriveWebServiceData(recordID);
            return SuccessResponse(data);
        }

        [HttpPost("read-service-form-submit/recordID/{recordID}")]
        public async Task<IActionResult> SaveWebServiceData([FromRoute] string recordID, [FromBody] DTO_WebsiteServiceProcess websiteServiceProcess)
        {
            var data = await this.websiteDataService.SaveWebServiceData(recordID, websiteServiceProcess);
            return SuccessResponse(data);
        }

        [HttpPost("get-google-form-requests")]
        public IActionResult GetGoogleGormRequests([FromBody] DTO_FilterAndPaging filterAndPagings)
        {
            var response = this.srvRequestService.GetGoogleGormRequests(filterAndPagings);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpDelete("google-form-requests/requestRowId/{requestRowId}")]
        public IActionResult DeleteGoogleGormRequests([FromRoute] string requestRowId)
        {
            var response = this.srvRequestService.DeleteGoogleGormRequests(requestRowId);
            return SuccessResponse(response);
        }


        [HttpPost("get-google-service-requests")]
        public IActionResult GetGoogleServiceRequests([FromBody] DTO_FilterAndPaging filterAndPagings)
        {
            var response = this.srvRequestService.GetGoogleServiceRequests(filterAndPagings);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpDelete("delete-google-service-requests/requestId/{rowId}")]
        public IActionResult DeleteGoogleServiceRequests([FromRoute] string rowId)
        {
            var response = this.srvRequestService.DeleteGoogleServiceRequests(rowId);
            return SuccessResponse(response);
        }
    }
}
