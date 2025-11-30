using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251123200301)]
    public class DB_20251123_200300_WebServiceRequestChanges : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("service_system_id")
                .OnTable("website_service_process")
                .AsInt32()
                .NotNullable();

            Create.ForeignKey("FK_WSP_SRV_SYSTEM_PARAM")
                .FromTable("website_service_process").ForeignColumn("service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
