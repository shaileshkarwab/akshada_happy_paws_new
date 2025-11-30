using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface ICompanyInformationService
    {
        Task<bool> SaveCompanyInformation(DTO_CompanyInformation companyInformation);
        Task<bool> UpdateCompanyInformation(string companyRowId, DTO_CompanyInformation companyInformation);

        Task<DTO_CompanyInformation> GetCompanyInformation();
    }
}
