using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<DTO_Customer>> ProcessDataFromImportData();

        public DTO_Customer ReteriveCustomerPetInformation(string petRowId);

        bool UpdateCustomerPetInformation(string customerRowId, string petRowId, DTO_Customer customer);

        bool SaveCustomerPetInformation(DTO_Customer customer);

        List<DTO_Customer> GetCustomers(string? customerName);

        DTO_Customer ReteriveCustomer(string customerRowId);

        bool AddPetToSelectedCustomer(string customerRowId, DTO_Customer customer);

        PagedList<DTO_PetInformation> GetPets(string? customerRowId,bool? includeAll, DTO_FilterAndPaging filterAndPaging);

        bool UpdateCustomerInformation(string customerRowId, DTO_Customer customer);

        bool DeleteCustomer(string customerRowId);

        DTO_PetInformation GetPetByCustomerAndPetID(string customerRowID , string petRowId);

        PagedList<DTO_Customer>  GetCustomerList(DTO_FilterAndPaging filterAndPaging);
    }
}
