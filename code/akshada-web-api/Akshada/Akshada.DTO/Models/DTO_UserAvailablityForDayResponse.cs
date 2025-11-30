using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_UserAvailablityForDayResponse
    {
        public bool IsOverlapExists { get; set; }
        public List<DTO_UserOccupancy> UserOccupancies { get; set; }
    }

    public class DTO_UserOccupancy { 
        public string FromTime { get; set; }
        public string ToTime { get; set; }

        public bool IsBetween { get; set; }

    }
}
