using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface IUserService
    {
        Int32? GetUserID(string userName);
        DTO_UserDetail GetUserById();
        List<DTO_MenuResponse> LoggedInUserDetailMenu();

        List<DTO_User> ListAllUsers(string? userName);

        DTO_User ReteriveUserById(string rowId);

        bool SaveUser(DTO_User saveEntity);

        bool ResetUserPassword(string userRowId,DTO_UserResetPassword resetPassword);

        bool UpdateUserPin(string userRowId, DTO_UpdateUserPin updateUserPin);

        bool DeleteUser(string userRowId);

        bool UpdateUser(string userRowId, DTO_User saveEntity);

        bool AdminResetUserPassword(string userRowId, DTO_UserResetPassword resetPassword);

        DTO_UserAvailablityForDayResponse GetAvailablityOfUser(string userRowId, DTO_UserAvailablityForDay userAvailablityForDay);
    }
}
