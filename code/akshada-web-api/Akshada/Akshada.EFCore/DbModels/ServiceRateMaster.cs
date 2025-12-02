using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class ServiceRateMaster
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int ServiceSystemId { get; set; }

    public DateOnly EntryDate { get; set; }

    public DateOnly EffectiveDate { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsChargedMonthly { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ServiceRateMasterDetail> ServiceRateMasterDetails { get; set; } = new List<ServiceRateMasterDetail>();

    public virtual SystemParameter ServiceSystem { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
