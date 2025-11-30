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
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Akshada.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private Int32 UserID;
        private readonly GoogleDriveService _driveService;
        private readonly string _saveFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploaddata", "client-pet-images");
        private readonly IMapper mapper;
        public CustomerService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, GoogleDriveService _driveService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this._driveService = _driveService;
            this.mapper = mapper;
        }
        public async Task<List<DTO_Customer>> ProcessDataFromImportData()
        {
            var customers = this.unitOfWork.ImportDataRepository.Find(c => c.IsProcessed == false && c.OperationKey == "import-customers").ToList();
            UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
            foreach (var customer in customers)
            {
                using JsonDocument doc = JsonDocument.Parse(customer.JsonData);
                JsonElement root = doc.RootElement;
                string Username = "SBK"; //root.GetProperty("Username").GetString();
                string Email = root.GetProperty("Email Id").GetString();
                string MobileNumber = root.GetProperty("Mobile Number").GetString();
                string AddressLocation = root.GetProperty("Address Location").GetString();
                string ClientName = root.GetProperty("Client Name").GetString();
                string CanineName = root.GetProperty("Canine Name").GetString();

                string CanineOwnerPhotoInSingleFrame = root.GetProperty("Canine & Owner Photo (In Single Frame)").GetString();
                string VaccinationDetails = root.GetProperty("Vaccination Details").GetString();

                string CanineOwnerPhotoInSingleFrameId = HttpUtility.ParseQueryString(new Uri(CanineOwnerPhotoInSingleFrame).Query).Get("id");
                string VaccinationDetailsId = HttpUtility.ParseQueryString(new Uri(VaccinationDetails).Query).Get("id");

                //save the images and get the path
                string CanineOwnerPhotoInSingleFrameSavedPath = await _driveService.DownloadFileAsync(CanineOwnerPhotoInSingleFrameId, _saveFolder);
                string VaccinationDetailsSavedPath = await _driveService.DownloadFileAsync(VaccinationDetailsId, _saveFolder);

                //check if the email already exists
                var customerExists = this.unitOfWork.CustomerRepository.FindFirst(c => c.Email == Email);

                List<CustomerPet> customerPets = new List<CustomerPet>();
                customerPets.Add(new CustomerPet
                {
                    ImportDataId = customer.Id,
                    PetAndOwnerImage = CanineOwnerPhotoInSingleFrameSavedPath,
                    PetName = CanineName,
                    PetVaccinationImage = VaccinationDetailsSavedPath,
                    RowId = System.Guid.NewGuid().ToString()
                });

                if (customerExists == null)
                {
                    this.unitOfWork.CustomerRepository.Add(new EFCore.DbModels.Customer
                    {
                        Address = AddressLocation,
                        CreatedAt = DateTime.Now,
                        CreatedBy = UserID,
                        CustomerName = ClientName,
                        Email = Email,
                        IsActive = true,
                        Mobile = MobileNumber,
                        RowId = System.Guid.NewGuid().ToString(),
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = UserID,
                        UserName = Username,
                        CustomerPets = customerPets
                    });
                }
                else
                {
                    foreach (var item in customerPets)
                    {
                        item.CustomerId = customerExists.Id;
                        item.Customer = customerExists;
                        customerExists.CustomerPets.Add(item);
                    }
                }
                customer.IsProcessed = true;
                this.unitOfWork.ImportDataRepository.Update(customer);
                this.unitOfWork.Complete();
            }

            var allCustomers = this.unitOfWork.CustomerRepository.GetAllCustomers();
            var dtoCustomers = this.mapper.Map<List<DTO_Customer>>(allCustomers);
            return dtoCustomers;
        }

        public DTO_Customer ReteriveCustomerPetInformation(string petRowId)
        {
            var customerAndPetInformation = this.unitOfWork.CustomerRepository.ReteriveCustomerPetInformation(petRowId);
            var response = this.mapper.Map<DTO_Customer>(customerAndPetInformation);
            return response;
        }

        public bool UpdateCustomerPetInformation(string customerRowId, string petRowId, DTO_Customer customer)
        {
            UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
            try
            {
                var response = this.unitOfWork.CustomerRepository.UpdateCustomerPetInformation(customerRowId, petRowId, customer);
                return response;
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

        public bool SaveCustomerPetInformation(DTO_Customer customer)
        {
            try
            {
                var response = this.unitOfWork.CustomerRepository.SaveCustomerPetInformation(customer);
                return true;
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

        public List<DTO_Customer> GetCustomers(string? customerName)
        {
            IEnumerable<Customer> customers;
            if(!string.IsNullOrEmpty(customerName))
                customers =  this.unitOfWork.CustomerRepository.GetAllWithInclude(c => c.CustomerName.Contains(customerName), includeProperties: "AreaLocationSystem");
            else 
                customers = this.unitOfWork.CustomerRepository.GetAllWithInclude(includeProperties: "AreaLocationSystem");
            
            var response = this.mapper.Map<List<DTO_Customer>>(customers);
            return response;
        }

        public DTO_Customer ReteriveCustomer(string customerRowId)
        {
            var customer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId, "AreaLocationSystem");
            var response = this.mapper.Map<DTO_Customer>(customer);
            return response;
        }

        public bool AddPetToSelectedCustomer(string customerRowId, DTO_Customer customer)
        {
            try
            {
                var response = this.unitOfWork.CustomerRepository.AddPetToSelectedCustomer(customerRowId, customer);
                return response;
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

        public PagedList<DTO_PetInformation> GetPets(string? customerRowId,bool? includeAll, DTO_FilterAndPaging filterAndPaging)
        {
            var pets = this.unitOfWork.CustomerRepository.GetPets(customerRowId,includeAll).ApplyAdvanceFilters(filterAndPaging);
            var pagedList = PagedList<CustomerPet>.ToPagedList(pets, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var response = this.mapper.Map<List<DTO_PetInformation>>(pagedList);
            return new PagedList<DTO_PetInformation>(response, pagedList.TotalCount, pagedList.CurrentPage, pagedList.PageSize);
        }

        public bool UpdateCustomerInformation(string customerRowId, DTO_Customer customer)
        {
            try
            {
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId);
                UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the select customer");
                }
                dbCustomer.CustomerName = customer.CustomerName;
                dbCustomer.Email = customer.Email;
                dbCustomer.Mobile = customer.Mobile;
                dbCustomer.Address = customer.Address;
                dbCustomer.UserName = customer.UserName;
                dbCustomer.IsActive = customer.IsActive;
                dbCustomer.UpdatedAt = DateTime.Now;
                dbCustomer.UpdatedBy = UserID;
                dbCustomer.AreaLocationSystemId = this.unitOfWork.SystemParamRepository.FindFirst(c => c.RowId == customer.AreaLocationSystem.RowId).Id;
                dbCustomer.AddressProofImage = customer.AddressProofImage;
                this.unitOfWork.CustomerRepository.Update(dbCustomer);
                this.unitOfWork.Complete();
                return true;
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

        public bool DeleteCustomer(string customerRowId)
        {
            try
            {
                var dbCustomer = this.unitOfWork.CustomerRepository.FindFirst(c => c.RowId == customerRowId, includeProperties: "CustomerPets");
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the selected customer");
                }
                this.unitOfWork.CustomerRepository.DeleteCustomer(customerRowId);
                return true;
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

        public DTO_PetInformation GetPetByCustomerAndPetID(string customerRowID, string petRowId)
        {
            var pets = this.unitOfWork.CustomerRepository.GetPetByCustomerAndPetID(customerRowID, petRowId);
            return pets;
        }

        public PagedList<DTO_Customer> GetCustomerList(DTO_FilterAndPaging filterAndPaging)
        {
            var pagedResponse = this.unitOfWork.CustomerRepository.GetAll().ApplyAdvanceFilters(filterAndPaging);
            var pagedList = PagedList<Customer>.ToPagedList(pagedResponse, filterAndPaging.PageParameter.PageNumber, filterAndPaging.PageParameter.PageSize);
            var customerList = this.mapper.Map<List<DTO_Customer>>(pagedList);
            return new PagedList<DTO_Customer>(customerList, pagedList.TotalCount, pagedList.CurrentPage, pagedList.PageSize);
        }
    }
}
