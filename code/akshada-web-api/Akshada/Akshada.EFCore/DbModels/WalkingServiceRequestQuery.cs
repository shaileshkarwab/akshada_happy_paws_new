using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.DbModels
{
    public class WalkingServiceRequestQuery
    {
        public DateTime ServiceDate { get; set; }
        public string DayName { get; set; }
        public string WalkingRequestID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerRowId { get; set; }
        public string PetName { get; set; }
        public string PetId { get; set; }
        public string Breed { get; set; }
        public string Colour { get; set; }

        public string? AreaLocationName { get; set; }

        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string WeekDayName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public Int32 DaysPending { get; set; }

        public string RequestDayId { get; set; }

        public string ScheduleRowId { get; set; }

        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserRowId { get; set; }

        public string? AreaLocationRowId { get; set; }

        public string? ServiceSystemRowId { get; set; }
        public string? ServiceSystemValue { get; set; }
        public string? FrequencySystemRowId { get; set; }
        public string? FrequencySystemValue { get; set; }

        public string? WalkingServiceRecordId { get; set; }

        public DateTime? VaccinationDueDate { get; set; }
        public int? VaccinationPendingDays { get; set; }
        public string? PetAndOwnerImage { get; set; }
        //public  DTO_UserMaster SelectedUser => new DTO_UserMaster
        //{
        //    FirstName = UserFirstName,
        //    LastName = UserLastName,
        //    RowId = UserRowId
        //};

    }
}
