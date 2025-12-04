using Akshada.API.AuthFilter;
using Akshada.API.Extensions;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class CustomerController : BaseController
    {
        public readonly ICustomerService CustomerService;
        public CustomerController(ICustomerService CustomerService) {
            this.CustomerService = CustomerService;
        }

        [HttpGet("get-unprocessed-customers")]
        public async Task<IActionResult> GetUnprocessedCustomers() {
            var response = await this.CustomerService.ProcessDataFromImportData();
            return SuccessResponse(response);
        }

        [HttpGet("reterive-customer-pet-information/{petRowId}")]
        public IActionResult ReteriveCustomerPetInformation([FromRoute]string petRowId) {
            var response = this.CustomerService.ReteriveCustomerPetInformation(petRowId);
            return SuccessResponse(response);
        }

        [HttpPut("update-customer-pet-information/customer-id/{customerRowId}/pet-id/{petRowId}")]
        public IActionResult UpdateCustomerPetInformation([FromRoute]string customerRowId, [FromRoute]string petRowId, [FromBody]DTO_Customer customer)
        {
            var response = this.CustomerService.UpdateCustomerPetInformation(customerRowId, petRowId, customer);
            return SuccessResponse(response);
        }


        [HttpPost("save-customer-pet-information")]
        public IActionResult SaveCustomerPetInformation([FromBody] DTO_Customer customer)
        {
            var response = this.CustomerService.SaveCustomerPetInformation(customer);
            return SuccessResponse(response);
        }

        [HttpGet("get-customers")]
        public IActionResult GetCustomers([FromQuery]string? customerName)
        {
            var response = this.CustomerService.GetCustomers(customerName);
            return SuccessResponse(response);
        }

        [HttpGet("reterive-customer/{customerRowId}")]
        public IActionResult ReteriveCustomer([FromRoute]string customerRowId)
        {
            var response = this.CustomerService.ReteriveCustomer(customerRowId);
            return SuccessResponse(response);
        }

        [HttpPost("add-pet-to-selected-customer/{customerRowId}")]
        public IActionResult AddPetToSelectedCustomer([FromRoute] string customerRowId, [FromBody]DTO_Customer customer)
        {
            var response = this.CustomerService.AddPetToSelectedCustomer(customerRowId, customer);
            return SuccessResponse(response);
        }

        [HttpPost("get-pets")]
        public IActionResult GetPets([FromQuery]string? customerRowId, [FromQuery]bool? includeAll, [FromBody]DTO_FilterAndPaging filterAndPaging) 
        {
            var response = this.CustomerService.GetPets(customerRowId, includeAll, filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }


        [HttpPut("update-customer-information/{customerRowId}")]
        public IActionResult UpdateCustomerInformation([FromRoute]string customerRowId, [FromBody]DTO_Customer customer)
        {
            var response = this.CustomerService.UpdateCustomerInformation(customerRowId, customer);
            return SuccessResponse(response);
        }

        [HttpDelete("delete-customer/{customerRowId}")]
        public IActionResult DeleteCustomer([FromRoute] string customerRowId)
        {
            var response = this.CustomerService.DeleteCustomer(customerRowId);
            return SuccessResponse(response);
        }

        [HttpGet("get-pet-for-customer-and-pet-row-id/customerRowId/{customerRowId}/petRowId/{petRowId}")]
        public IActionResult GetPetForCustomerAndPetRowId([FromRoute]string customerRowId, [FromRoute] string petRowId)
        {
            var response = this.CustomerService.GetPetByCustomerAndPetID(customerRowId, petRowId);
            return SuccessResponse(response);
        }

        [HttpPost("get-customers")]
        public IActionResult GetCustomerList([FromBody]DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.CustomerService.GetCustomerList(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }


        [HttpDelete("delete-customer-pet/customerRowId/{customerRowId}/petRowId/{petRowId}")]
        public IActionResult DeleteCustomerPet([FromRoute] string customerRowId, [FromRoute] string petRowId)
        {
            var response = this.CustomerService.DeleteCustomerPet(customerRowId, petRowId);
            return SuccessResponse(response);
        }

    }
}
