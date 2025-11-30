using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WalkingServiceRequestDaySchedule
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int WalkingServiceRequestDaysMasterId { get; set; }

    public DateTime FromTime { get; set; }

    public DateTime ToTime { get; set; }

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecords { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRequestDayScheduleAssignedToUser> WalkingServiceRequestDayScheduleAssignedToUsers { get; set; } = new List<WalkingServiceRequestDayScheduleAssignedToUser>();

    public virtual WalkingServiceRequestDay WalkingServiceRequestDaysMaster { get; set; } = null!;
}
