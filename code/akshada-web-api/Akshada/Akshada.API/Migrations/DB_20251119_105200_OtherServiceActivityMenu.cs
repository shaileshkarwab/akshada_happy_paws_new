using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251119105200)]
    public class DB_20251119_105200_OtherServiceActivityMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 19,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Today's Activity (Other)",
                    seq_no = 4,
                    controller = DBNull.Value,
                    page = "list-daily-activity-other-service",
                    fa_icon = "fas fa-bath",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
