using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_WalingServiceList
    {
        public string RowId { get; set; }
        public DTO_LookUp ServiceSystem { get; set; }
        public DTO_LookUp FrequencySystem { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DTO_CustomerPet PetInformation { get; set; }

        public DTO_CustomerList CustomerInformation { get; set; }

    }
}
