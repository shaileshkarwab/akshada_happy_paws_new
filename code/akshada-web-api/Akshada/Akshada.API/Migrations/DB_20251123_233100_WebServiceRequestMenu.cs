using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251123233100)]
    public class DB_20251123_233100_WebServiceRequestMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 21,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Web Service Request",
                    seq_no = 5,
                    controller = DBNull.Value,
                    page = "list-all-web-service-request",
                    fa_icon = "fas fa-globe",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
