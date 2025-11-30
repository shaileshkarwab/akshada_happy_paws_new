using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WalkingServiceRecord
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateTime ServiceOfferedDate { get; set; }

    public DateTime FromTime { get; set; }

    public DateTime ToTime { get; set; }

    public int CustomerId { get; set; }

    public int PetId { get; set; }

    public int WalkingServiceMasterId { get; set; }

    public int WalkingServiceDayMasterId { get; set; }

    public int WalkingServiceDayScheduleMasterId { get; set; }

    public string? Remarks { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? ServiceOfferedByUserId { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomerPet Pet { get; set; } = null!;

    public virtual UserMaster? ServiceOfferedByUser { get; set; }

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual WalkingServiceRequestDay WalkingServiceDayMaster { get; set; } = null!;

    public virtual WalkingServiceRequestDaySchedule WalkingServiceDayScheduleMaster { get; set; } = null!;

    public virtual WalkingServiceRequest WalkingServiceMaster { get; set; } = null!;

    public virtual ICollection<WalkingServiceRecordImage> WalkingServiceRecordImages { get; set; } = new List<WalkingServiceRecordImage>();
}
