using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Akshada.API.AuthFilter
{
    public class BasicAuthorization : IActionFilter
    {
        private readonly IUserService userService;
        public BasicAuthorization(IUserService userService)
        {
            this.userService = userService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                var userName = context.HttpContext.User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                {
                    context.Result = new UnauthorizedObjectResult("User name cannot be blank or empty");
                }
                else
                {
                    int? checkUserNameExists = null;
                    checkUserNameExists = this.userService.GetUserID(userName);
                    if (checkUserNameExists.HasValue)
                    {
                        var controller = (ControllerBase)context.Controller;
                        controller.HttpContext.Items.Add("USER_ID", checkUserNameExists);
                    }
                    else
                    {
                        context.Result = new BadRequestObjectResult("Invalid user name or user is not existing");
                    }
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
