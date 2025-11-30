using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_Customer: DTO_CustomerList
    {

        public List<DTO_CustomerPet> CustomerPets { get; set; } 

    }

    public class DTO_CustomerList
    {

        public string? RowId { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Mobile { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? UserName { get; set; } = null!;

        public bool? IsActive { get; set; }


        public DTO_LookUp? AreaLocationSystem { get; set; }

        public string? AddressProofImage { get; set; }

        public string? GoogleFormSubmissionJsonData { get; set; }

        public string? GoogleFormRowId { get; set; }

    }
}
