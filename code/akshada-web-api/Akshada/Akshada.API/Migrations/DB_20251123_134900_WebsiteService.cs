using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251123134903)]
    public class DB_20251123_134900_WebsiteService : AutoReversingMigration
    {
        public override void Up()
        {
            if (!Schema.Table("website_service").Exists())
            {
                Create.Table("website_service")
                    .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                    .WithColumn("row_id").AsString().NotNullable().Unique("UK_WEB_SRV_ROW_ID")
                    .WithColumn("body").AsString().NotNullable()
                    .WithColumn("json_data").AsString().NotNullable();
            }

            Create.Table("website_service_process")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_WEB_SRV_ROW_ID")
                .WithColumn("website_service_master_id").AsInt32().NotNullable()
                .WithColumn("request_accepted").AsBoolean().NotNullable()
                .WithColumn("request_not_param_system_id").AsInt32().Nullable()
                .WithColumn("customer_name").AsString().Nullable()
                .WithColumn("mobile_contact_number").AsString().Nullable()
                .WithColumn("service_date").AsDateTime().Nullable()
                .WithColumn("service_from_time").AsDateTime().Nullable()
                .WithColumn("service_to_time").AsDateTime().Nullable()
                .WithColumn("assigned_to_user_id").AsInt32().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_WEB_SRV_PRO_CRT_BY")
                .FromTable("website_service_process").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_WEB_SRV_PRO_UPD_BY")
               .FromTable("website_service_process").ForeignColumn("updated_by")
               .ToTable("website_service").PrimaryColumn("id");


            Create.ForeignKey("FK_WEB_SRV_PRO_ID")
                .FromTable("website_service_process").ForeignColumn("website_service_master_id")
                .ToTable("website_service").PrimaryColumn("id");


            Create.ForeignKey("FK_WEB_SRV_PRO_ASSIGNED_TO_ID")
               .FromTable("website_service_process").ForeignColumn("assigned_to_user_id")
               .ToTable("user_master").PrimaryColumn("id");


            Create.ForeignKey("FK_WEB_SRV_SYS_PARAM_NOT_ACCEPTED")
               .FromTable("website_service_process").ForeignColumn("request_not_param_system_id")
               .ToTable("system_parameter").PrimaryColumn("id");

        }
    }
}
