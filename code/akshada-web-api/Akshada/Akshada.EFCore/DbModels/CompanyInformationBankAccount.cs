using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class CompanyInformationBankAccount
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int CompanyInformationMasterId { get; set; }

    public string BankName { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public string BankIfscCode { get; set; } = null!;

    public string BankBranch { get; set; } = null!;

    public string? AccountHolderName { get; set; }

    public virtual CompanyInformation CompanyInformationMaster { get; set; } = null!;
}
