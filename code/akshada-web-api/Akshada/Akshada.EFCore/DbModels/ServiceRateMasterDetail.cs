using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class ServiceRateMasterDetail
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int ServiceMasterId { get; set; }

    public int LocationSystemId { get; set; }

    public decimal RegularRate { get; set; }

    public decimal SpecialDayRate { get; set; }

    public bool IsActive { get; set; }

    public virtual SystemParameter LocationSystem { get; set; } = null!;

    public virtual ServiceRateMaster ServiceMaster { get; set; } = null!;
}
