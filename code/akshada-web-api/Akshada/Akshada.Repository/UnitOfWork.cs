using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AkshadaPawsContext akshadaPawsContext;
        private readonly IServiceProvider services;
        public UnitOfWork(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services)
        {
            this.akshadaPawsContext = akshadaPawsContext;
            UserRepository = new UserRepository(akshadaPawsContext, configuration, services);
            MenuMasterRepository = new MenuMasterRepository(akshadaPawsContext, configuration, services);
            SystemParamRepository = new SystemParamRepository(akshadaPawsContext, configuration, services);
            ServiceRateRepository = new ServiceRateRepository(akshadaPawsContext, configuration, services);
            RolesRepository = new RolesRepository(akshadaPawsContext, configuration, services);
            ImportDataRepository = new ImportDataRepository(akshadaPawsContext, configuration, services);
            CustomerRepository = new CustomerRepository(akshadaPawsContext, configuration, services);
            ServiceRequestRepository = new ServiceRequestRepository(akshadaPawsContext, configuration, services);
            WalkingScheduleAssignUserRepo = new WalkingScheduleAssignUserRepo(akshadaPawsContext, configuration, services);
            WalkingServiceRequestRepo = new WalkingServiceRequestRepo(akshadaPawsContext, configuration, services);
            WalkingServiceRequestDayScheduleRepo = new WalkingServiceRequestDayScheduleRepo(akshadaPawsContext, configuration, services);
            NewUserAssignToWalkingSrvRepo = new NewUserAssignToWalkingSrvRepo(akshadaPawsContext, configuration, services);
            WalkingServiceRequestDaysRepo = new WalkingServiceRequestDaysRepo(akshadaPawsContext, configuration, services);

            WalkingServiceRecordRepo = new WalkingServiceRecordRepo(akshadaPawsContext, configuration, services);
            WalkingServiceRecordImagesRepo = new WalkingServiceRecordImagesRepo(akshadaPawsContext, configuration, services);

            CustomerPetsRepository = new CustomerPetsRepository(akshadaPawsContext, configuration, services);
            OtherServiceRepository = new OtherServiceRepository(akshadaPawsContext, configuration, services);
            OtherServiceImageRepo = new OtherServiceImageRepo(akshadaPawsContext, configuration, services);
            OtherServiceRequestRepository = new OtherServiceRequestRepository(akshadaPawsContext, configuration, services);
            AssignOtherServiceRequestRepo = new AssignOtherServiceRequestRepo(akshadaPawsContext, configuration, services);
            OtherServiceRateDetailRepository = new OtherServiceRateDetailRepository(akshadaPawsContext, configuration, services);
            OtherServiceRateRepository = new OtherServiceRateRepository(akshadaPawsContext, configuration, services);
            HolidayScheduleRepository = new HolidayScheduleRepository(akshadaPawsContext, configuration, services);
            ImportantContactRepo = new ImportantContactRepo(akshadaPawsContext, configuration, services);
            ImportantContactAddDtlRepo = new ImportantContactAddDtlRepo(akshadaPawsContext, configuration, services);
            VaccinationRecordRepository = new VaccinationRecordRepository(akshadaPawsContext, configuration, services);
            VaccinationRecordDetailRepository = new VaccinationRecordDetailRepository(akshadaPawsContext, configuration, services);
            NotificationScheduleRepository = new NotificationScheduleRepository(akshadaPawsContext, configuration, services);
            CompanyInfoRepository = new CompanyInfoRepository(akshadaPawsContext, configuration, services);
            WebsiteServiceRepository = new WebsiteServiceRepository(akshadaPawsContext, configuration, services);
            WebsiteServiceProcessRepository = new WebsiteServiceProcessRepository(akshadaPawsContext, configuration, services);
            GoogleFormSubmissionRepository = new GoogleFormSubmissionRepository(akshadaPawsContext, configuration, services);
            CompanyInformationUPIRepository = new CompanyInformationUPIRepository(akshadaPawsContext, configuration, services);
            CompanyInformationBankAccountRepository = new CompanyInformationBankAccountRepository(akshadaPawsContext, configuration, services);
            ServiceRateDetailRepository = new ServiceRateDetailRepository(akshadaPawsContext, configuration, services);
            EmailTemplateMasterRepository = new EmailTemplateMasterRepository(akshadaPawsContext, configuration, services);
            EmailTemplateMasterScheduleDetailRepository = new EmailTemplateMasterScheduleDetailRepository(akshadaPawsContext, configuration, services);
        }
        public IUserRepository UserRepository { get; private set; }
        public IMenuMasterRepository MenuMasterRepository { get; private set; }
        public ISystemParamRepository SystemParamRepository { get; private set; }

        public IServiceRateRepository ServiceRateRepository { get; private set; }
        public IRolesRepository RolesRepository { get; private set; }

        public IImportDataRepository ImportDataRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }
        public IServiceRequestRepository ServiceRequestRepository { get; private set; }

        public IWalkingScheduleAssignUserRepo WalkingScheduleAssignUserRepo { get; private set; }

        public IWalkingServiceRequestRepo WalkingServiceRequestRepo { get; private set; }

        public IWalkingServiceRequestDayScheduleRepo WalkingServiceRequestDayScheduleRepo { get; private set; }

        public INewUserAssignToWalkingSrvRepo NewUserAssignToWalkingSrvRepo { get; private set; }

        public IWalkingServiceRequestDaysRepo WalkingServiceRequestDaysRepo { get; private set; }

        public IWalkingServiceRecordRepo WalkingServiceRecordRepo { get; private set; }
        public IWalkingServiceRecordImagesRepo WalkingServiceRecordImagesRepo { get; private set; }

        public ICustomerPetsRepository CustomerPetsRepository { get; private set; }

        public IOtherServiceRepository OtherServiceRepository { get; private set; }
        public IOtherServiceImageRepo OtherServiceImageRepo { get; private set; }

        public IOtherServiceRequestRepository OtherServiceRequestRepository { get; private set; }

        public IAssignOtherServiceRequestRepo AssignOtherServiceRequestRepo { get; private set; }

        public IOtherServiceRateDetailRepository OtherServiceRateDetailRepository { get; private set; }
        public IOtherServiceRateRepository OtherServiceRateRepository { get; private set; }

        public IHolidayScheduleRepository HolidayScheduleRepository { get; private set; }

        public IImportantContactRepo ImportantContactRepo { get; private set; }

        public IImportantContactAddDtlRepo ImportantContactAddDtlRepo { get; private set; }

        public IVaccinationRecordRepository VaccinationRecordRepository { get; private set; }

        public IVaccinationRecordDetailRepository VaccinationRecordDetailRepository { get; private set; }

        public INotificationScheduleRepository NotificationScheduleRepository { get; private set; }

        public ICompanyInfoRepository CompanyInfoRepository { get; private set; }

        public IWebsiteServiceRepository WebsiteServiceRepository { get; private set; }
        public IWebsiteServiceProcessRepository WebsiteServiceProcessRepository { get; private set; }

        public GoogleFormSubmissionRepository GoogleFormSubmissionRepository { get; private set; }

        public CompanyInformationUPIRepository CompanyInformationUPIRepository { get; private set; }
        public CompanyInformationBankAccountRepository CompanyInformationBankAccountRepository { get; private set; }

        public ServiceRateDetailRepository ServiceRateDetailRepository { get; private set; }

        public EmailTemplateMasterRepository EmailTemplateMasterRepository { get; private set; }
        public EmailTemplateMasterScheduleDetailRepository EmailTemplateMasterScheduleDetailRepository { get; private set; }
        public int Complete()
        {
            return this.akshadaPawsContext.SaveChanges();
        }

        public void Dispose()
        {
            this.akshadaPawsContext.Dispose();
        }
    }
}
