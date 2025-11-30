using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251118112200)]
    public class DB_20251118_112200_ColumNameChangeInNotification : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Rename.Column("notification_type_system_id")
                .OnTable("notification_schedule")
                .To("notification_enum_id");

            if (Schema.Table("notification_schedule").Constraint("FK_NOTI_SYS_ID").Exists())
            {
                Delete.ForeignKey("FK_NOTI_SYS_ID")
                    .OnTable("notification_schedule");
            }

        }
    }
}
