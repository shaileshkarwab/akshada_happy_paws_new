using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class AssignOtherServiceRequestUser
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateTime AssignDate { get; set; }

    public int OtherServiceRequestMasterId { get; set; }

    public DateTime ChangedRequestDate { get; set; }

    public DateTime FromTime { get; set; }

    public DateTime ToTime { get; set; }

    public int AssignedToUserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Remarks { get; set; }

    public double ServiceCharge { get; set; }

    public bool? IsAmountToBeCollectedAfterService { get; set; }

    public virtual UserMaster AssignedToUser { get; set; } = null!;

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual OtherServiceRequest OtherServiceRequestMaster { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;
}
