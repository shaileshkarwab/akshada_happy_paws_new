using Akshada.API.AuthFilter;
using Akshada.API.Extensions;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class SystemParameterController : BaseController
    {
        private readonly ISystemParameterService systemParameterService;
        public SystemParameterController(ISystemParameterService systemParameterService) {
            this.systemParameterService = systemParameterService;
        }

        [HttpPost("get-system-parameter")]
        public IActionResult GetSystemParameter([FromBody]DTO_FilterAndPaging filterAndPaging) {
            var response = this.systemParameterService.GetSystemParameter(filterAndPaging);
            this.AddPaginationHeader(response);
            return SuccessResponse(response);
        }

        [HttpGet("get-system-parameter-by-id/{systemParamRowID}")]
        public IActionResult GetSystemParameterById([FromRoute]string systemParamRowID)
        {
            var response = this.systemParameterService.GetSystemParameterById(systemParamRowID);
            return SuccessResponse(response);
        }

        [HttpPost("save-system-parameter")]
        public IActionResult SaveSystemParameter([FromBody]DTO_SystemParameter systemParameter) {
            var result = this.systemParameterService.SaveSystemParameter(systemParameter);
            return SuccessResponse(true);
        }

        [HttpGet("get-system-parameter-details-by-enum-id/{enumID}")]
        public IActionResult GetSystemParameterDetailsByEnumId([FromRoute]Int32 enumID)
        {
            var response = this.systemParameterService.GetSystemParameterDetailsByEnumId(enumID);
            return SuccessResponse(response);
        }

        [HttpPost("get-system-parameter-details-by-enum-ids")]
        public IActionResult GetSystemParameterDetailsByEnumIds([FromBody] List<Int32> enumIds)
        {
            var response = this.systemParameterService.GetSystemParameterDetailsByEnumIds(enumIds);
            return SuccessResponse(response);
        }


        [HttpGet("get-system-parameter-data-details-by-enum-id/{enumID}")]
        public IActionResult GetSystemParameterDataDetailsByEnumId([FromRoute] Int32 enumID)
        {
            var response = this.systemParameterService.GetSystemParameterDataDetailsByEnumId(enumID);
            return SuccessResponse(response);
        }


        [HttpDelete("sysParameterRowId/{sysParameterRowId}")]
        public IActionResult DeleteSystemParameter([FromRoute] string sysParameterRowId)
        {
            var response = this.systemParameterService.DeleteSystemParameter(sysParameterRowId);
            return SuccessResponse(response);
        }
    }
}
