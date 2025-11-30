using FluentMigrator.Runner;

namespace Akshada.API.Migrations
{
    public static class Migrate
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app) {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
            return app;
        }
    }
}
