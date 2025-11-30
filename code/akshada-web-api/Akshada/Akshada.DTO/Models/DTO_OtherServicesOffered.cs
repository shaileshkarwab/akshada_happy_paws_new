using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
   public class DTO_OtherServicesOffered
    {

        public string RowId { get; set; } = null!;

        public DateTime ServiceOfferedDate { get; set; }

        public DTO_LookUp Customer { get; set; }

        public DTO_LookUp Pet { get; set; }

        public DTO_LookUp OtherServiceSystem { get; set; }

        public DTO_LookUp ServiceOfferedUser { get; set; }
    }
}
