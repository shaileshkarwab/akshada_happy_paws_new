using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251022091300)]
    public class DB_20251022_091300_ServiceRate : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("service_rate_master")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("service_system_id").AsInt32().NotNullable()
                .WithColumn("entry_date").AsDate().NotNullable()
                .WithColumn("effective_date").AsDate().NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_SRM_USR_CRT_BY")
                .FromTable("service_rate_master").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_SRM_USR_UPDT_BY")
                .FromTable("service_rate_master").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_SRM_SYS_PARAM_SERVICE")
                .FromTable("service_rate_master").ForeignColumn("service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");


            Create.UniqueConstraint("UK_SRM_EFF_DATE_SERVICE_ID")
                .OnTable("service_rate_master")
                .Columns("service_system_id", "effective_date");

            Create.Table("service_rate_master_detail")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("service_master_id").AsInt32().NotNullable()
                .WithColumn("location_system_id").AsInt32().NotNullable()
                .WithColumn("regular_rate").AsDecimal().NotNullable()
                .WithColumn("special_day_rate").AsDecimal().NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable();

            Create.ForeignKey("FK_SRM_SRM_DTL_ID")
                .FromTable("service_rate_master_detail").ForeignColumn("service_master_id")
                .ToTable("service_rate_master").PrimaryColumn("id");

            Create.ForeignKey("FK_SRMD_SYS_PARAM_LOCATION")
                .FromTable("service_rate_master_detail").ForeignColumn("location_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
