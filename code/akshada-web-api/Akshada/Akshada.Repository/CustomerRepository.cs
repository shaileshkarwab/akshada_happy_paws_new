using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Akshada.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private Int32 UserID;
        public CustomerRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
            this.httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
            this.mapper = services.GetRequiredService<IMapper>();
        }

        public bool AddPetToSelectedCustomer(string customerRowId, DTO_Customer customer)
        {
            try
            {
                var dbCustomer = this.akshadaPawsContext.Customers.FirstOrDefault(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the details for the selected customer");
                }

                var breedInfo = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].BreedSystem.RowId);
                var colourInfo = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].Colour.RowId);
                var pet = new CustomerPet
                {
                    BreedSystemId = breedInfo.Id,
                    ColourId = colourInfo.Id,
                    RowId = System.Guid.NewGuid().ToString(),
                    PetName = customer.CustomerPets[0].PetName,
                    PetAgeYear = customer.CustomerPets[0].PetAgeYear,
                    PetAgeMonth = customer.CustomerPets[0].PetAgeMonth,
                    PetWeight = customer.CustomerPets[0].PetWeight,
                    PetAndOwnerImage = customer.CustomerPets[0].PetAndOwnerImage,
                    PetVaccinationImage = customer.CustomerPets[0].PetVaccinationImage,
                    PetPastIllness = customer.CustomerPets[0].PetPastIllness,
                    PetDateOfBirth = customer.CustomerPets[0].PetDateOfBirth,
                    CustomerId = dbCustomer.Id,
                    IsActive = customer.CustomerPets[0].IsActive,
                    IsDataComplete = customer.CustomerPets[0].IsDataComplete,
                };

                this.akshadaPawsContext.CustomerPets.Add(pet);
                this.akshadaPawsContext.SaveChanges();
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

        public bool DeleteCustomer(string customerRowId)
        {
            try
            {
                var dbCustomers = this.akshadaPawsContext.Customers
                    .Include(c => c.CustomerPets)
                    .FirstOrDefault(c => c.RowId == customerRowId);

                this.akshadaPawsContext.RemoveRange(dbCustomers.CustomerPets);
                this.akshadaPawsContext.Remove(dbCustomers);
                this.akshadaPawsContext.SaveChanges();
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

        public List<Customer> GetAllCustomers()
        {
            //var customers = this.akshadaPawsContext.Customers.Include(c => c.CustomerPets).ToList();
            var customers = this.akshadaPawsContext.CustomerPets.Where(c => c.ImportDataId != null && c.IsDataComplete == false).Include(c => c.Customer).ToList();
            return customers.Select(c => c.Customer).Distinct().ToList();
        }

        public DTO_PetInformation GetPetByCustomerAndPetID(string customerRowID, string petRowId)
        {
            //var pets = this.akshadaPawsContext.CustomerPets.Where(c => c.IsActive == true 
            //&& c.IsDataComplete == true && c.RowId == petRowId)
            //    .Include(c => c.Customer)
            //    .Include(c => c.BreedSystem)
            //    .Include(c => c.Colour)
            //    .FirstOrDefault();


            var pets = this.akshadaPawsContext.CustomerPets
            .Where(c => c.IsActive == true && c.IsDataComplete == true && c.RowId == petRowId)
            .Select(c => new CustomerPet
            {
                // Copy scalar properties
                RowId = c.RowId,
                PetName = c.PetName,
                PetAgeYear = c.PetAgeYear,
                PetAgeMonth = c.PetAgeMonth,
                IsActive = c.IsActive,
                IsDataComplete = c.IsDataComplete,
                BreedSystemId = c.BreedSystemId,
                ColourId = c.ColourId,
                BreedSystem = c.BreedSystem,
                Colour = c.Colour,
                PetAndOwnerImage = c.PetAndOwnerImage,
                PetDateOfBirth = c.PetDateOfBirth,
                PetWeight = c.PetWeight,
                Customer = c.Customer != null && c.Customer.RowId == customerRowID ? c.Customer : null
            }).FirstOrDefault();

            var response = this.mapper.Map<DTO_PetInformation>(pets);
            return response;
        }

        public IQueryable<CustomerPet> GetPets(string? customerRowId, bool? includeAll)
        {
            var pets = new List<CustomerPet>();
            IQueryable<CustomerPet> petsQuerable;
            if (string.IsNullOrEmpty(customerRowId))
            {
                petsQuerable = this.akshadaPawsContext.CustomerPets.Where(c => c.IsActive == true && c.IsDataComplete == true)
                    .Include(c => c.Customer)
                    .Include(c => c.Customer).ThenInclude(c=>c.AreaLocationSystem)
                    .Include(c => c.BreedSystem)
                    .Include(c => c.Colour)
                    .Include(c=>c.NatureOfPetSystem)
                    .AsQueryable();
            }
            else
            {
                petsQuerable = this.akshadaPawsContext.CustomerPets.Where(c => c.IsActive == true
                && c.IsDataComplete == true
                && c.Customer.RowId == customerRowId
                )
                    .Include(c => c.Customer)
                    .Include(c => c.Customer).ThenInclude(c => c.AreaLocationSystem)
                    .Include(c => c.BreedSystem)
                    .Include(c => c.Colour)
                    .Include(c => c.NatureOfPetSystem)
                    .AsQueryable();
            }



            if (includeAll.HasValue && includeAll.Value == true)
            {
                petsQuerable = this.akshadaPawsContext.CustomerPets.Where(c=> c.Customer.RowId == customerRowId
                )
                    .Include(c => c.Customer)
                    .Include(c => c.Customer).ThenInclude(c => c.AreaLocationSystem)
                    .Include(c => c.BreedSystem)
                    .Include(c => c.Colour)
                    .Include(c => c.NatureOfPetSystem)
                    .AsQueryable();
            }
            return petsQuerable;
        }

        public Customer ReteriveCustomerPetInformation(string petRowId)
        {
            var customerPetInformation = this.akshadaPawsContext.CustomerPets.Include(c => c.Customer)
                .Include(b => b.BreedSystem)
                .Include(c => c.Colour)
                .Include(c=>c.NatureOfPetSystem)
                .Include(c=>c.Customer)
                .Include(c => c.Customer).ThenInclude(c=>c.AreaLocationSystem)
                .FirstOrDefault(c => c.RowId == petRowId);

            return customerPetInformation.Customer;
        }

        public bool SaveCustomerPetInformation(DTO_Customer customer)
        {
            try
            {
                UserID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                var dbCustomer = this.mapper.Map<Customer>(customer);
                dbCustomer.UserName = customer.Email;
                dbCustomer.CreatedBy = UserID;
                dbCustomer.CreatedAt = DateTime.Now;
                dbCustomer.UpdatedBy = UserID;
                dbCustomer.UpdatedAt = DateTime.Now;
                dbCustomer.RowId = System.Guid.NewGuid().ToString();
                var breedID = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].BreedSystem.RowId).Id;
                var colourID = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].Colour.RowId).Id;
                var natureID = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].NatureOfPetSystem.RowId).Id;
                var dbAreaLocation = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.AreaLocationSystem.RowId);
                if (dbAreaLocation != null)
                {
                    dbCustomer.AreaLocationSystemId = dbAreaLocation == null ? null : dbAreaLocation.Id;
                    dbCustomer.AreaLocationSystem = dbAreaLocation;
                }
                List<CustomerPet> pets = new List<CustomerPet>();
                pets.Add(new CustomerPet
                {
                    RowId = System.Guid.NewGuid().ToString(),
                    BreedSystemId = breedID,
                    ColourId = colourID,
                    PetName = customer.CustomerPets[0].PetName,
                    PetAgeMonth = customer.CustomerPets[0].PetAgeMonth,
                    PetAgeYear = customer.CustomerPets[0].PetAgeYear,
                    PetWeight = customer.CustomerPets[0].PetWeight,
                    PetAndOwnerImage = customer.CustomerPets[0].PetAndOwnerImage,
                    PetVaccinationImage = customer.CustomerPets[0].PetVaccinationImage,
                    PetPastIllness = customer.CustomerPets[0].PetPastIllness,
                    PetDateOfBirth = customer.CustomerPets[0].PetDateOfBirth,
                    IsActive = customer.CustomerPets[0].IsActive,
                    IsDataComplete = customer.CustomerPets[0].IsDataComplete,
                    NatureOfPetSystemId = natureID
                });
                dbCustomer.CustomerPets = pets;
                this.akshadaPawsContext.Customers.Add(dbCustomer);
                this.akshadaPawsContext.SaveChanges();
                // if the google form row id is present update the respective reocrd in google forms
                if (!string.IsNullOrEmpty(customer.GoogleFormRowId))
                {
                    var googleFormDB = this.akshadaPawsContext.GoogleFormSubmissions.FirstOrDefault(c => c.RowId == customer.GoogleFormRowId);
                    if(googleFormDB != null)
                    {
                        googleFormDB.CustomerId = dbCustomer.Id;
                        googleFormDB.PetId = dbCustomer.CustomerPets.FirstOrDefault().Id;
                    }
                    this.akshadaPawsContext.SaveChanges();
                }
                    
                
                
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

        public bool UpdateCustomerPetInformation(string customerRowId, string petRowId, DTO_Customer customer)
        {
            try
            {
                var userID = Convert.ToInt32(this.httpContextAccessor.HttpContext.Items["USER_ID"]);
                var dbCustomer = this.akshadaPawsContext.Customers.FirstOrDefault(c => c.RowId == customerRowId);
                if (dbCustomer == null)
                {
                    throw new Exception("Failed to get the customer details");
                }
                var dbAreaLocation = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.AreaLocationSystem.RowId);
                dbCustomer.CustomerName = customer.CustomerName;
                dbCustomer.Email = customer.Email;
                dbCustomer.Mobile = customer.Mobile;
                dbCustomer.Address = customer.Address;
                dbCustomer.UserName = customer.UserName;
                dbCustomer.IsActive = customer.IsActive;
                dbCustomer.UpdatedAt = DateTime.Now;
                dbCustomer.UpdatedBy = userID;
                dbCustomer.AreaLocationSystemId = dbAreaLocation == null ? null : dbAreaLocation.Id;
                //update the pet information
                var dbPet = this.akshadaPawsContext.CustomerPets.FirstOrDefault(c => c.RowId == petRowId);
                if (dbPet == null)
                {
                    throw new Exception("Failed to get the customer pet details");
                }
                Int32 breedID = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].BreedSystem.RowId).Id;
                Int32 colourID = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].Colour.RowId).Id;
                Int32 natureId = this.akshadaPawsContext.SystemParameters.FirstOrDefault(c => c.RowId == customer.CustomerPets[0].NatureOfPetSystem.RowId).Id;
                dbPet.PetName = customer.CustomerPets[0].PetName;
                dbPet.BreedSystemId = breedID;
                dbPet.ColourId = colourID;
                dbPet.PetAgeYear = customer.CustomerPets[0].PetAgeYear;
                dbPet.PetAgeMonth = customer.CustomerPets[0].PetAgeMonth;
                dbPet.PetWeight = customer.CustomerPets[0].PetWeight;
                dbPet.PetAndOwnerImage = customer.CustomerPets[0].PetAndOwnerImage;
                dbPet.PetVaccinationImage = customer.CustomerPets[0].PetVaccinationImage;
                dbPet.PetPastIllness = customer.CustomerPets[0].PetPastIllness;
                dbPet.PetDateOfBirth = customer.CustomerPets[0].PetDateOfBirth;
                dbPet.IsActive = customer.CustomerPets[0].IsActive;
                dbPet.IsDataComplete = customer.CustomerPets[0].IsDataComplete;
                dbPet.NatureOfPetSystemId = natureId;

                this.akshadaPawsContext.SaveChanges();

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
    }
}
