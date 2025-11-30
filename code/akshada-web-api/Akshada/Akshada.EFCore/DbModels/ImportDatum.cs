using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class ImportDatum
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string JsonData { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public string OperationKey { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CustomerPet> CustomerPets { get; set; } = new List<CustomerPet>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
