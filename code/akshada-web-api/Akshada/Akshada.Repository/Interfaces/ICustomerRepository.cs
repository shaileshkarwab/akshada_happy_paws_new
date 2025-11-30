using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        List<Customer> GetAllCustomers();

        Customer ReteriveCustomerPetInformation(string petRowId);

        bool UpdateCustomerPetInformation(string customerRowId, string petRowId, DTO_Customer customer);

        bool SaveCustomerPetInformation(DTO_Customer customer);

        bool AddPetToSelectedCustomer(string customerRowId, DTO_Customer customer);

        IQueryable<CustomerPet> GetPets(string? customerRowIdcustomerRowId, bool? includeAll);

        bool DeleteCustomer(string customerRowId);

        DTO_PetInformation GetPetByCustomerAndPetID(string customerRowID, string petRowId);
    }
}
