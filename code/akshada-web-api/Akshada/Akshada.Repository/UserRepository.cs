using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class UserRepository : GenericRepository<UserMaster>, IUserRepository
    {
        public UserRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
        }

        public List<UserMaster> GetAllUsers(string? userName)
        {
            var users = new List<UserMaster>();
            if (!string.IsNullOrEmpty(userName))
            {
                users = this.akshadaPawsContext.UserMasters.Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(userName.ToLower()))
                    .Include(c => c.Role).OrderBy(c => c.FirstName).ToList();
            }
            else
            {
                users = this.akshadaPawsContext.UserMasters.Include(c => c.Role).OrderBy(c => c.FirstName).ToList();
            }

            return users;
        }

        public DTO_UserAvailablityForDayResponse GetAvailablityOfUser(string userRowId, DTO_UserAvailablityForDay userAvailablityForDay)
        {
            var newFromTime = DateTime.ParseExact(userAvailablityForDay.FromTime, "hh:mm tt", null).TimeOfDay;
            var newToTime = DateTime.ParseExact(userAvailablityForDay.ToTime, "hh:mm tt", null).TimeOfDay;
            var db = akshadaPawsContext;

            // Base query that returns detail list + overlap flag per row
            var scheduleList =
                (from a in db.WalkingServiceRequestDayScheduleAssignedToUsers
                 join b in db.WalkingServiceRequestDaySchedules
                     on a.WalkingServiceRequestDayScheduleId equals b.Id
                 join c in db.WalkingServiceRequestDays
                     on b.WalkingServiceRequestDaysMasterId equals c.Id
                 join d in db.UserMasters
                     on a.UserId equals d.Id
                 where c.IsSelected == true
                    && d.RowId == userRowId
                    && c.WeekDayName == userAvailablityForDay.DayName
                 select new DTO_UserOccupancy
                 {
                     FromTime = b.FromTime.ToString(),
                     ToTime = b.ToTime.ToString(),
                     IsBetween = (newFromTime <= b.FromTime.TimeOfDay
                               && newToTime >= b.ToTime.TimeOfDay)
                 })
                .ToList(); // Materialize list here

            // Final Output → Overlap exists bool + all rows list
            var output = new DTO_UserAvailablityForDayResponse
            {
                IsOverlapExists = scheduleList.Any(x => x.IsBetween),   // true/false
                UserOccupancies = scheduleList                                     // list returned
            };

            return output;
        }

        public UserMaster ReteriveUserById(string rowId)
        {
            var user = this.akshadaPawsContext.UserMasters.Include(c => c.Role).FirstOrDefault(c => c.RowId == rowId);
            return user;
        }

        public DTO_VerifiedUser UserVerificationWithPin(string userRowId, DTO_UserPin pin)
        {
            try
            {
                var response = (from user in akshadaPawsContext.UserMasters.Where(u => u.RowId == userRowId && u.Status == true)
                                join role in akshadaPawsContext.RoleMasters.Where(r => r.Status == true)
                                on user.RoleId equals role.Id
                                select new
                                {
                                    Pin = user.MobilePin,
                                    UserName = user.LoginName
                                }
                                    ).FirstOrDefault();

                var passwordVerified = BCrypt.Net.BCrypt.Verify(pin.Pin, response.Pin);
                if (passwordVerified)
                {
                    return new DTO_VerifiedUser()
                    {
                        UserVerified = true,
                        Token = GenerateJSONWebToken(new DTO_UserVerification
                        {
                            Password = pin.Pin,
                            UserName = response.UserName,
                        })
                    };
                }
                throw new DTO_SystemException()
                {
                    Message = "Invalid user credentials. Password Not Matching",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    SystemException = new Exception()
                };
            }
            catch (Exception ex) {
                throw new DTO_SystemException()
                {
                    Message = string.Format("{0} - Exception {1}", "Invalid user credentials", ex.Message),
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    SystemException = ex
                };
            }
        }

        public DTO_VerifiedUser VerifyUser(DTO_UserVerification userDetails)
        {
            try
            {
                var response = (from user in akshadaPawsContext.UserMasters.Where(u => u.LoginName == userDetails.UserName && u.Status == true)
                                join role in akshadaPawsContext.RoleMasters.Where(r => r.Status == true)
                                on user.RoleId equals role.Id
                                select new
                                {
                                    Password = user.Password,
                                    User = user
                                }
                                ).FirstOrDefault();

                //verify user password
                var passwordVerified = BCrypt.Net.BCrypt.Verify(userDetails.Password, response.Password);

                if (passwordVerified)
                {

                    //update the refresh token and expiry date
                    var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                    var userDB = response.User;
                    userDB.RefreshToken = refreshToken;
                    userDB.RefreshTokenExpiry = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(Convert.ToInt16( configuration["Jwt:RefreshTokenExpiryDays"])));
                    this.akshadaPawsContext.UserMasters.Update(userDB);
                    this.akshadaPawsContext.SaveChanges();
                    return new DTO_VerifiedUser()
                    {
                        UserVerified = true,
                        Token = GenerateJSONWebToken(userDetails),
                        RefreshToken =  Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                        IsCompanyPresent = this.akshadaPawsContext.CompanyInformations.Any()
                    };
                }
                throw new DTO_SystemException()
                {
                    Message = "Invalid user credentials. Password Not Matching",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    SystemException = new Exception()
                };
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException()
                {
                    Message = string.Format("{0} - Exception {1}", "Invalid user credentials", ex.Message),
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    SystemException = ex
                };
            }

        }

        private string GenerateJSONWebToken(DTO_UserVerification userVerificationRequest)
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userVerificationRequest.UserName)
            }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16( configuration["Jwt:AccessTokenExpiryMinute"])),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Issuer"]
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
