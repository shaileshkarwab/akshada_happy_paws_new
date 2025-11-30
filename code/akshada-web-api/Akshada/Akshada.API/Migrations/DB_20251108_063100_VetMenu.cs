using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251108063100)]
    public class DB_20251108_063100_VetMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 17,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Important Contacts",
                    seq_no = 7,
                    controller = DBNull.Value,
                    page = "list-important-contacts",
                    fa_icon = "fas fa-hospital-user",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
