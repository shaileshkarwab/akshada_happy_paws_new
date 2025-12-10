using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_UserDetail
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ImagePath { get; set; }

        public string? RowId { get; set; }

        public bool? IsMobilePinAvailable { get; set; }

        public DTO_LookUp? TimeSlot { get; set; }
    }
}
