using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WalkingServiceRequestDayScheduleAssignedToUser
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int WalkingServiceRequestDayScheduleId { get; set; }

    public int UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int WalkingServiceRequestMasterId { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual UserMaster User { get; set; } = null!;

    public virtual WalkingServiceRequestDaySchedule WalkingServiceRequestDaySchedule { get; set; } = null!;

    public virtual WalkingServiceRequest WalkingServiceRequestMaster { get; set; } = null!;
}
