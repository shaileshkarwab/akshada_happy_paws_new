using Akshada.API.AuthFilter;
using Akshada.DTO.Models;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService) {
            this.userService = userService;
        }

        [HttpGet("logged-in-user-details")]
        public IActionResult LoggedInUserDetails() {
           var response =  this.userService.GetUserById();
            return SuccessResponse(response);
        }

        [HttpGet("logged-in-user-detail-menu")]
        public IActionResult LoggedInUserDetailMenu()
        {
            var response = this.userService.LoggedInUserDetailMenu();
            return SuccessResponse(response);
        }

        [HttpGet("list-all-users")]
        public IActionResult ListAllUsers([FromQuery]string? userName) {
            if (string.IsNullOrEmpty(userName))
            {
                userName = string.Empty;
            }
            var response = this.userService.ListAllUsers(userName.Trim());
            return SuccessResponse(response);
        }

        [HttpGet("reterive-user-by-id/{rowId}")]
        public IActionResult ReteriveUserById([FromRoute]string rowId)
        {
            var response = this.userService.ReteriveUserById(rowId);
            return SuccessResponse(response);
        }

        [HttpPost("save-user")]
        public IActionResult SaveUser([FromBody]DTO_User saveEntity) {
            var response = this.userService.SaveUser(saveEntity);
            return SuccessResponse(response);
        }

        [HttpPost("reset-user-password/{userRowId}")]
        public IActionResult ResetUserPassword([FromRoute]string userRowId, [FromBody] DTO_UserResetPassword resetPassword)
        {
            var response = this.userService.ResetUserPassword(userRowId,resetPassword);
            return SuccessResponse(response);
        }

        [HttpPut("update-user-pin/{userRowId}")]
        public IActionResult UpdateUserPin([FromRoute] string userRowId, [FromBody] DTO_UpdateUserPin updateUserPin)
        {
            var response = this.userService.UpdateUserPin(userRowId, updateUserPin);
            return SuccessResponse(response);
        }

        [HttpDelete("{userRowId}")]
        public IActionResult DeleteUser([FromRoute] string userRowId)
        {
            var response = this.userService.DeleteUser(userRowId);
            return SuccessResponse(response);
        }

        [HttpPut("userRowId/{userRowId}")]
        public IActionResult UpdateUser([FromRoute]string userRowId, [FromBody] DTO_User saveEntity)
        {
            var response = this.userService.UpdateUser(userRowId,saveEntity);
            return SuccessResponse(response);
        }


        [HttpPatch("admin-reset-user-password/{userRowId}")]
        public IActionResult AdminResetUserPassword([FromRoute] string userRowId, [FromBody] DTO_UserResetPassword resetPassword)
        {
            var response = this.userService.AdminResetUserPassword(userRowId, resetPassword);
            return SuccessResponse(response);
        }

        [HttpPost("get-availablity-of-user/userRowId/{userRowId}")]
        public IActionResult GetAvailablityOfUser([FromRoute] string userRowId, [FromBody] DTO_UserAvailablityForDay userAvailablityForDay)
        {
            var response = this.userService.GetAvailablityOfUser(userRowId, userAvailablityForDay);
            return SuccessResponse(response);
        }
    }
}
