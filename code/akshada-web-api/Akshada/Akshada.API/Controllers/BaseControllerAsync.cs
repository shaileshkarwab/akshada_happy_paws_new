using Akshada.API.models;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    public class BaseControllerAsync : ControllerBase
    {
        protected async Task<IActionResult> SuccessResponse<T>(T data, string? message = null)
        {
            var response = new APIResponse<T>(StatusCodes.Status200OK, data, message ?? "Success");
            return await Task.FromResult( Ok(response));
        }

        protected async Task<IActionResult> ErrorResponse(string message, int statusCode = StatusCodes.Status400BadRequest)
        {
            var response = new APIResponse<object>(statusCode, null, message);
            return await Task.FromResult( StatusCode(statusCode, response));
        }
    }
}
