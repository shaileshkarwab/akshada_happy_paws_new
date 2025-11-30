using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_AssignOtherServiceRequestUser
    {

        public string? RowId { get; set; } = null!;

        public string? AssignDate { get; set; }

        public string OtherServiceRequestMasterId { get; set; }

        public string ChangedRequestDate { get; set; }

        public string FromTime { get; set; }

        public string ToTime { get; set; }

        public  DTO_LookUp AssignedToUser { get; set; } = null!;
        public string? Remarks { get; set; }

        public double ServiceCharge { get; set; }

        public bool? IsAmountToBeCollectedAfterService { get; set; }
    }
}
