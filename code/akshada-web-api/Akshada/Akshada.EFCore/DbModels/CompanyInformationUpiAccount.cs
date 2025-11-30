using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class CompanyInformationUpiAccount
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int CompanyInformationMasterId { get; set; }

    public string UpiId { get; set; } = null!;

    public string UpiName { get; set; } = null!;

    public virtual CompanyInformation CompanyInformationMaster { get; set; } = null!;
}
