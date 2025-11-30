using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_NewUserAssignToWalkingService
    {
        public DateTime? ChangeFromTime { get; set; }

        public DateTime? ChangeToTime { get; set; }
        public DTO_User User { get; set; } = null!;

        public DTO_LookUp UserLookUp { get; set; } = null!;

        public string Remarks { get; set; }
    }
}
