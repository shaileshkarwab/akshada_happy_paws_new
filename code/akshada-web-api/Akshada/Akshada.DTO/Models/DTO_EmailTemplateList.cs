using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_EmailTemplateList
    {
        public string RowId { get; set; } = null!;
        public string EmailEventName { get; set; } = null!;

        public DateOnly EmailEventDate { get; set; }

        public bool EmailEventRepeatForEveryYear { get; set; }

        public bool? IsActive { get; set; }

        public DTO_LookUp EmailNotificationSystem { get; set; } = null!;
    }
}
