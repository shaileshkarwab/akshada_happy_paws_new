using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class GoogleFormSubmission
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string JsonData { get; set; } = null!;

    public int? CustomerId { get; set; }

    public int? PetId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual CustomerPet? Pet { get; set; }
}
