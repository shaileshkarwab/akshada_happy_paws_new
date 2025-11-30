using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
  public  class DTO_AdvanceSearchCriteria
    {
        public List<DTO_MultipleSelection>? selectedUsers { get; set; }
        public List<DTO_MultipleSelection>? selectedServices { get; set; }
        public List<DTO_MultipleSelection>? selectedWeekDays { get; set; }
        public DateTime? selectedFromDate { get; set; }
        public DateTime? selectedToDate { get; set; }
    }
}
