using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WebsiteServiceProcess
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int WebsiteServiceMasterId { get; set; }

    public bool RequestAccepted { get; set; }

    public int? RequestNotParamSystemId { get; set; }

    public string? CustomerName { get; set; }

    public string? MobileContactNumber { get; set; }

    public DateTime? ServiceDate { get; set; }

    public DateTime? ServiceFromTime { get; set; }

    public DateTime? ServiceToTime { get; set; }

    public int? AssignedToUserId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Email { get; set; }

    public string? Remarks { get; set; }

    public string? Address { get; set; }

    public int ServiceSystemId { get; set; }

    public virtual UserMaster? AssignedToUser { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OtherServiceRequest> OtherServiceRequests { get; set; } = new List<OtherServiceRequest>();

    public virtual SystemParameter? RequestNotParamSystem { get; set; }

    public virtual SystemParameter ServiceSystem { get; set; } = null!;

    public virtual WebsiteService UpdatedByNavigation { get; set; } = null!;

    public virtual WebsiteService WebsiteServiceMaster { get; set; } = null!;
}
