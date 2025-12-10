using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.DbModels
{
    public class DailyOtherServiceList
    {
        public DateTime ServiceDate { get; set; }
        public string RequestRowId { get; set; }
        public DateTime ServiceRequiredOnDate { get; set; }
        public DateTime ChangedRequestDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string AreaLocation { get; set; }

        public string AreaLocationRowId { get; set; }
        public string PetName { get; set; }
        public string Breed { get; set; }
        public string Colour { get; set; }

        public string BreedRowId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string UserFirstName { get; set; }
        public string userLastName { get; set; }

        public string ServiceName { get; set; }

        public string ServiceRowId { get; set; }

        public string AssignedToUserRowId { get; set; }

        public string? ServiceOfferedRowId { get; set; }

        public string? TimeSlot { get; set; }
        public string? TimeSlotId { get; set; }

    }
}
