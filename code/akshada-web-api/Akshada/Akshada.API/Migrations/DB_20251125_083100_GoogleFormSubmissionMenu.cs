using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251125083100)]
    public class DB_20251125_083100_GoogleFormSubmissionMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 22,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Google Form Request(s)",
                    seq_no = 6,
                    controller = DBNull.Value,
                    page = "list-all-google-service-request",
                    fa_icon = "fab fa-google",
                    parent_id = 5,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
