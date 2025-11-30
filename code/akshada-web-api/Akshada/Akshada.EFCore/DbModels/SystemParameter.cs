using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class SystemParameter
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int EnumId { get; set; }

    public string ParamValue { get; set; } = null!;

    public string ParamAbbrivation { get; set; } = null!;

    public string? Identifier1 { get; set; }

    public string? Identifier2 { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool? Status { get; set; }

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<CustomerPet> CustomerPetBreedSystems { get; set; } = new List<CustomerPet>();

    public virtual ICollection<CustomerPet> CustomerPetColours { get; set; } = new List<CustomerPet>();

    public virtual ICollection<CustomerPet> CustomerPetNatureOfPetSystems { get; set; } = new List<CustomerPet>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<HolidaySchedule> HolidaySchedules { get; set; } = new List<HolidaySchedule>();

    public virtual ICollection<ImportantContactAddressDetail> ImportantContactAddressDetails { get; set; } = new List<ImportantContactAddressDetail>();

    public virtual ICollection<ImportantContact> ImportantContacts { get; set; } = new List<ImportantContact>();

    public virtual ICollection<OtherServiceRateDetail> OtherServiceRateDetails { get; set; } = new List<OtherServiceRateDetail>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequestAddressLocationSystems { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequestPetColourBreeds { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequestPetColourSystems { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequestRequiredServiceSystems { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<ServiceRateMasterDetail> ServiceRateMasterDetails { get; set; } = new List<ServiceRateMasterDetail>();

    public virtual ICollection<ServiceRateMaster> ServiceRateMasters { get; set; } = new List<ServiceRateMaster>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<VaccinationRecordDetail> VaccinationRecordDetails { get; set; } = new List<VaccinationRecordDetail>();

    public virtual ICollection<WalkingServiceRecordImage> WalkingServiceRecordImages { get; set; } = new List<WalkingServiceRecordImage>();

    public virtual ICollection<WalkingServiceRequest> WalkingServiceRequestFrequencySystems { get; set; } = new List<WalkingServiceRequest>();

    public virtual ICollection<WalkingServiceRequest> WalkingServiceRequestServiceSystems { get; set; } = new List<WalkingServiceRequest>();

    public virtual ICollection<WebsiteServiceProcess> WebsiteServiceProcessRequestNotParamSystems { get; set; } = new List<WebsiteServiceProcess>();

    public virtual ICollection<WebsiteServiceProcess> WebsiteServiceProcessServiceSystems { get; set; } = new List<WebsiteServiceProcess>();
}
