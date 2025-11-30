using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_WalkingServiceRecord
    {

        public string? RowId { get; set; } = null!;

        public string? ServiceOfferedDate { get; set; }

        public string? FromTime { get; set; }

        public string? ToTime { get; set; }

        public string WalkingServiceMasterId { get; set; }

        public string WalkingServiceDayMasterId { get; set; }

        public string WalkingServiceDayScheduleMasterId { get; set; }

        public string? Remarks { get; set; }

        public DTO_LookUp Customer { get; set; } = null!;

        public DTO_LookUp Pet { get; set; } = null!;

        public DTO_LookUp ServiceOfferedByUser { get; set; }


        public List<DTO_WalkingServiceRecordImage> WalkingServiceRecordImages { get; set; } = new List<DTO_WalkingServiceRecordImage>();

    }
}
