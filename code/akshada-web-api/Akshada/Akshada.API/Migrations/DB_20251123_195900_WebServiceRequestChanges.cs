using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251123195901)]
    public class DB_20251123_195900_WebServiceRequestChanges : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("address")
                 .OnTable("website_service_process")
                 .AsString()
                 .Nullable();
        }
    }
}
