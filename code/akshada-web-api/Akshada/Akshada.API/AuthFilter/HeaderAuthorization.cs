using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Akshada.API.AuthFilter
{
    public class HeaderAuthorization : IActionFilter
    {
        private readonly IConfiguration configuration;
        public HeaderAuthorization(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //getting the header keys
            var xAPIKey = context.HttpContext.Request.Headers["X-API-KEY"].ToString();
            var xSource = context.HttpContext.Request.Headers["X-SOURCE"].ToString();

            if(string.IsNullOrEmpty(xAPIKey))
            {
                context.Result = new BadRequestObjectResult("The API Key is missing");
            }

            if (string.IsNullOrEmpty(xSource))
            {
                context.Result = new BadRequestObjectResult("The Source Key is missing");
            }

            var apiKey =  this.configuration.GetSection("GoogleFormSubmission:X-API-KEY").Value.ToString();
            var source = this.configuration.GetSection("GoogleFormSubmission:X-SOURCE").Value.ToString();

            if(!string.Equals(xAPIKey,apiKey,StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult("The API Key is missing");
            }

            if (!string.Equals(xSource, source, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult("The Source Key is missing");
            }
        }
    }
}
