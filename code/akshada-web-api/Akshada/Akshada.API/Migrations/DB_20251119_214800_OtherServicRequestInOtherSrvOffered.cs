using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251119214800)]
    public class DB_20251119_214800_OtherServicRequestInOtherSrvOffered : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("other_service_request_master_id")
                .OnTable("other_services_offered")
                .AsInt32()
                .NotNullable();

            Create.ForeignKey("FK_OSOFF_OSREQ_ID")
                .FromTable("other_services_offered").ForeignColumn("other_service_request_master_id")
                .ToTable("other_service_request").PrimaryColumn("id");
        }
    }
}
