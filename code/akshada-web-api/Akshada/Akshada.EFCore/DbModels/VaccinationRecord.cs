using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class VaccinationRecord
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateOnly RecordEntryDate { get; set; }

    public DateOnly VaccinationDate { get; set; }

    public int CustomerId { get; set; }

    public int PetId { get; set; }

    public string? VetOrClinicName { get; set; }

    public string? VetOrClinicContactNumber { get; set; }

    public string? Remarks { get; set; }

    public string VaccinationProofImage { get; set; } = null!;

    public int? VetOrClinicNameImpContactId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomerPet Pet { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<VaccinationRecordDetail> VaccinationRecordDetails { get; set; } = new List<VaccinationRecordDetail>();

    public virtual ImportantContact? VetOrClinicNameImpContact { get; set; }
}
