using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251114113300)]
    public class DB_20251114_113300_NotificationSchedule : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("notification_schedule")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("notification_type_system_id").AsInt32().NotNullable()
                .WithColumn("before_days").AsInt16().NotNullable()
                .WithColumn("schedule_on_time").AsTime().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_NOTI_S_CRT_BY")
                .FromTable("notification_schedule").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_NOTI_S_UPDT_BY")
                .FromTable("notification_schedule").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_NOTI_SYS_ID")
                .FromTable("notification_schedule").ForeignColumn("notification_type_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
