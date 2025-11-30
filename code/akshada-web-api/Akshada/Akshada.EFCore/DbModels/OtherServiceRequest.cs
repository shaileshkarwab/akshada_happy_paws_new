using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class OtherServiceRequest
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public DateTime ServiceRequestDate { get; set; }

    public DateTime ServiceRequiredOnDate { get; set; }

    public int? CustomerId { get; set; }

    public int? PetId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public int? AddressLocationSystemId { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? PetName { get; set; }

    public string? PetImage { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? RequiredServiceSystemId { get; set; }

    public int? PetColourSystemId { get; set; }

    public int? PetColourBreedId { get; set; }

    public string? CustomerAddressProof { get; set; }

    public int OtherServiceRequestMasterId { get; set; }

    public int? WebsiteServiceProcessMasterId { get; set; }

    public virtual SystemParameter? AddressLocationSystem { get; set; }

    public virtual AssignOtherServiceRequestUser? AssignOtherServiceRequestUser { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OtherServicesOffered> OtherServicesOffereds { get; set; } = new List<OtherServicesOffered>();

    public virtual CustomerPet? Pet { get; set; }

    public virtual SystemParameter? PetColourBreed { get; set; }

    public virtual SystemParameter? PetColourSystem { get; set; }

    public virtual SystemParameter? RequiredServiceSystem { get; set; }

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual WebsiteServiceProcess? WebsiteServiceProcessMaster { get; set; }
}
