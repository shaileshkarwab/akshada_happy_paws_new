using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251025215500)]
    public class DB_20251025_215500_AreaLocationInCustomer : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("area_location_system_id")
                .OnTable("customer")
                .AsInt32()
                .Nullable();

            Create.ForeignKey("FK_CUSTOMER_SYS_PARAM_LOCATION_ID")
                .FromTable("customer").ForeignColumn("area_location_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
