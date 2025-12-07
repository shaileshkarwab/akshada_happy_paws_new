using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
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
    public class ContactAndAddressService : IContactAndAddressService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IHttpContextAccessor httpContextAccessor;
        private Int32 UserID;
        private readonly DateTime TransactionDate;
        public ContactAndAddressService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            TransactionDate = System.DateTime.Now;
        }

        public bool SaveContact(DTO_ImportantContact importantContact)
        {
            try
            {
                UserID = (Int32)httpContextAccessor.HttpContext.Items["USER_ID"];
                var contactTypeSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == importantContact.ContactTypeSystem.RowId);
                if (contactTypeSystem == null)
                {
                    throw new Exception("Failed to get the details for the contact type");
                }
                var dbContact = new ImportantContact
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    ContactTypeSystemId = contactTypeSystem.Id,
                    ContactName = importantContact.ContactName,
                    Email = importantContact.Email,
                    IsActive = importantContact.IsActive,
                    CreatedAt = TransactionDate,
                    CreatedBy = UserID,
                    UpdatedAt = TransactionDate,
                    UpdatedBy = UserID,
                    Mobile = importantContact.Mobile
                };

                foreach (var add in importantContact.ImportantContactAddressDetails)
                {
                    var contactAddressTypeSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == add.ContactAddressTypeSystem.RowId);
                    if (contactAddressTypeSystem == null)
                    {
                        throw new Exception("Failed to get the details for the contact address type");
                    }
                    dbContact.ImportantContactAddressDetails.Add(new ImportantContactAddressDetail
                    {
                        RowId = System.Guid.NewGuid().ToString(),
                        ContactAddressTypeSystemId = contactAddressTypeSystem.Id,
                        AddressName = add.AddressName,
                        Address1 = add.Address1,
                        Address2 = add.Address2,
                        CityTown = add.CityTown,
                        PinCode = add.PinCode,
                        Email = add.Email,
                        Mobile = add.Mobile,
                        IsActive = add.IsActive,
                        CreatedAt = TransactionDate,
                        CreatedBy = UserID,
                        UpdatedAt = TransactionDate,
                        UpdatedBy = UserID,
                    });
                };
                this.unitOfWork.ImportantContactRepo.Add(dbContact);
                this.unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw new DTO_SystemException
                {
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = new Exception()
                };
            }
        }

        public PagedList<DTO_ImportantContact> GetAll(DTO_FilterAndPaging filterAndPaging)
        {
            var contactQuery = this.unitOfWork.ImportantContactRepo.GetAllWithInclude(includeProperties: "ContactTypeSystem");
            var pagedList = PagedList<ImportantContact>.ToPagedList(contactQuery, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var dtoResponse = this.mapper.Map<List<DTO_ImportantContact>>(pagedList);
            return new PagedList<DTO_ImportantContact>(
                    dtoResponse,
                    pagedList.TotalCount,
                    pagedList.CurrentPage,
                    pagedList.PageSize
                );
        }

        public DTO_ImportantContact Reterive(string customerRowId)
        {
            var dbContact = this.unitOfWork.ImportantContactRepo.FindFirst(c => c.RowId == customerRowId, includeProperties: "ContactTypeSystem,ImportantContactAddressDetails,ImportantContactAddressDetails.ContactAddressTypeSystem");
            var response = this.mapper.Map<DTO_ImportantContact>(dbContact);
            return response;
        }

        public bool UpdateContactAndAddress(string customerRowId, DTO_ImportantContact updateEntity)
        {
            try
            {
                UserID = (Int32)httpContextAccessor.HttpContext.Items["USER_ID"];
                var contact = this.unitOfWork.ImportantContactRepo.FindFirst(c => c.RowId == customerRowId, includeProperties: "ContactTypeSystem");
                if (contact == null)
                {
                    throw new Exception("Failed to get the details for the contact");
                }
                var contactTypeSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == updateEntity.ContactTypeSystem.RowId);
                if (contact == null)
                {
                    throw new Exception("Failed to get the details for the contact type");
                }
                var dbImportantContat = new ImportantContact
                {
                    Id = contact.Id,
                    RowId = contact.RowId,
                    ContactTypeSystemId = contactTypeSystem.Id,
                    Email = updateEntity.Email,
                    Mobile = updateEntity.Mobile,
                    IsActive = updateEntity.IsActive,
                    CreatedBy = contact.CreatedBy,
                    CreatedAt = contact.CreatedAt,
                    UpdatedBy = UserID,
                    UpdatedAt = System.DateTime.Now,
                    ContactName = updateEntity.ContactName
                };

                foreach (var add in updateEntity.ImportantContactAddressDetails)
                {
                    var ContactAddressTypeSystem = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == add.ContactAddressTypeSystem.RowId);
                    if (ContactAddressTypeSystem == null)
                    {
                        throw new Exception("Failed to get the details for the contact type details");
                    }
                    if (string.IsNullOrEmpty(add.RowId))
                    {
                        dbImportantContat.ImportantContactAddressDetails.Add(new ImportantContactAddressDetail { 
                            Address1 = add.Address1,
                            Address2 = add.Address2,
                            AddressName = add.AddressName,
                            CityTown = add.CityTown,
                            ContactAddressTypeSystemId = ContactAddressTypeSystem.Id,
                            CreatedBy = UserID,
                            CreatedAt = System.DateTime.Now,
                            UpdatedBy = UserID,
                            UpdatedAt = System.DateTime.Now,
                            Email = add.Email,
                            Mobile = add.Mobile,
                            PinCode = add.PinCode,
                            IsActive = add.IsActive,
                            RowId = System.Guid.NewGuid().ToString()
                        });
                    }
                    else
                    {
                        var contactAddressDetail = this.unitOfWork.ImportantContactAddDtlRepo.FindFirst(c => c.RowId == add.RowId);
                        if (contactAddressDetail == null)
                        {
                            throw new Exception("Failed to get the details for the contact details");
                        }
                        contactAddressDetail.ContactAddressTypeSystemId = ContactAddressTypeSystem.Id;
                        contactAddressDetail.AddressName = add.AddressName;
                        contactAddressDetail.Address1 = add.Address1;
                        contactAddressDetail.Address2 = add.Address2;
                        contactAddressDetail.CityTown = add.CityTown;
                        contactAddressDetail.PinCode = add.PinCode;
                        contactAddressDetail.Email = add.Email;
                        contactAddressDetail.Mobile = add.Mobile;
                        contactAddressDetail.IsActive = add.IsActive;
                        contactAddressDetail.UpdatedBy = UserID;
                        contactAddressDetail.UpdatedAt = System.DateTime.Now;
                        this.unitOfWork.ImportantContactAddDtlRepo.Update(contactAddressDetail);
                    }
                }

                this.unitOfWork.ImportantContactRepo.Update(dbImportantContat);
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
        }

        public bool DeleteContactAndAddress(string customerRowId)
        {
            try
            {
                var contact = this.unitOfWork.ImportantContactRepo.FindFirst(c => c.RowId == customerRowId);
                if(contact == null)
                {
                    throw new Exception("Failed to get the contact and address details");
                }
                var dbContactAndAddress = this.unitOfWork.ImportantContactRepo.RemoveIncludingChildren(includes: "ImportantContactAddressDetails", c => c.RowId == customerRowId);
                this.unitOfWork.Complete();
                return true;
            }
            catch(Exception ex)
            {
                throw new DTO_SystemException { 
                    StatusCode = (Int32)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    SystemException = new Exception()
                };
            }
        }
    }
}
