using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251018195800)]
    public class DB_20251018_195800_AddUser : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("user_master")
                .Row(new { id=1,
                    row_id= "c2088ba5-7225-4474-8539-28c128442e4d",
                    first_name="Shailesh",
                    last_name="Karwa",
                    role_id=1,
                    login_name="shailesh.karwa@gmail.com",
                    status=true,
                    image_path= "c2088ba5-7225-4474-8539-28c128442e4d.png",
                    created_by=1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
