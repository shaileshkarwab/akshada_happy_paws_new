using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class CompanyInformation
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string CityTown { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public string ContactNo1 { get; set; } = null!;

    public string? ContactNo2 { get; set; }

    public string Email { get; set; } = null!;

    public string? Website { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Logo { get; set; } = null!;

    public virtual ICollection<CompanyInformationBankAccount> CompanyInformationBankAccounts { get; set; } = new List<CompanyInformationBankAccount>();

    public virtual ICollection<CompanyInformationUpiAccount> CompanyInformationUpiAccounts { get; set; } = new List<CompanyInformationUpiAccount>();

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
