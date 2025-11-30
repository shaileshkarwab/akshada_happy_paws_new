using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251103193800)]
    public class DB_20251103_193800_RequiredService_Request : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("required_service_system_id")
                 .OnTable("other_service_request")
                 .AsInt32()
                 .Nullable();

            Create.ForeignKey("FK_OSR_REQ_SRV_SYSTEM_ID")
                .FromTable("other_service_request").ForeignColumn("required_service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
