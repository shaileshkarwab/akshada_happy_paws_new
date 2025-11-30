using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_CustomerPetForActivity
    {
        public DTO_LookUp ServiceSystem { get; set; }
        public string RowId { get; set; }

        public DTO_LookUp ServiceFrequency { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DTO_PetInformation Pet {get;set;}

        public DTO_LookUp? AreaLocationSystem { get; set; }

        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public DTO_LookUp? AssignedTo { get; set; }

        public DateTime? ServiceDate { get; set; }

        public string FromTimeStr { get; set; }
        public string ToTimeStr { get; set; }

        public DTO_LookUp AssignedToUser { get; set; }

        public string CustomerRowId { get; set; }

        public DTO_User SelectedUser { get; set; }

        public string WalkingServiceRequestDaysId { get; set; }
        public string WalkingServiceRequestDayScheduleId { get; set; }

        public string AssignRequestId { get; set; }

        public List<DTO_NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; }

        public string? Remarks { get; set; }
    }
}
