using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_PetInformation: DTO_CustomerPet
    {
        public string CustomerName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Mobile { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string CustomerRowId { get; set; } = null;

        public DTO_LookUp? AreaLocationSystem { get; set; }

        public DTO_LookUp? NatureOfPetSystem { get; set; }

    }
}
