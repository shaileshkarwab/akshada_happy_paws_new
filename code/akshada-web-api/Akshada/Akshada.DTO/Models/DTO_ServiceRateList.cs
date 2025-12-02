using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_ServiceRateList
    {
        public string? RowId { get; set; }
        public string ServiceName { get; set; } = null!;
        public DateOnly EntryDate { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsChargedMonthly { get; set; }
    }
}
