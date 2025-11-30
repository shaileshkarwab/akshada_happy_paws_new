using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class MenuMaster
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string MenuText { get; set; } = null!;

    public int SeqNo { get; set; }

    public string? Controller { get; set; }

    public string? Page { get; set; }

    public string FaIcon { get; set; } = null!;

    public int? ParentId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }
}
