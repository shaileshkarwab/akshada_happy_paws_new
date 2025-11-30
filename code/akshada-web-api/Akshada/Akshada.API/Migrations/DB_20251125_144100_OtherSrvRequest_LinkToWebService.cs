using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251125144100)]
    public class DB_20251125_144100_OtherSrvRequest_LinkToWebService : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("website_service_process_master_id")
                .OnTable("other_service_request")
                .AsInt32()
                .Nullable();

            Create.ForeignKey("FK_OTH_SRV_REQ_WEB_SRV_PROC")
                .FromTable("other_service_request").ForeignColumn("website_service_process_master_id")
                .ToTable("website_service_process").PrimaryColumn("id");
        }
    }
}
