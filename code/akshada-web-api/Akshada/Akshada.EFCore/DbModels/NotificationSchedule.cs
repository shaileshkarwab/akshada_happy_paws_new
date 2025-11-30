using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class NotificationSchedule
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int NotificationEnumId { get; set; }

    public short BeforeDays { get; set; }

    public DateTime ScheduleOnTime { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
