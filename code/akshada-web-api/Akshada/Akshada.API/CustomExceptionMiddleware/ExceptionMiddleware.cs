using Akshada.DTO.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Akshada.API.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> _logger) { 
            this._next = next;
            this._logger = _logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var errorDetails = new ErrorDetails();
            if (exception.GetType().Name.Equals("DTO_SystemException"))
            {
                string excpetionMessage = ((DTO_SystemException)exception).SystemException.Message;
                if(((DTO_SystemException)exception).SystemException.InnerException != null)
                {
                    excpetionMessage = string.Format("{0}-Inner Exception -- {1}", excpetionMessage , ((DTO_SystemException)exception).SystemException.InnerException.Message);
                }
                errorDetails.StatusCode = ((DTO_SystemException)exception).StatusCode;
                errorDetails.Message = excpetionMessage;
                context.Response.StatusCode = ((DTO_SystemException)exception).StatusCode;
                this._logger.LogError(excpetionMessage);
            }
            else {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetails.StatusCode = context.Response.StatusCode;
                string innerExceptionMessage = string.Empty;
                if(exception.InnerException != null)
                {
                    innerExceptionMessage = string.Format("{0}-{1}", "Inner Exception", exception.InnerException.Message);
                }
                errorDetails.Message = string.Format("Message {0} -- Details {1} -- {2}", "Internal Server Error from the custom middleware.", exception.Message, innerExceptionMessage);
                this._logger.LogError(errorDetails.Message);
            }
            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
