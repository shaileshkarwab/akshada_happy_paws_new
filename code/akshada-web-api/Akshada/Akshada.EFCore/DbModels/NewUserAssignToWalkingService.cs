using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class NewUserAssignToWalkingService
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateTime AssignDate { get; set; }

    public int CustomerId { get; set; }

    public int PetId { get; set; }

    public int WalkingRequestMasterId { get; set; }

    public int WalkingRequestDayMasterId { get; set; }

    public int WalkingRequestScheduleMasterId { get; set; }

    public int UserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Remarks { get; set; }

    public DateTime? ChangeFromTime { get; set; }

    public DateTime? ChangeToTime { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomerPet Pet { get; set; } = null!;

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual UserMaster User { get; set; } = null!;

    public virtual WalkingServiceRequestDay WalkingRequestDayMaster { get; set; } = null!;

    public virtual WalkingServiceRequest WalkingRequestMaster { get; set; } = null!;

    public virtual WalkingServiceRequestDaySchedule WalkingRequestScheduleMaster { get; set; } = null!;
}
