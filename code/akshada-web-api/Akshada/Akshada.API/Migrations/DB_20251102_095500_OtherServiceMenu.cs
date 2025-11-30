using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251102095500)]
    public class DB_20251102_095500_OtherServiceMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 13,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Other Services",
                    seq_no = 4,
                    controller = DBNull.Value,
                    page = "list-other-services",
                    fa_icon = "fas fa-bone",
                    parent_id = 10,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
