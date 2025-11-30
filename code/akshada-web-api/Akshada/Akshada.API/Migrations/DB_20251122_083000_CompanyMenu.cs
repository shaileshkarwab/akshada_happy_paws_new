using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251122083000)]
    public class DB_20251122_083000_CompanyMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 20,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Company Information",
                    seq_no = 9,
                    controller = DBNull.Value,
                    page = "manage-company-information",
                    fa_icon = "fas fa-building",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
