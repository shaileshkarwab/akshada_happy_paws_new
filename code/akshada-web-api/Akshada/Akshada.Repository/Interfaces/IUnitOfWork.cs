using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository UserRepository { get; }
        IMenuMasterRepository MenuMasterRepository { get; }
        ISystemParamRepository SystemParamRepository { get; }

        IServiceRateRepository ServiceRateRepository { get; }
        IRolesRepository RolesRepository { get; }

        IImportDataRepository ImportDataRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IServiceRequestRepository ServiceRequestRepository { get; }

        IWalkingScheduleAssignUserRepo WalkingScheduleAssignUserRepo { get; }

        IWalkingServiceRequestRepo WalkingServiceRequestRepo { get; }

        IWalkingServiceRequestDayScheduleRepo WalkingServiceRequestDayScheduleRepo { get; }

        INewUserAssignToWalkingSrvRepo NewUserAssignToWalkingSrvRepo { get; }

        IWalkingServiceRequestDaysRepo WalkingServiceRequestDaysRepo { get; }

        IWalkingServiceRecordRepo WalkingServiceRecordRepo { get; }
        IWalkingServiceRecordImagesRepo WalkingServiceRecordImagesRepo { get; }

        ICustomerPetsRepository CustomerPetsRepository { get; }

        IOtherServiceRepository OtherServiceRepository { get; }
        IOtherServiceImageRepo OtherServiceImageRepo { get; }

        IOtherServiceRequestRepository OtherServiceRequestRepository { get; }

        IAssignOtherServiceRequestRepo AssignOtherServiceRequestRepo { get; }

        IOtherServiceRateDetailRepository OtherServiceRateDetailRepository { get; }
        IOtherServiceRateRepository OtherServiceRateRepository { get; }

        IHolidayScheduleRepository HolidayScheduleRepository { get; }

        IImportantContactRepo ImportantContactRepo { get; }

        IImportantContactAddDtlRepo ImportantContactAddDtlRepo { get; }

        IVaccinationRecordRepository VaccinationRecordRepository { get; }

        IVaccinationRecordDetailRepository VaccinationRecordDetailRepository { get; }

        INotificationScheduleRepository NotificationScheduleRepository { get; }

        ICompanyInfoRepository CompanyInfoRepository { get; }

        IWebsiteServiceRepository WebsiteServiceRepository { get; }

        IWebsiteServiceProcessRepository WebsiteServiceProcessRepository { get; }

        GoogleFormSubmissionRepository GoogleFormSubmissionRepository { get; }

        CompanyInformationUPIRepository CompanyInformationUPIRepository { get; }
        CompanyInformationBankAccountRepository CompanyInformationBankAccountRepository { get; }

        ServiceRateDetailRepository ServiceRateDetailRepository { get; }
        int Complete();
    }
}
