using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IUserVerificationService
    {
        DTO_VerifiedUser VerifyUser(DTO_UserVerification userCredentials);

        DTO_VerifiedUser UserVerificationWithPin(string userRowId, DTO_UserPin pin);
    }
}
