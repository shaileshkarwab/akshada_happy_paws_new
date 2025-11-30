using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_OtherServiceRate
    {

        public string? RowId { get; set; } = null!;

        public string EntryDate { get; set; }

        public string EffectiveDate { get; set; }

        public bool IsActive { get; set; }

        public List<DTO_OtherServiceRateDetail> OtherServiceRateDetails { get; set; } = new List<DTO_OtherServiceRateDetail>();

    }

    public class DTO_OtherServiceRateDetail
    {

        public string? RowId { get; set; } = null!;

        public double ChargeableAmount { get; set; }

        public DTO_LookUp ServiceSystem { get; set; } = null!;

    }
}
