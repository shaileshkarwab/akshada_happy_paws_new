using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251204161800)]
    public class DB_20251204_161800_NotificationTemplates : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("email_template_master")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_ETM_ROW_ID")
                .WithColumn("email_notification_system_id").AsInt32().NotNullable()
                .WithColumn("email_event_name").AsString().NotNullable()
                .WithColumn("email_event_date").AsDate().NotNullable()
                .WithColumn("email_event_repeat_for_every_year").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_ETM_CRT_BY")
                .FromTable("email_template_master").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_ETM_UPD_BY")
               .FromTable("email_template_master").ForeignColumn("updated_by")
               .ToTable("user_master").PrimaryColumn("id");


            Create.ForeignKey("FK_ETM_SYS_PARAM_ID")
               .FromTable("email_template_master").ForeignColumn("email_notification_system_id")
               .ToTable("system_parameter").PrimaryColumn("id");


            Create.Table("email_template_master_schedule_detail")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_ETMSD_ROW_ID")
                .WithColumn("email_template_master_id").AsInt32().NotNullable()
                .WithColumn("reminder_days").AsInt16().NotNullable()
                .WithColumn("time_for_notification").AsTime().NotNullable();

            Create.ForeignKey("FK_ETM_ETMSD_ID")
               .FromTable("email_template_master_schedule_detail").ForeignColumn("email_template_master_id")
               .ToTable("email_template_master").PrimaryColumn("id");

        }
    }
}
