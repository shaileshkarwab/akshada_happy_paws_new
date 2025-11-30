using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_HolidaySchedule
    {
        public string? RowId { get; set; } = null!;

        public string? HolidayName { get; set; } = null!;

        public string? HolidayDate { get; set; }

        public  DTO_SystemParameter? HolidayTypeSystem { get; set; } = null!;

    }
}
