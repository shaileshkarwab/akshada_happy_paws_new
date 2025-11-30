using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_ChangeTimeOrUserForRequest
    {
        public string WalkingServiceRequestRowId { get; set; }
        public string WalkingServiceRequestDayRowId { get; set; }
        public string WalkingServiceRequestDayScheduleRowId { get; set; }
        public string AssignDate { get; set; }
        public string ChangeFromTime { get; set; }
        public string ChangeToTime { get; set; }
        public string Remarks { get; set; }
        public string NewUserId { get; set; }
    }
}
