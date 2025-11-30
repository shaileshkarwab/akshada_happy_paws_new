using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_CustomerPet
    {

        public string? RowId { get; set; } = null!;

        public string? PetName { get; set; }

        public int? PetAgeYear { get; set; }

        public int? PetAgeMonth { get; set; }

        public double? PetWeight { get; set; }

        public string? PetAndOwnerImage { get; set; }

        public string? PetVaccinationImage { get; set; }

        public string? PetPastIllness { get; set; }

        public DateTime? PetDateOfBirth { get; set; }

        public bool? IsActive { get; set; }

        public DTO_LookUp? BreedSystem { get; set; }

        public DTO_LookUp? Colour { get; set; }

        public bool IsDataComplete { get; set; }

        public DTO_LookUp? NatureOfPetSystem { get; set; }
    }
}
