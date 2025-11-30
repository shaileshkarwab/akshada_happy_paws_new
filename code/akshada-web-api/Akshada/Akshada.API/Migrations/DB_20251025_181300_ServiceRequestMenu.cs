using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251025181300)]
    public class DB_20251025_181300_ServiceRequestMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 9,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Service Request",
                    seq_no = 3,
                    controller = DBNull.Value,
                    page = "list-service-request",
                    fa_icon = "fab fa-servicestack",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
