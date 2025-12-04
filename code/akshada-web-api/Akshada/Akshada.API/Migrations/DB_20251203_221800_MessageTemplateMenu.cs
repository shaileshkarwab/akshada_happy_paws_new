using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251203221800)]
    public class DB_20251203_221800_MessageTemplateMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 23,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Message Templates",
                    seq_no = 10,
                    controller = DBNull.Value,
                    page = "list-message-template",
                    fa_icon = "fas fa-envelope-square",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
