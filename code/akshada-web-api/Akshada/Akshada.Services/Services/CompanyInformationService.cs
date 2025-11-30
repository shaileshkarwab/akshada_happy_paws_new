using Akshada.DTO.Models;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class CompanyInformationService : ICompanyInformationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public CompanyInformationService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task<DTO_CompanyInformation> GetCompanyInformation()
        {
            try
            {
                var companyInfoQuery = this.unitOfWork.CompanyInfoRepository.GetAllWithInclude(includeProperties: "CompanyInformationBankAccounts,CompanyInformationUpiAccounts");
                var companyInfo = companyInfoQuery.FirstOrDefault();
                var response = this.mapper.Map<DTO_CompanyInformation>(companyInfo);
                return await Task.Run(() =>
                {
                    return response;
                });
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    Message = ex.Message,
                    StatusCode = (Int32)HttpStatusCode.BadRequest
                };
            }
        }

        public async Task<bool> SaveCompanyInformation(DTO_CompanyInformation companyInformation)
        {
            try
            {
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);

                var dbCompany = new EFCore.DbModels.CompanyInformation
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    CompanyName = companyInformation.CompanyName,
                    Address1 = companyInformation.Address1,
                    Address2 = companyInformation.Address2,
                    CityTown = companyInformation.CityTown,
                    PinCode = companyInformation.PinCode,
                    ContactNo1 = companyInformation.ContactNo1,
                    ContactNo2 = companyInformation.ContactNo2,
                    Email = companyInformation.Email,
                    Website = companyInformation.Website,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = userID,
                    UpdatedAt = System.DateTime.Now,
                    UpdatedBy = userID,
                    Logo = companyInformation.Logo
                };

                dbCompany.CompanyInformationBankAccounts.Add(new EFCore.DbModels.CompanyInformationBankAccount
                {
                    BankAccount = companyInformation.CompanyInformationBankAccounts[0].BankAccount,
                    BankBranch = companyInformation.CompanyInformationBankAccounts[0].BankBranch,
                    BankIfscCode = companyInformation.CompanyInformationBankAccounts[0].BankIfscCode,
                    BankName = companyInformation.CompanyInformationBankAccounts[0].BankName,
                    RowId = System.Guid.NewGuid().ToString()
                });

                dbCompany.CompanyInformationUpiAccounts.Add(new EFCore.DbModels.CompanyInformationUpiAccount
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    UpiId = companyInformation.CompanyInformationUpiAccounts[0].UpiId,
                    UpiName = companyInformation.CompanyInformationUpiAccounts[0].UpiName
                });

                this.unitOfWork.CompanyInfoRepository.Add(dbCompany);
                this.unitOfWork.Complete();
                return await Task.Run(() =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
        }

        public Task<bool> UpdateCompanyInformation(string companyRowId, DTO_CompanyInformation companyInformation)
        {
            try
            {
                var companyDB = this.unitOfWork.CompanyInfoRepository.FindFirst(c => c.RowId == companyRowId);
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                if (companyDB == null)
                {
                    throw new Exception("Failed to get the company information details");
                }
                companyDB.CompanyName = companyInformation.CompanyName;
                companyDB.Address1 = companyInformation.Address1;
                companyDB.Address2 = companyInformation.Address2;
                companyDB.CityTown = companyInformation.CityTown;
                companyDB.PinCode = companyInformation.PinCode;
                companyDB.ContactNo1 = companyInformation.ContactNo1;
                companyDB.ContactNo2 = companyInformation.ContactNo2;
                companyDB.Email = companyInformation.Email;
                companyDB.Website = companyInformation.Website;
                companyDB.UpdatedBy = userID;
                companyDB.UpdatedAt = System.DateTime.Now;
                companyDB.Logo = companyInformation.Logo;
                this.unitOfWork.CompanyInfoRepository.Update(companyDB);

                var bankAccount = this.unitOfWork.CompanyInformationBankAccountRepository.FindFirst(c=>c.RowId == companyInformation.CompanyInformationBankAccounts.First().RowId);
                if(bankAccount != null)
                {
                    var bankObj = companyInformation.CompanyInformationBankAccounts.First();
                    bankAccount.BankBranch = bankObj.BankBranch;
                    bankAccount.BankAccount = bankObj.BankAccount;
                    bankAccount.BankIfscCode = bankObj.BankIfscCode;
                    bankAccount.BankBranch = bankObj.BankBranch;
                    bankAccount.AccountHolderName = bankObj.AccountHolderName;
                    bankAccount.BankName = bankObj.BankName;
                    this.unitOfWork.CompanyInformationBankAccountRepository.Update(bankAccount);
                }

                var bankIFSC = this.unitOfWork.CompanyInformationUPIRepository.FindFirst(c => c.RowId == companyInformation.CompanyInformationUpiAccounts.First().RowId);
                if (bankIFSC != null)
                {
                    var bankObj = companyInformation.CompanyInformationUpiAccounts.First();
                    bankIFSC.UpiId = bankObj.UpiId;
                    bankIFSC.UpiName = bankObj.UpiName;
                    this.unitOfWork.CompanyInformationUPIRepository.Update(bankIFSC);
                }

                this.unitOfWork.Complete();
                return Task.Run(() =>
                {
                    return true;
                });
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }
    }
}
