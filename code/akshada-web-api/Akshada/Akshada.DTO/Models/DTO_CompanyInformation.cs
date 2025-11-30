using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_CompanyInformation
    {

        public string? RowId { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        public string CityTown { get; set; } = null!;

        public string PinCode { get; set; } = null!;

        public string ContactNo1 { get; set; } = null!;

        public string? ContactNo2 { get; set; }

        public string Email { get; set; } = null!;

        public string? Website { get; set; }

        public string Logo { get; set; }

        public List<DTO_CompanyInformationBankAccount> CompanyInformationBankAccounts { get; set; }

        public List<DTO_CompanyInformationUpiAccount> CompanyInformationUpiAccounts { get; set; }
    }

    public class DTO_CompanyInformationBankAccount
    {

        public string? RowId { get; set; } = null!;


        public string? BankName { get; set; } = null!;

        public string? BankAccount { get; set; } = null!;

        public string? BankIfscCode { get; set; } = null!;

        public string? BankBranch { get; set; } = null!;

        public string? AccountHolderName { get; set; }

    }

    public class DTO_CompanyInformationUpiAccount
    {
        public string? RowId { get; set; } = null!;

        public string? UpiId { get; set; } = null!;

        public string? UpiName { get; set; } = null!;
    }
}
