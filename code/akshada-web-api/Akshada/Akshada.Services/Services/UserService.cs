using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public bool DeleteUser(string userRowId)
        {
            try
            {
                var userDB = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == userRowId);
                if (userDB == null)
                {
                    throw new Exception("Failed to get the details for the selected user");
                }
                this.unitOfWork.UserRepository.Remove(userDB);
                this.unitOfWork.Complete();
                return true;
            }
            catch (DbUpdateException de)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = "There requested user cannot be deleted. It may be refrenced in some other transactions",
                    SystemException = de
                };
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public DTO_UserDetail GetUserById()
        {
            var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
            var userDetails = this.unitOfWork.UserRepository.FindFirst(c => c.Id == userID);
            var timeSlotID = this.unitOfWork.SystemParamRepository.GetTimeSlotId();
            var dtoUser = this.mapper.Map<DTO_UserDetail>(userDetails);
            dtoUser.TimeSlot = this.mapper.Map<DTO_LookUp>(timeSlotID);
            return dtoUser;
        }

        public int? GetUserID(string userName)
        {
            var userDetail = this.unitOfWork.UserRepository.FindFirst(c => c.LoginName == userName && c.Status == true);
            return userDetail.Id;
        }

        public List<DTO_User> ListAllUsers(string? userName)
        {
            var users = this.unitOfWork.UserRepository.GetAllUsers(userName);
            var response = this.mapper.Map<List<DTO_User>>(users);
            return response;
        }

        public List<DTO_MenuResponse> LoggedInUserDetailMenu()
        {
            var menuResponse = this.unitOfWork.MenuMasterRepository.LoggedInUserDetailMenu();
            var masterMenus = menuResponse.Where(m => m.Page == "#").OrderBy(m => m.SeqNo).ToList();
            var menus = new List<DTO_MenuResponse>();
            foreach (var menu in masterMenus)
            {
                menus.Add(new DTO_MenuResponse
                {
                    Controller = menu.Controller,
                    FaIcon = menu.FaIcon,
                    MenuText = menu.MenuText,
                    Page = menu.Page,
                    ChildMenus = GetChildMenus(menuResponse, menu.Id)
                });
            }
            return menus;
        }

        public bool ResetUserPassword(string userRowId, DTO_UserResetPassword resetPassword)
        {
            try
            {
                if(resetPassword.ExistingPassWord.Equals(resetPassword.NewPassword,StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("The existing password and new password should be diffrent");
                }
                var user = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == userRowId);
                //check the existing password
                var isExistingPasswordMatch = BCrypt.Net.BCrypt.Verify(resetPassword.ExistingPassWord, user.Password);
                if (isExistingPasswordMatch)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.NewPassword);
                    this.unitOfWork.UserRepository.Update(user);
                    this.unitOfWork.Complete();
                    return true;
                }
                else
                {
                    throw new Exception("The existing password doesnot match. Update password failed");
                }
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException {
                     StatusCode = (Int32)HttpStatusCode.BadRequest,
                     Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public DTO_User ReteriveUserById(string rowId)
        {
            var users = this.unitOfWork.UserRepository.ReteriveUserById(rowId);
            var response = this.mapper.Map<DTO_User>(users);
            return response;
        }

        bool ValidateEntityForInsert(DTO_User validateEntity)
        {
            var user = this.unitOfWork.UserRepository.Any(c => c.LoginName == validateEntity.LoginName);
            if (user)
            {
                return false;
            }
            return true;
        }


        bool ValidateEntityForUpdate(DTO_User validateEntity)
        {
            var user = this.unitOfWork.UserRepository.Any(c => c.LoginName == validateEntity.LoginName && c.RowId != validateEntity.RowId);
            if (user)
            {
                throw new Exception("The login name already exists. Please use a diffrent login name");
            }

            var dbUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == validateEntity.RowId && c.Status == true);
            if (dbUser == null)
            {
                throw new Exception("Failed to get the details for the user to be updated");
            }

            return true;
        }

        public bool SaveUser(DTO_User saveEntity)
        {
            try
            {
                var validate = ValidateEntityForInsert(saveEntity);
                if (validate)
                {
                    var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                    var dbUser = new UserMaster { 
                        RowId = System.Guid.NewGuid().ToString(),
                        FirstName = saveEntity.FirstName,
                        LastName = saveEntity.LastName,
                        RoleId = this.unitOfWork.RolesRepository.FindFirst(c => c.RowId == saveEntity.Role.RowId).Id,
                        LoginName = saveEntity.LoginName,
                        Status = saveEntity.Status,
                        ImagePath = saveEntity.ImagePath,
                        CreatedBy = userID,
                        CreatedAt = System.DateTime.Now,
                        UpdatedBy = userID,
                        UpdatedAt = System.DateTime.Now,
                        Password  = BCrypt.Net.BCrypt.HashPassword(saveEntity.Password)
                    };
                    this.unitOfWork.UserRepository.Add(dbUser);
                    this.unitOfWork.Complete();
                    return true;
                }
                else
                {
                    throw new Exception("The login name already exists. Please use a diffrent login name");
                }
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public bool UpdateUserPin(string userRowId, DTO_UpdateUserPin updateUserPin)
        {
            try
            {
                if(updateUserPin.ExistingPin.Equals(updateUserPin.NewPin,StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("The new pin and the existing pin should be diffrent.");
                }
                var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                var userDB = this.unitOfWork.UserRepository.FindFirst(c => c.Id == userID);
                if (userDB == null)
                {
                    throw new Exception("Failed to get the details for the user");
                }

                if (!string.IsNullOrEmpty(userDB.MobilePin))
                {
                    var pinVerified = BCrypt.Net.BCrypt.Verify(updateUserPin.ExistingPin, userDB.MobilePin);
                    if (!pinVerified)
                    {
                        throw new Exception("Invalid existing pin. Please provide the excat pin.");
                    }
                }

                var newHashedPin = BCrypt.Net.BCrypt.HashPassword(updateUserPin.NewPin);
                userDB.MobilePin = newHashedPin;
                this.unitOfWork.UserRepository.Update(userDB);
                this.unitOfWork.Complete();
                return true;

            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
            throw new NotImplementedException();
        }

        List<DTO_MenuResponse> GetChildMenus(List<MenuMaster> menus, Int32 menuID)
        {
            var childMenus = new List<DTO_MenuResponse>();
            var subMenus = menus.Where(c => c.ParentId == menuID);
            foreach (var menu in subMenus)
            {
                childMenus.Add(new DTO_MenuResponse
                {
                    MenuText = menu.MenuText,
                    Page = menu.Page,
                    FaIcon = menu.FaIcon,
                });
            }
            return childMenus;
        }

        public bool UpdateUser(string userRowId, DTO_User saveEntity)
        {
            try
            {
                var validate = ValidateEntityForUpdate(saveEntity);
                if (validate)
                {
                    var dbUser = this.unitOfWork.UserRepository.FindFirst(c => c.RowId == saveEntity.RowId && c.Status == true);
                    dbUser.FirstName = saveEntity.FirstName;
                    dbUser.LastName = saveEntity.LastName;
                    dbUser.RoleId = this.unitOfWork.RolesRepository.FindFirst(c => c.RowId == saveEntity.Role.RowId).Id;
                    dbUser.LoginName = saveEntity.LoginName;
                    dbUser.Status = saveEntity.Status;
                    dbUser.ImagePath = saveEntity.ImagePath;
                    dbUser.UpdatedAt = System.DateTime.Now;
                    var userID = (Int32)this.httpContextAccessor.HttpContext.Items["USER_ID"];
                    dbUser.UpdatedBy = userID;
                    this.unitOfWork.UserRepository.Update(dbUser);
                    this.unitOfWork.Complete();
                    return true;
                }
                else
                {
                    throw new Exception("Something went wrong. Please check and validate the input");
                }
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException { 
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public bool AdminResetUserPassword(string userRowId, DTO_UserResetPassword resetPassword)
        {
            try
            {
                var dbUser = this.unitOfWork.UserRepository.FindFirst(c=>c.RowId == userRowId);
                if(dbUser == null)
                {
                    throw new Exception("Failed to get the details for the user");
                }
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetPassword.NewPassword);
                if(hashedPassword == dbUser.Password)
                {
                    throw new Exception("The existing and the new password must not be same");
                }
                dbUser.Password = hashedPassword;
                this.unitOfWork.UserRepository.Update(dbUser);
                this.unitOfWork.Complete();
                return true;
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException { 
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = ex
                };
            }
        }

        public DTO_UserAvailablityForDayResponse GetAvailablityOfUser(string userRowId, DTO_UserAvailablityForDay userAvailablityForDay)
        {
            var userAvailablity = this.unitOfWork.UserRepository.GetAvailablityOfUser(userRowId, userAvailablityForDay);
            return userAvailablity;
        }
    }
}
