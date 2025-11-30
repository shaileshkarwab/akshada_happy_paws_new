using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class CustomerPet
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string? PetName { get; set; }

    public int? BreedSystemId { get; set; }

    public int? ColourId { get; set; }

    public int? PetAgeYear { get; set; }

    public int? PetAgeMonth { get; set; }

    public double? PetWeight { get; set; }

    public string? PetAndOwnerImage { get; set; }

    public string? PetVaccinationImage { get; set; }

    public string? PetPastIllness { get; set; }

    public DateTime? PetDateOfBirth { get; set; }

    public int? ImportDataId { get; set; }

    public int CustomerId { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDataComplete { get; set; }

    public int? NatureOfPetSystemId { get; set; }

    public virtual SystemParameter? BreedSystem { get; set; }

    public virtual SystemParameter? Colour { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual GoogleFormSubmission? GoogleFormSubmission { get; set; }

    public virtual ImportDatum? ImportData { get; set; }

    public virtual SystemParameter? NatureOfPetSystem { get; set; }

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequests { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecords { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRequest> WalkingServiceRequests { get; set; } = new List<WalkingServiceRequest>();
}
