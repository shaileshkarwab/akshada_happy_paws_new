using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class RoleMaster
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public bool? Status { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<UserMaster> UserMasters { get; set; } = new List<UserMaster>();
}
