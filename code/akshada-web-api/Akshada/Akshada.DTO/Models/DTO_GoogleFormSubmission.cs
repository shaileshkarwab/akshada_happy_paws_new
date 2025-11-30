using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_GoogleFormSubmission
    {
        public string? ClientName { get; set; }
        public string? MobileNumber { get; set; }
        public string? CanineBreedColor { get; set; }
        public string? CanineName { get; set; }
        public string? AgeYearsMonth { get; set; }
        public string? ServiceTimeRequired { get; set; }
        public List<string>? ServiceShiftRequired { get; set; }
        public string? ServicePeriodRequired { get; set; }
        public string? WeightInKG { get; set; }
        public string? EmailId { get; set; }
        public string? AddressLocation { get; set; }
        public string? Date { get; set; }
        public List<string>? CanineOwnerPhotoInSingleFrame { get; set; }
        public string? CanineDateOfBirth { get; set; }
        public List<string>? VaccinationDetails { get; set; }
        public string? PastillnesshistoryCanine { get; set; }
        public string? Caninebehavior { get; set; }
        public string? TermsCondition { get; set; }

        public string? RowId { get; set; }
    }

    public class DTO_GoogleFormRawData
    {
        public string? RowId { get; set; } = null!;

        public string? JsonData { get; set; } = null!;

        public string? CustomerId { get; set; }

        public string? PetId { get; set; }
    }
}
