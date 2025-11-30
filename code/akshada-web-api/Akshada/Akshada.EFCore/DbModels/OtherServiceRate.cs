using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class OtherServiceRate
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public DateTime EffectiveDate { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OtherServiceRateDetail> OtherServiceRateDetails { get; set; } = new List<OtherServiceRateDetail>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
