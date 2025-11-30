using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class ImportantContact
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int ContactTypeSystemId { get; set; }

    public string ContactName { get; set; } = null!;

    public string? Email { get; set; }

    public string Mobile { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SystemParameter ContactTypeSystem { get; set; } = null!;

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ImportantContactAddressDetail> ImportantContactAddressDetails { get; set; } = new List<ImportantContactAddressDetail>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
}
