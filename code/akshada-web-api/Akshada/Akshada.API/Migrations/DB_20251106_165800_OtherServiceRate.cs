using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251106165800)]
    public class DB_20251106_165800_OtherServiceRate : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("other_service_rate")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().Unique().NotNullable()
                .WithColumn("entry_date").AsDateTime().NotNullable()
                .WithColumn("effective_date").AsDateTime().NotNullable().Unique("UK_OSRM_EFF_DATE")
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_OTH_SRV_RM_CRT_BY")
                .FromTable("other_service_rate").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_OTH_SRV_RM_UPDT_BY")
                .FromTable("other_service_rate").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.Table("other_service_rate_detail")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().Unique().NotNullable()
                .WithColumn("other_service_rate_master_id").AsInt32().NotNullable()
                .WithColumn("service_system_id").AsInt32().NotNullable()
                .WithColumn("chargeable_amount").AsDouble().NotNullable();

            //other_service_rate_master_id
            Create.ForeignKey("FK_OTH_SRV_RM_DETAIL_ID")
                .FromTable("other_service_rate_detail").ForeignColumn("other_service_rate_master_id")
                .ToTable("other_service_rate").PrimaryColumn("id");

            //service_system_id
            Create.ForeignKey("FK_OTH_SRV_RM_SRV_SYS_ID")
                .FromTable("other_service_rate_detail").ForeignColumn("service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            //create UK for rate detail
            Create.UniqueConstraint("UK_OTH_SRV_RT_DETAIL")
                .OnTable("other_service_rate_detail")
                .Columns("other_service_rate_master_id", "service_system_id");
        }
    }
}
