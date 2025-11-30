using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_WebsiteServiceProcess
    {

        public string? RowId { get; set; } = null!;

        public bool? RequestAccepted { get; set; }


        public string? CustomerName { get; set; }

        public string? MobileContactNumber { get; set; }

        public string? ServiceDate { get; set; }

        public string? ServiceFromTime { get; set; }

        public string? ServiceToTime { get; set; }

        public DTO_LookUp? AssignedToUser { get; set; }

        public DTO_LookUp? RequestNotParamSystem { get; set; }

        public string? Email { get; set; }

        public string? Remarks { get; set; }

        public string? Address { get; set; }

        public DTO_LookUp ServiceSystem { get; set; }

        public DTO_LookUp AreaLocationSystem { get; set; }
    }
}
