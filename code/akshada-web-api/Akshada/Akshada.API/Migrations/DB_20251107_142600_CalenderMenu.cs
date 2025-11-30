using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107142600)]
    public class DB_20251107_142600_CalenderMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 15,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Festival & Holidays",
                    seq_no = 1,
                    controller = DBNull.Value,
                    page = "list-festival-holidays",
                    fa_icon = "fas fa-calendar-alt",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
