using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class UserMaster
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int RoleId { get; set; }

    public string LoginName { get; set; } = null!;

    public bool? Status { get; set; }

    public string? ImagePath { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Password { get; set; } = null!;

    public string? MobilePin { get; set; }

    public virtual ICollection<AssignOtherServiceRequestUser> AssignOtherServiceRequestUserAssignedToUsers { get; set; } = new List<AssignOtherServiceRequestUser>();

    public virtual ICollection<AssignOtherServiceRequestUser> AssignOtherServiceRequestUserCreatedByNavigations { get; set; } = new List<AssignOtherServiceRequestUser>();

    public virtual ICollection<AssignOtherServiceRequestUser> AssignOtherServiceRequestUserUpdatedByNavigations { get; set; } = new List<AssignOtherServiceRequestUser>();

    public virtual ICollection<CompanyInformation> CompanyInformationCreatedByNavigations { get; set; } = new List<CompanyInformation>();

    public virtual ICollection<CompanyInformation> CompanyInformationUpdatedByNavigations { get; set; } = new List<CompanyInformation>();

    public virtual UserMaster CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Customer> CustomerCreatedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<Customer> CustomerUpdatedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<HolidaySchedule> HolidayScheduleCreatedByNavigations { get; set; } = new List<HolidaySchedule>();

    public virtual ICollection<HolidaySchedule> HolidayScheduleUpdatedByNavigations { get; set; } = new List<HolidaySchedule>();

    public virtual ICollection<ImportDatum> ImportDatumCreatedByNavigations { get; set; } = new List<ImportDatum>();

    public virtual ICollection<ImportDatum> ImportDatumUpdatedByNavigations { get; set; } = new List<ImportDatum>();

    public virtual ICollection<ImportantContactAddressDetail> ImportantContactAddressDetailCreatedByNavigations { get; set; } = new List<ImportantContactAddressDetail>();

    public virtual ICollection<ImportantContactAddressDetail> ImportantContactAddressDetailUpdatedByNavigations { get; set; } = new List<ImportantContactAddressDetail>();

    public virtual ICollection<ImportantContact> ImportantContactCreatedByNavigations { get; set; } = new List<ImportantContact>();

    public virtual ICollection<ImportantContact> ImportantContactUpdatedByNavigations { get; set; } = new List<ImportantContact>();

    public virtual ICollection<UserMaster> InverseCreatedByNavigation { get; set; } = new List<UserMaster>();

    public virtual ICollection<UserMaster> InverseUpdatedByNavigation { get; set; } = new List<UserMaster>();

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServiceCreatedByNavigations { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServiceUpdatedByNavigations { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<NewUserAssignToWalkingService> NewUserAssignToWalkingServiceUsers { get; set; } = new List<NewUserAssignToWalkingService>();

    public virtual ICollection<NotificationSchedule> NotificationScheduleCreatedByNavigations { get; set; } = new List<NotificationSchedule>();

    public virtual ICollection<NotificationSchedule> NotificationScheduleUpdatedByNavigations { get; set; } = new List<NotificationSchedule>();

    public virtual ICollection<OtherServiceRate> OtherServiceRateCreatedByNavigations { get; set; } = new List<OtherServiceRate>();

    public virtual ICollection<OtherServiceRate> OtherServiceRateUpdatedByNavigations { get; set; } = new List<OtherServiceRate>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequestCreatedByNavigations { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<OtherServiceRequest> OtherServiceRequestUpdatedByNavigations { get; set; } = new List<OtherServiceRequest>();

    public virtual ICollection<OtherServicesOffered> OtherServicesOfferedCreatedByNavigations { get; set; } = new List<OtherServicesOffered>();

    public virtual ICollection<OtherServicesOffered> OtherServicesOfferedServiceOfferedUsers { get; set; } = new List<OtherServicesOffered>();

    public virtual ICollection<OtherServicesOffered> OtherServicesOfferedUpdatedByNavigations { get; set; } = new List<OtherServicesOffered>();

    public virtual RoleMaster Role { get; set; } = null!;

    public virtual ICollection<RoleMaster> RoleMasterCreatedByNavigations { get; set; } = new List<RoleMaster>();

    public virtual ICollection<RoleMaster> RoleMasterUpdatedByNavigations { get; set; } = new List<RoleMaster>();

    public virtual ICollection<ServiceRateMaster> ServiceRateMasterCreatedByNavigations { get; set; } = new List<ServiceRateMaster>();

    public virtual ICollection<ServiceRateMaster> ServiceRateMasterUpdatedByNavigations { get; set; } = new List<ServiceRateMaster>();

    public virtual ICollection<SystemParameter> SystemParameterCreatedByNavigations { get; set; } = new List<SystemParameter>();

    public virtual ICollection<SystemParameter> SystemParameterUpdatedByNavigations { get; set; } = new List<SystemParameter>();

    public virtual UserMaster UpdatedByNavigation { get; set; } = null!;

    public virtual ICollection<VaccinationRecord> VaccinationRecordCreatedByNavigations { get; set; } = new List<VaccinationRecord>();

    public virtual ICollection<VaccinationRecord> VaccinationRecordUpdatedByNavigations { get; set; } = new List<VaccinationRecord>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecordCreatedByNavigations { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecordServiceOfferedByUsers { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRecord> WalkingServiceRecordUpdatedByNavigations { get; set; } = new List<WalkingServiceRecord>();

    public virtual ICollection<WalkingServiceRequest> WalkingServiceRequestCreatedByNavigations { get; set; } = new List<WalkingServiceRequest>();

    public virtual ICollection<WalkingServiceRequestDayScheduleAssignedToUser> WalkingServiceRequestDayScheduleAssignedToUserCreatedByNavigations { get; set; } = new List<WalkingServiceRequestDayScheduleAssignedToUser>();

    public virtual ICollection<WalkingServiceRequestDayScheduleAssignedToUser> WalkingServiceRequestDayScheduleAssignedToUserUpdatedByNavigations { get; set; } = new List<WalkingServiceRequestDayScheduleAssignedToUser>();

    public virtual ICollection<WalkingServiceRequestDayScheduleAssignedToUser> WalkingServiceRequestDayScheduleAssignedToUserUsers { get; set; } = new List<WalkingServiceRequestDayScheduleAssignedToUser>();

    public virtual ICollection<WalkingServiceRequest> WalkingServiceRequestUpdatedByNavigations { get; set; } = new List<WalkingServiceRequest>();

    public virtual ICollection<WebsiteServiceProcess> WebsiteServiceProcessAssignedToUsers { get; set; } = new List<WebsiteServiceProcess>();

    public virtual ICollection<WebsiteServiceProcess> WebsiteServiceProcessCreatedByNavigations { get; set; } = new List<WebsiteServiceProcess>();
}
