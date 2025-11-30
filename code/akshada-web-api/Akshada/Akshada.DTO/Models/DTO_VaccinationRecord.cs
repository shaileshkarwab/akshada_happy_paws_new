using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Models
{
    public class DTO_VaccinationRecord
    {

        public string? RowId { get; set; } = null!;

        public string RecordEntryDate { get; set; }

        public string VaccinationDate { get; set; }

        public string? VetOrClinicName { get; set; }

        public string? VetOrClinicContactNumber { get; set; }

        public string? Remarks { get; set; }

        public string VaccinationProofImage { get; set; } = null!;


        public DTO_LookUp Customer { get; set; } = null!;

        public DTO_LookUp Pet { get; set; } = null!;


        public List<DTO_VaccinationRecordDetail> VaccinationRecordDetails { get; set; } = new List<DTO_VaccinationRecordDetail>();

        public DTO_LookUp? VetOrClinicNameImpContact { get; set; }

        public bool IsFileDeleted {get;set;}
    }

    public class DTO_VaccinationRecordDetail {

        public string? RowId { get; set; } = null!;

        public string VaccinatedDate { get; set; }

        public string DueDate { get; set; }

        public DTO_SystemParameter VaccinationSystem { get; set; } = null!;

        public bool Selected { get; set; }
    }
}
