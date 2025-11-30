using Akshada.DTO.Models;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserVerificationService(IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public DTO_VerifiedUser UserVerificationWithPin(string userRowId, DTO_UserPin pin)
        {
            var response = this.unitOfWork.UserRepository.UserVerificationWithPin(userRowId, pin);
            return response;
        }

        public DTO_VerifiedUser VerifyUser(DTO_UserVerification userCredentials)
        {
            var response =  this.unitOfWork.UserRepository.VerifyUser(userCredentials);
            return response;
        }
    }
}
