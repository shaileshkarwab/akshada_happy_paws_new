using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_EmailTemplateMaster
    {

        public string? RowId { get; set; }


        public string EmailEventName { get; set; } = null!;

        public string EmailEventDate { get; set; }

        public bool EmailEventRepeatForEveryYear { get; set; }

        public bool? IsActive { get; set; }

        public DTO_LookUp EmailNotificationSystem { get; set; } = null!;

        public List<DTO_EmailTemplateMasterScheduleDetail> EmailTemplateMasterScheduleDetails { get; set; }

        public string? HtmlEmailTemplate { get; set; }

        public string? EmailSubject { get; set; } = null!;
    }

    public class DTO_EmailTemplateMasterScheduleDetail
    {
        public string? RowId { get; set; } = null!;

        public short ReminderDays { get; set; }

        public string TimeForNotification { get; set; }

    }
}
