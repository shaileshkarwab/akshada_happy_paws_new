using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IUserRepository:IGenericRepository<UserMaster>
    {
        DTO_VerifiedUser VerifyUser(DTO_UserVerification userDetails);

        List<UserMaster> GetAllUsers(string? userName);

        UserMaster ReteriveUserById(string rowId);

        DTO_VerifiedUser UserVerificationWithPin(string userRowId, DTO_UserPin pin);

        DTO_UserAvailablityForDayResponse GetAvailablityOfUser(string userRowId, DTO_UserAvailablityForDay userAvailablityForDay);
    }
}
