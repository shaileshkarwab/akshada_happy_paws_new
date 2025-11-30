using Akshada.API.AuthFilter;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(HeaderAuthorization))]
    public class FormSubmitController : BaseController
    {
        private readonly IWebsiteDataService websiteDataService;
        public FormSubmitController(IWebsiteDataService websiteDataService) { 
            this.websiteDataService = websiteDataService;
        }

        [HttpPost("read-service-form-submit")]
        public async Task<IActionResult> GetDataFromGmailFormSubmit([FromBody] DTO_ReciveFormSubmission reciveFormSubmission)
        {
           await Task.Run(() => {
                this.websiteDataService.CaptureServiecRequest(reciveFormSubmission);
            });

            return Accepted();
        }

        [HttpPost("read-google-form-submission")]
        public async Task<IActionResult> ReadGoogleFormSubmission([FromBody] DTO_GoogleFormSubmission reciveFormSubmission)
        {
            await this.websiteDataService.SaveGoogleFormSubmissionRawData(Newtonsoft.Json.JsonConvert.SerializeObject(reciveFormSubmission));
            return Accepted();

        }

    }
}
