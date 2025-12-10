using Akshada.API.models;
using Akshada.DTO.Models;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyUserController : BaseController
    {
        private readonly IUserVerificationService userVerificationService;
        private readonly ILogger<VerifyUserController> logger;
        public VerifyUserController(IUserVerificationService userVerificationService, ILogger<VerifyUserController> logger) { 
            this.userVerificationService = userVerificationService;
            this.logger = logger;
        }

        [HttpPost("user-verification")]
        public IActionResult UserVerification([FromBody] DTO_UserVerification userVerification) {

            this.logger.LogInformation("Verifying user" + Newtonsoft.Json.JsonConvert.SerializeObject(userVerification));
            var response = this.userVerificationService.VerifyUser(userVerification);
            if (response.UserVerified)
            {
                Response.Headers.Add("REFRESH_TOKEN", response.RefreshToken);
            }
            return SuccessResponse(response);
        }

        [HttpPost("user-verification-with-pin/{userRowId}")]
        public IActionResult UserVerificationWithPin([FromRoute]string userRowId, [FromBody] DTO_UserPin pin)
        {
            var response = this.userVerificationService.UserVerificationWithPin(userRowId,pin);
            return SuccessResponse(response);
        }
    }
}
