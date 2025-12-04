using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class EmailTemplateMaster
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int EmailNotificationSystemId { get; set; }

    public string EmailEventName { get; set; } = null!;

    public DateOnly EmailEventDate { get; set; }

    public bool EmailEventRepeatForEveryYear { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? HtmlEmailTemplate { get; set; }

    public string EmailSubject { get; set; } = null!;

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual SystemParameter EmailNotificationSystem { get; set; } = null!;

    public virtual ICollection<EmailTemplateMasterScheduleDetail> EmailTemplateMasterScheduleDetails { get; set; } = new List<EmailTemplateMasterScheduleDetail>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
