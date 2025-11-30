using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251031083000)]
    public class DB_20251031_083000_MenuWalkingServiceRecord : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 12,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Walking Service Record",
                    seq_no = 2,
                    controller = DBNull.Value,
                    page = "list-walking-service-record",
                    fa_icon = "fas fa-book",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
