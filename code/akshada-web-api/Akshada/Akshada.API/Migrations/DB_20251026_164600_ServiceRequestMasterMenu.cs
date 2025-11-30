using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251026164600)]
    public class DB_20251026_164600_ServiceRequestMasterMenu : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 10,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Services",
                    seq_no = 1,
                    controller = "services",
                    page = "#",
                    fa_icon = "fab fa-servicestack",
                    parent_id = DBNull.Value,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });

            Update.Table("menu_master")
                .Set(new { parent_id = 10 })
                .Where(new { id = 9 });
        }
    }
}
