using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251114112900)]
    public class DB_20251114_112900_NotificationScheduleMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 18,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Notification Schedule",
                    seq_no = 8,
                    controller = DBNull.Value,
                    page = "list-notification-schedule",
                    fa_icon = "fas fa-bell",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
