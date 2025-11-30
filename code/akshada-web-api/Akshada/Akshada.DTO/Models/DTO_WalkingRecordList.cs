using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_WalkingRecordList
    {
        public string ServiceDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public DTO_LookUp Pet { get; set; }
        public DTO_LookUp Customer {get;set;}

        public DTO_LookUp ServiceProvidedBy { get; set; }

        public string CustomerAddress { get; set; }
        public string PetColour { get; set; }
        public string PetBreed { get; set; }
        public string RowId { get; set; }

        public DTO_LookUp ServiceSystem { get; set; }
    }
}
