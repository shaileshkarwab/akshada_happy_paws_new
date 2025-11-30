using Akshada.API.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult SuccessResponse<T>(T data, string ? message = null) {
            var response = new APIResponse<T>(StatusCodes.Status200OK, data, message ?? "Success");
            return Ok(response);
        }

        protected IActionResult ErrorResponse(string message, int statusCode = StatusCodes.Status400BadRequest)
        {
            var response = new APIResponse<object>(statusCode, null, message);
            return StatusCode(statusCode, response);
        }
    }
}
