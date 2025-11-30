using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WalkingServiceRequestDay
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int WalkingServiceRequestMasterId { get; set; }

    public bool IsSelected { get; set; }

    public string WeekDayName { get; set; } = null!;

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecords { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRequestDaySchedule> WalkingServiceRequestDaySchedules { get; set; } = new List<WalkingServiceRequestDaySchedule>();

    public virtual WalkingServiceRequest WalkingServiceRequestMaster { get; set; } = null!;
}
