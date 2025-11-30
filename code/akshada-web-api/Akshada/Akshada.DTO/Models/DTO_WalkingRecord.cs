using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_WalkingRecord
    {

        public string? RowId { get; set; }

        public string? ServiceOfferedDate { get; set; }

        public string? FromTime { get; set; }

        public string? ToTime { get; set; }

        public string? Remarks { get; set; }

        public DTO_LookUp? Customer { get; set; }
        public DTO_LookUp? Pet { get; set; }

        public List<DTO_WalkingServiceRecordImage> WalkingServiceRecordImages { get; set; }

        public DTO_LookUp? ServiceOfferedByUser { get; set; }

        public string? WalkingServiceMasterRowId { get; set; }
        public string? WalkingServiceDayMasterRowId { get; set; }
        public string? WalkingServiceDayScheduleMasterRowId { get; set; }

        public List<DTO_WalkingServiceRequestServices>? WalkingServiceRequestServices { get; set; }
    }

    public class DTO_WalkingServiceRecordImage
    {
        public string? RowId { get; set; }
        public DTO_LookUp? UploadImageSystemParam { get; set; }

        public string? ImageName { get; set; }

        public double? Lattitude { get; set; }

        public double? Longitude { get; set; }

        public string? RecordTime { get; set; }
    }

    public class DTO_WalkingServiceRequestServices
    {
        public string? WalkingServiceMasterId { get; set; }
        public string? WalkingServiceDayMasterId { get; set; }
        public string? WalkingServiceDayScheduleMasterId { get; set; }

        public string? DayName { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public bool? Selected { get; set; }

    }
}
