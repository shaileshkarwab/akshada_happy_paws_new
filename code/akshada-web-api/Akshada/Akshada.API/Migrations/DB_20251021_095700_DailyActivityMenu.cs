using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251021095700)]
    public class DB_20251021_095700_DailyActivityMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 5,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Daily Activity",
                    seq_no = 2,
                    controller = "daily-activity",
                    page = "#",
                    fa_icon = "fa-plus",
                    parent_id = DBNull.Value,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });

            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 6,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Assign Pet",
                    seq_no = 1,
                    controller = DBNull.Value,
                    page = "list-assign-pets-to-user",
                    fa_icon = "fa-plus",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });

            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 7,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Today's Activity",
                    seq_no = 2,
                    controller = DBNull.Value,
                    page = "list-todays-activity",
                    fa_icon = "fa-plus",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
