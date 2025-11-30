using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_ImportantContact
    {


        public string? RowId { get; set; } = null!;


        public string? ContactName { get; set; } = null!;

        public string? Email { get; set; }

        public string Mobile { get; set; } = null!;

        public bool IsActive { get; set; }


        public DTO_LookUp ContactTypeSystem { get; set; } = null!;


        public List<DTO_ImportantContactAddressDetail> ImportantContactAddressDetails { get; set; } = new List<DTO_ImportantContactAddressDetail>();


    }

    public class DTO_ImportantContactAddressDetail
    {

        public string? RowId { get; set; } = null!;

        public string? AddressName { get; set; }

        public string Address1 { get; set; } = null!;

        public string Address2 { get; set; } = null!;

        public string? CityTown { get; set; }

        public string? PinCode { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        public bool? IsActive { get; set; }

        public DTO_LookUp ContactAddressTypeSystem { get; set; } = null!;

    }
}
