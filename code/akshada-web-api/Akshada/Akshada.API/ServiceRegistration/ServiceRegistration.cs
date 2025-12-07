using Akshada.API.AuthFilter;
using Akshada.Repository;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using Akshada.Services.Services;
using System.CodeDom;

namespace Akshada.API.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddScoped<IUserVerificationService, UserVerificationService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<BasicAuthorization>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ISystemParameterService, SystemParameterService>();
            services.AddScoped<ISrvRateService, SrvRateService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IImportDataService, ImportDataService>();
            services.AddSingleton<GoogleDriveService>();
            services.AddSingleton<CredentialFactory>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISrvRequestService, SrvRequestService>();
            services.AddScoped<IWalkingRecordService, WalkingRecordService>();
            services.AddScoped<IOtherSrvService, OtherSrvService>();
            services.AddScoped<IOtherServiceRequestService, OtherServiceRequestService>();
            services.AddScoped<IOtherServiceRate, OtherServiceRate>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IContactAndAddressService, ContactAndAddressService>();
            services.AddScoped<IVaccinationRecordService, VaccinationRecordService>();
            services.AddScoped<INotificationScheduleService, NotificationScheduleService>();
            services.AddScoped<IPetOtherSrvService, PetOtherSrvService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IRawSqlService, RawSqlService>();
            services.AddScoped<ICompanyInformationService, CompanyInformationService>();
            services.AddScoped(typeof(ITemplateService<>), typeof(TemplateService<>));
            services.AddScoped<HeaderAuthorization>();
            services.AddScoped<IWebsiteDataService, WebsiteDataService>();
            services.AddScoped<ISignalRService, SignalRService>();
            services.AddSignalR();
            services.AddScoped<GoogleMapService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
