using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class OtherServicesOffered
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateTime ServiceOfferedDate { get; set; }

    public int ServiceOfferedUserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int OtherServiceRequestMasterId { get; set; }

    public DateTime FromTime { get; set; }

    public DateTime ToTime { get; set; }

    public string? Remarks { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual OtherServiceRequest OtherServiceRequestMaster { get; set; } = null!;

    public virtual ICollection<OtherServicesOfferedImage> OtherServicesOfferedImages { get; set; } = new List<OtherServicesOfferedImage>();

    public virtual UserMaster ServiceOfferedUser { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
