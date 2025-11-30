using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class Customer
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? AreaLocationSystemId { get; set; }

    public string? AddressProofImage { get; set; }

    public virtual SystemParameter? AreaLocationSystem { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CustomerPet> CustomerPets { get; set; } = new List<CustomerPet>();

    public virtual ICollection<GoogleFormSubmission> GoogleFormSubmissions { get; set; } = new List<GoogleFormSubmission>();

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequests { get; set; } = new List<OtherServiceRequest>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecords { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRequest> WalkingServiceRequests { get; set; } = new List<WalkingServiceRequest>();
}
