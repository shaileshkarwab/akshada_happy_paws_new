using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_OtherServiceRateList
    {
        public string? RowId { get; set; } = null!;

        public string EntryDate { get; set; }

        public string EffectiveDate { get; set; }

        public bool IsActive { get; set; }
    }
}
