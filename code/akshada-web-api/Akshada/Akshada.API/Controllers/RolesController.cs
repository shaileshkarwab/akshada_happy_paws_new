using Akshada.API.AuthFilter;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class RolesController : BaseController
    {
        private readonly IRolesService rolesService;
        public RolesController(IRolesService rolesService) {
            this.rolesService = rolesService;
        }
        [HttpGet("get-all-roles")]
        public IActionResult GetAllRoles()
        {
            var response = this.rolesService.GetAllRoles();
            return SuccessResponse(response);
        }
    }
}
