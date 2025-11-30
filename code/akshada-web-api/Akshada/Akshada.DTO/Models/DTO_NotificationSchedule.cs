using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_NotificationSchedule
    {

        public string? RowId { get; set; } = null!;

        public short? BeforeDays { get; set; }

        public string? ScheduleOnTime { get; set; }

        public  DTO_LookUp? NotificationTypeSystem { get; set; } = null!;

    }
}
