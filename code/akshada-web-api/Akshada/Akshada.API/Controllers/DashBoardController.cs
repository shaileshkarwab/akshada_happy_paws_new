using Akshada.API.AuthFilter;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class DashBoardController : BaseControllerAsync
    {
        private readonly IDashBoardService dashBoardService;
        public DashBoardController(IDashBoardService dashBoardService)
        {
            this.dashBoardService = dashBoardService;
        }

        [HttpGet("get-dashboard-details/date/{date}")]
        public async Task<IActionResult> GetDashBoardDetails([FromRoute]string date)
        {
            var response = await this.dashBoardService.GetDashBoardCounts(date);
            return await SuccessResponse(response);
        }

        [HttpGet("get-web-site-data-service")]
        public async Task<IActionResult> GetWebSiteSataService()
        {
            var response = await this.dashBoardService.GetWebSiteSataService();
            return await SuccessResponse(response);
        }

        [HttpGet("get-google-form-submission-data")]
        public async Task<IActionResult> GetGoogleFormSubmissionData()
        {
            var response = await this.dashBoardService.GetGoogleFormSubmissionData();
            return await SuccessResponse(response);
        }

        [HttpGet("get-google-form-submission-data/{rowID}")]
        public async Task<IActionResult> ReteriveGoogleFormSubmissionData([FromRoute]string rowID)
        {
            var response = await this.dashBoardService.ReteriveGoogleFormSubmissionData(rowID);
            return await SuccessResponse(response);
        }
    }
}
