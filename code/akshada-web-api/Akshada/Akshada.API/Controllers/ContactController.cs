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
    public class ContactController : BaseController
    {
        private readonly IContactAndAddressService contactAndAddressService;
        public ContactController(IContactAndAddressService contactAndAddressService) {
            this.contactAndAddressService = contactAndAddressService;
        }

        [HttpPost]
        public IActionResult SaveContact([FromBody]DTO_ImportantContact importantContact) {
            var response = this.contactAndAddressService.SaveContact(importantContact);
            return SuccessResponse(response);
        }

        [HttpPost("get-all")]
        public IActionResult GetAll([FromBody] DTO_FilterAndPaging filterAndPaging)
        {
            var response = this.contactAndAddressService.GetAll(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpGet("reterive/customerRowId/{customerRowId}")]
        public IActionResult Reterive([FromRoute]string customerRowId)
        {
            var response = this.contactAndAddressService.Reterive(customerRowId);
            return SuccessResponse(response);
        }

        [HttpPut("{customerRowId}")]
        public IActionResult UpdateContactAndAddress([FromRoute] string customerRowId, [FromBody]DTO_ImportantContact updateEntity)
        {
            var response = this.contactAndAddressService.UpdateContactAndAddress(customerRowId, updateEntity);
            return SuccessResponse(response);
        }

        [HttpDelete("{customerRowId}")]
        public IActionResult DeleteContactAndAddress([FromRoute] string customerRowId)
        {
            var response = this.contactAndAddressService.DeleteContactAndAddress(customerRowId);
            return SuccessResponse(response);
        }
    }
}
