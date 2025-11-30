using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class HolidaySchedule
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string HolidayName { get; set; } = null!;

    public int HolidayTypeSystemId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateOnly HolidayDate { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual SystemParameter HolidayTypeSystem { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
