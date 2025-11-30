using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251106163400)]
    public class DB_20251106_163400_OtherServiceRateMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                 .Row(new {
                     id = 14,
                     row_id = System.Guid.NewGuid().ToString(),
                     menu_text = "Other Service Rates",
                     seq_no = 5,
                     controller = DBNull.Value,
                     page = "list-other-service-rates",
                     fa_icon = "fas fa-hand-holding-usd",
                     parent_id = 1,
                     created_by = 1,
                     created_at = System.DateTime.UtcNow,
                     updated_by = 1,
                     updated_at = System.DateTime.UtcNow
                 });
        }
    }
}
