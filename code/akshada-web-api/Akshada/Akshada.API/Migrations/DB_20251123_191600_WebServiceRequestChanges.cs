using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251123191601)]
    public class DB_20251123_191600_WebServiceRequestChanges : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("email")
                .OnTable("website_service_process")
                .AsString()
                .Nullable();

            Create.Column("remarks")
                .OnTable("website_service_process")
                .AsString()
                .Nullable();
        }
    }
}
