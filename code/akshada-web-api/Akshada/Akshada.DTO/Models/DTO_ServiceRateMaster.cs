using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_ServiceRateMaster
    {
        public string? RowId { get; set; } = null!;

        public string EntryDate { get; set; }

        public string EffectiveDate { get; set; }

        public bool IsActive { get; set; }

        public DTO_SystemParameter ServiceSystem { get; set; }

        public List<DTO_ServiceRateMasterDetail> ServiceRateMasterDetails { get; set; }

        public bool IsChargedMonthly { get; set; }
    }

    public class DTO_ServiceRateMasterDetail
    {
        public string? RowId { get; set; }

        public decimal? RegularRate { get; set; }

        public decimal? SpecialDayRate { get; set; }

        public bool? IsActive { get; set; }

        public string? LocationSystemName { get; set; }

        public DTO_SystemParameter? LocationSystem { get; set; }
    }
}
