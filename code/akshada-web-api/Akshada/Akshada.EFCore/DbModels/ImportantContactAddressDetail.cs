using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class ImportantContactAddressDetail
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int ImportantContactMasterId { get; set; }

    public int ContactAddressTypeSystemId { get; set; }

    public string? AddressName { get; set; }

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public string? CityTown { get; set; }

    public string? PinCode { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SystemParameter ContactAddressTypeSystem { get; set; } = null!;

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ImportantContact ImportantContactMaster { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
