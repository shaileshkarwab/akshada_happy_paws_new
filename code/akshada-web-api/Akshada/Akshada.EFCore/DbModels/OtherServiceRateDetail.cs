using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class OtherServiceRateDetail
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int OtherServiceRateMasterId { get; set; }

    public int ServiceSystemId { get; set; }

    public double ChargeableAmount { get; set; }

    public virtual OtherServiceRate OtherServiceRateMaster { get; set; } = null!;

    public virtual SystemParameter ServiceSystem { get; set; } = null!;
}
