using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_WalkingServiceRequest
    {

        public string? RowId { get; set; } = null!;

        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public bool? IsActive { get; set; }

        public DTO_LookUp? FrequencySystem { get; set; } = null!;

        public DTO_LookUp? ServiceSystem { get; set; } = null!;

        public List<DTO_WalkingServiceRequestDay>? WalkingServiceRequestDays { get; set; }

        public DTO_Customer? Customer { get; set; }

        public DTO_CustomerPet? Pet { get; set; }

        public decimal RegularDayRate { get; set; }

        public decimal SpecialDayRate { get; set; }

        public bool IsChargedMonthly { get; set; }


    }

    public class DTO_WalkingServiceRequestDay
    {

        public string? RowId { get; set; } = null!;


        public bool? IsSelected { get; set; }

        public string? WeekDayName { get; set; } = null!;

        public List<DTO_WalkingServiceRequestDaySchedule>? WalkingServiceRequestDaySchedules { get; set; }
    }

    public class DTO_WalkingServiceRequestDaySchedule
    {

        public string? RowId { get; set; } = null!;


        public string? FromTime { get; set; }

        public string? ToTime { get; set; }

        public List<DTO_WalkingServiceRequestDayScheduleAssignedToUserDisplay>? WalkingServiceRequestDayScheduleAssignedToUsers { get; set; }
    }

    public class DTO_WalkingServiceRequestDayScheduleAssignedToUserDisplay
    {
        public string? RowId { get; set; }

        public DTO_LookUp User { get; set; } = null!;
    }

    public class DTO_SearchParam
    {
        public string customerRowId { get; set; }
        public string petRowId { get; set; }

        public Int32 WalkingServiceMasterId { get; set; }

        public Int32 WalkingServiceDayMasterId { get; set; }

        public Int32 WalkingServiceDayScheduleMasterId { get; set; }

        public DateTime ServiceOfferedDate { get; set; }
    }
}
