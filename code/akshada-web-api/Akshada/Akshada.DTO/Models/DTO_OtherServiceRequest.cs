using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_OtherServiceRequest
    {

        public string? RowId { get; set; } = null!;

        public string? ServiceRequestDate { get; set; }

        public string? ServiceRequiredOnDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerAddress { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? PetName { get; set; }

        public string? PetImage { get; set; }

        public  DTO_LookUp? Customer { get; set; }

        public DTO_LookUp? Pet { get; set; }

        public DTO_LookUp? AddressLocationSystem { get; set; }

        public DTO_LookUp? RequiredServiceSystem { get; set; }

        public string? ExecutionDate { get; set; }

        public string? ExecutionFromTime { get; set; }

        public string? ExecutionToTime { get; set; }

        public DTO_LookUp? AssignedToUser { get; set; }

        public string? AssignRequestToUserRowId { get; set; }

        public DTO_LookUp? PetColourBreed { get; set; }

        public DTO_LookUp? PetColourSystem { get; set; }

        public string? CustomerAddressProof { get; set; }
    }
}
