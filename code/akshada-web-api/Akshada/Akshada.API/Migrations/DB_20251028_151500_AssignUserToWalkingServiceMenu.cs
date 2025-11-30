using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251028151500)]
    public class DB_20251028_151500_AssignUserToWalkingServiceMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 11,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Assign User",
                    seq_no = 2,
                    controller = DBNull.Value,
                    page = "list-assign-user-to-request",
                    fa_icon = "fas fa-user-plus",
                    parent_id = 10,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
