using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class VaccinationRecordDetail
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int VaccinationRecordMasterId { get; set; }

    public int VaccinationSystemId { get; set; }

    public DateOnly VaccinatedDate { get; set; }

    public DateOnly DueDate { get; set; }

    public virtual VaccinationRecord VaccinationRecordMaster { get; set; } = null!;

    public virtual SystemParameter VaccinationSystem { get; set; } = null!;
}
