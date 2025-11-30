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
    public class OtherServiceController : BaseController
    {
        private readonly IOtherSrvService otherSrvService;
        public OtherServiceController(IOtherSrvService otherSrvService) {
            this.otherSrvService = otherSrvService;
        }

        [HttpPost("get-all")]
        public IActionResult GetAll([FromBody]DTO_FilterAndPaging filterAndPaging)
        {
            var response = otherSrvService.GetAll(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpPost("save-web-service-request/webServiceRequest/{webServiceRequestRowId}")]
        public async Task<IActionResult> SaveWebServiceRequest([FromRoute]string webServiceRequestRowId, [FromBody]DTO_WebsiteServiceProcess saveEntity)
        {
            var response = await Task.FromResult(true);
            return SuccessResponse(response);
        }
    }
}
