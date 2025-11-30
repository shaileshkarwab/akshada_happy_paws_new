using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251023185900)]
    public class DB_20251023_185900_CustomerMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 8,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Customers",
                    seq_no = 4,
                    controller = DBNull.Value,
                    page = "list-customers",
                    fa_icon = "fas fa-user-tie",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
