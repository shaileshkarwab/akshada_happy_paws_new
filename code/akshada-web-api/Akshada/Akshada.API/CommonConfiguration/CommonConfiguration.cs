using Akshada.DTO.Models;
using FluentMigrator.Runner;

namespace Akshada.API.CommonConfiguration
{
    public static class CommonConfiguration
    {
        public static IApplicationBuilder SetCommonConfiguration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();
            var appSection = configuration.GetSection("Appsettings");
            DTO_Configuration.DataUploadPath = appSection.GetValue<string>("DataUploadPath");
            DTO_Configuration.NoImagePath = appSection.GetValue<string>("NoImagePath");
            return app;
        }
    }
}
