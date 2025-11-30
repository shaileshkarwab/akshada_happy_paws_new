using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251018200800)]
    public class DB_20251018_200800_Insert_Admin_Role : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("role_master")
                .Row(new {
                    id =1,
                    row_id = "0e786086-051e-42f4-9cef-ccd5c0b66fc6",
                    role_name = "Administrator",
                    status = true,
                    created_by =1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow,
                });
        }
    }
}
