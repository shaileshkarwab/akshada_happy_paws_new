using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class EmailTemplateMasterScheduleDetail
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int EmailTemplateMasterId { get; set; }

    public short ReminderDays { get; set; }

    public DateTime TimeForNotification { get; set; }

    public virtual EmailTemplateMaster EmailTemplateMaster { get; set; } = null!;
}
