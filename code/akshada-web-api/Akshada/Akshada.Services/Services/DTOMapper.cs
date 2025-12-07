using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class DTOMapper:Profile
    {
        public DTOMapper() {
            CreateMap<UserMaster, DTO_UserDetail>()
                .ForMember(c=>c.IsMobilePinAvailable,d=>d.MapFrom(c=> !string.IsNullOrEmpty( c.MobilePin)))
                .ReverseMap();
            CreateMap<MenuMaster, DTO_MenuResponse>().ReverseMap();
            CreateMap<SystemParameter,DTO_SystemParameter>().ReverseMap();
            CreateMap<SystemParameter, DTO_LookUp>()
                .ForMember(d => d.LookUpDesc, s => s.MapFrom(c => c.ParamValue))
                .ForMember(d => d.RowId, s => s.MapFrom(c => c.RowId)).ReverseMap();

            CreateMap<DTO_ServiceRateMaster, ServiceRateMaster>().ReverseMap();
            CreateMap<DTO_ServiceRateMasterDetail,ServiceRateMasterDetail>().ReverseMap();
            CreateMap<DTO_User, UserMaster>().ReverseMap();
            CreateMap<RoleMaster, DTO_LookUp>()
                .ForMember(d => d.LookUpDesc, s => s.MapFrom(c => c.RoleName))
                .ForMember(d => d.RowId, s => s.MapFrom(c => c.RowId))
                .ReverseMap();

            CreateMap<ImportDatum,DTO_ImportData>().ReverseMap();
            CreateMap<Customer, DTO_Customer>().ReverseMap();
            CreateMap<CustomerPet, DTO_CustomerPet>().ReverseMap();
            CreateMap<CustomerPet, DTO_PetInformation>()
                .ForMember(c => c.CustomerName, s => s.MapFrom(d => d.Customer.CustomerName))
                .ForMember(c => c.Email, s => s.MapFrom(d => d.Customer.Email))
                .ForMember(c => c.Mobile, s => s.MapFrom(d => d.Customer.Mobile))
                .ForMember(c => c.Address, s => s.MapFrom(d => d.Customer.Address))
                .ForMember(c=>c.CustomerRowId,s=>s.MapFrom(d=>d.Customer.RowId))
                .ForMember(c=>c.AreaLocationSystem,s=>s.MapFrom(d=>d.Customer.AreaLocationSystem))
                .ReverseMap();

            CreateMap<DTO_WalkingServiceRequest, WalkingServiceRequest>().ReverseMap();
            CreateMap<DTO_WalkingServiceRequestDay, WalkingServiceRequestDay>().ReverseMap();
            CreateMap<DTO_WalkingServiceRequestDaySchedule, WalkingServiceRequestDaySchedule>().ReverseMap();
            CreateMap<WalkingServiceRequest, DTO_CustomerPetForActivity>()
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.ServiceSystem, d => d.MapFrom(s => s.ServiceSystem))
                .ForMember(c => c.ServiceFrequency, d => d.MapFrom(s => s.FrequencySystem))
                .ForMember(c => c.FromDate, d => d.MapFrom(s => s.FromDate))
                .ForMember(c => c.ToDate, d => d.MapFrom(s => s.ToDate))
                .ForMember(c => c.AreaLocationSystem, d => d.MapFrom(s => s.Customer.AreaLocationSystem))
                .ForMember(c => c.FromTime, d => d.MapFrom(s => s.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().FromTime))
                .ForMember(c => c.ToTime, d => d.MapFrom(s => s.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().ToTime))
                .ForMember(c=>c.AssignedToUser,d=>d.MapFrom(s=>s.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().WalkingServiceRequestDayScheduleAssignedToUsers.FirstOrDefault().User))
                .ForMember(c=>c.SelectedUser, d => d.MapFrom(s => s.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().WalkingServiceRequestDayScheduleAssignedToUsers.FirstOrDefault().User))
                .ForMember(c=>c.WalkingServiceRequestDaysId,d=>d.MapFrom(c=>c.WalkingServiceRequestDays.FirstOrDefault().RowId))
                .ForMember(c => c.WalkingServiceRequestDayScheduleId, d => d.MapFrom(c => c.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().RowId))
                .ForMember(c=>c.AssignRequestId,d=>d.MapFrom(c=>c.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().NewUserAssignToWalkingServices.FirstOrDefault().RowId))
                .ForMember(c=>c.NewUserAssignToWalkingServices, d => d.MapFrom(c => c.WalkingServiceRequestDays.FirstOrDefault().WalkingServiceRequestDaySchedules.FirstOrDefault().NewUserAssignToWalkingServices))
                .ReverseMap();

            CreateMap<WalkingServiceRequest, DTO_WalingServiceList>()
                .ForMember(c=>c.FrequencySystem, d=>d.MapFrom(s=>s.FrequencySystem))
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.ServiceSystem, d => d.MapFrom(s => s.ServiceSystem))
                .ForMember(c => c.FromDate, d => d.MapFrom(s => s.FromDate))
                .ForMember(c => c.ToDate, d => d.MapFrom(s => s.ToDate))
                .ForMember(c => c.PetInformation, d => d.MapFrom(s => s.Pet))
                .ForMember(c => c.CustomerInformation, d => d.MapFrom(s => s.Customer))
                .ReverseMap();

            CreateMap<Customer, DTO_CustomerList>().ReverseMap();
            CreateMap<WalkingServiceRequestDayScheduleAssignedToUser, DTO_WalkingServiceRequestDayScheduleAssignedToUserDisplay>()
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.User, d => d.MapFrom(s => s.User))
                .ReverseMap();

            CreateMap<UserMaster, DTO_LookUp>()
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.LookUpDesc, d => d.MapFrom(s => $"{s.FirstName}  {s.LastName}"))
                .ReverseMap();

            CreateMap<NewUserAssignToWalkingService,DTO_NewUserAssignToWalkingService>()
                .ForMember(c=>c.UserLookUp,d=>d.MapFrom(c=>c.User))
                .ReverseMap();

            CreateMap<WalkingServiceRecord, DTO_WalkingRecordList>()
                .ForMember(c=>c.ServiceDate,d=>d.MapFrom(s=>s.ServiceOfferedDate))
                .ForMember(c => c.FromTime, d => d.MapFrom(s => s.FromTime))
                .ForMember(c => c.ToTime, d => d.MapFrom(s => s.ToTime))
                .ForMember(c => c.Pet, d => d.MapFrom(s => s.Pet))
                .ForMember(c => c.Customer, d => d.MapFrom(s => s.Customer))
                .ForMember(c => c.ServiceProvidedBy, d => d.MapFrom(s => s.ServiceOfferedByUser))
                .ForMember(c=>c.RowId,d=>d.MapFrom(s=>s.RowId))
                .ForMember(c=>c.CustomerAddress,d=>d.MapFrom(s=>s.Customer.Address))
                .ForMember(c=>c.PetColour,d=>d.MapFrom(s=>s.Pet.Colour.ParamValue))
                .ForMember(c => c.PetBreed, d => d.MapFrom(s => s.Pet.BreedSystem.ParamValue))
                .ForMember(c=>c.ServiceSystem,d=>d.MapFrom(c=>c.WalkingServiceMaster.ServiceSystem))
                .ReverseMap();

            CreateMap<CustomerPet, DTO_LookUp>()
                .ForMember(c=>c.LookUpDesc,d=>d.MapFrom(s=>s.PetName))
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ReverseMap();

            CreateMap<Customer, DTO_LookUp>()
                .ForMember(c => c.LookUpDesc, d => d.MapFrom(s => s.CustomerName))
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ReverseMap();

            CreateMap<WalkingServiceRecord, DTO_WalkingRecord>()
                .ReverseMap();

            CreateMap<WalkingServiceRecordImage, DTO_WalkingServiceRecordImage>()
                .ForMember(c => c.UploadImageSystemParam, d => d.MapFrom(s => s.ImageUploadSystem))
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.ImageName, d => d.MapFrom(s => s.ImageName))
                .ReverseMap();


            CreateMap<OtherServicesOffered, DTO_OtherServicesOffered>().ReverseMap();
            CreateMap<OtherServiceRequest, DTO_OtherServiceRequest>()
                .ForMember(c=>c.Email,d=>d.MapFrom(c=>c.Customer == null?c.Email:c.Customer.Email))
                .ForMember(c => c.Mobile, d => d.MapFrom(c => c.Customer == null ? c.Mobile : c.Customer.Mobile))
                .ForMember(c => c.CustomerAddress, d => d.MapFrom(c => c.Customer == null ? c.CustomerAddress : c.Customer.Address))
                .ForMember(c=>c.ExecutionDate, d=>d.MapFrom(c=>c.AssignOtherServiceRequestUser.ChangedRequestDate))
                .ForMember(c=>c.ExecutionFromTime,d=>d.MapFrom(c=>c.AssignOtherServiceRequestUser.FromTime))
                .ForMember(c => c.ExecutionToTime, d => d.MapFrom(c => c.AssignOtherServiceRequestUser.ToTime))
                .ForMember(c => c.AssignedToUser, d => d.MapFrom(c => c.AssignOtherServiceRequestUser.AssignedToUser))
                .ForMember(c=>c.AssignRequestToUserRowId,d=>d.MapFrom(c=>c.AssignOtherServiceRequestUser.RowId))
                .ReverseMap();

            CreateMap<Akshada.EFCore.DbModels.OtherServiceRate, DTO_OtherServiceRateList>().ReverseMap();
            CreateMap<Akshada.EFCore.DbModels.OtherServiceRate, DTO_OtherServiceRate>().ReverseMap();
            CreateMap<Akshada.EFCore.DbModels.OtherServiceRateDetail, DTO_OtherServiceRateDetail>().ReverseMap();
            CreateMap<HolidaySchedule, DTO_HolidaySchedule>().ReverseMap();
            CreateMap<ImportantContact, DTO_ImportantContact>().ReverseMap();
            CreateMap<ImportantContactAddressDetail, DTO_ImportantContactAddressDetail>().ReverseMap();
            CreateMap<DTO_VaccinationSummary, VaccinationSummary>().ReverseMap();
            CreateMap<VaccinationRecord, DTO_VaccinationRecord>().ReverseMap();
            CreateMap<VaccinationRecordDetail, DTO_VaccinationRecordDetail>().ReverseMap();
            CreateMap<DTO_LookUp, ImportantContact>().ReverseMap()
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.LookUpDesc, d => d.MapFrom(s => s.ContactName));

            CreateMap<ServiceRateMaster, DTO_ServiceRateList>()
                .ForMember(c => c.EffectiveDate, d => d.MapFrom(s => s.EffectiveDate))
                .ForMember(c => c.EntryDate, d => d.MapFrom(s => s.EntryDate))
                .ForMember(c => c.RowId, d => d.MapFrom(s => s.RowId))
                .ForMember(c => c.ServiceName, d => d.MapFrom(s => s.ServiceSystem.ParamValue))
                .ReverseMap();

            CreateMap<NotificationSchedule, DTO_NotificationSchedule>().ReverseMap();
            CreateMap<DTO_CompanyInformation, CompanyInformation>().ReverseMap();

            CreateMap<GoogleFormSubmission,DTO_GoogleFormRawData>()
                .ForMember(c=>c.RowId, d=>d.MapFrom(s=>s.RowId))
                .ForMember(c=>c.JsonData,d=>d.MapFrom(s=>s.JsonData))
                .ForMember(c=>c.CustomerId, d=>d.MapFrom(s=>s.Customer.RowId))
                .ForMember(c => c.PetId, d => d.MapFrom(s => s.Pet.RowId))
                .ReverseMap();

            CreateMap<WebsiteService, DTO_ReciveFormSubmission>()
                .ForMember(c => c.JsonContent, d => d.MapFrom(c => c.JsonData))
                .ForMember(c => c.RowId, d => d.MapFrom(c => c.RowId))
                .ReverseMap();

            CreateMap<CompanyInformationBankAccount, DTO_CompanyInformationBankAccount>().ReverseMap();
            CreateMap<CompanyInformationUpiAccount, DTO_CompanyInformationUpiAccount>().ReverseMap();
            CreateMap<AssignOtherServiceRequestUser, DTO_AssignOtherServiceRequestUser>().ReverseMap();
            CreateMap<DTO_WalkingServiceRequestQuery, WalkingServiceRequestQuery>().ReverseMap();
            CreateMap<EmailTemplateMaster, DTO_EmailTemplateList>().ReverseMap();
            CreateMap<EmailTemplateMaster, DTO_EmailTemplateMaster>().ReverseMap();
            CreateMap<EmailTemplateMasterScheduleDetail, DTO_EmailTemplateMasterScheduleDetail>().ReverseMap();
        }
    }
}
