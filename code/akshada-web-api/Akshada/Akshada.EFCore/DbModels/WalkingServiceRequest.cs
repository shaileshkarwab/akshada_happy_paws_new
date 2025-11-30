using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WalkingServiceRequest
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int CustomerId { get; set; }

    public int PetId { get; set; }

    public int ServiceSystemId { get; set; }

    public int FrequencySystemId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal RegularDayRate { get; set; }

    public decimal SpecialDayRate { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual SystemParameter FrequencySystem { get; set; } = null!;

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual CustomerPet Pet { get; set; } = null!;

    public virtual SystemParameter ServiceSystem { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecords { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRequestDayScheduleAssignedToUser> WalkingServiceRequestDayScheduleAssignedToUsers { get; set; } = new List<WalkingServiceRequestDayScheduleAssignedToUser>();

    public virtual ICollection<WalkingServiceRequestDay> WalkingServiceRequestDays { get; set; } = new List<WalkingServiceRequestDay>();
}
