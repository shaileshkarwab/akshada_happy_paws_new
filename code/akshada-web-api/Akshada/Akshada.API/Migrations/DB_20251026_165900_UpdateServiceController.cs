using FluentMigrator;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Migrations
{
    [Migration(20251026165900)]
    public class DB_20251026_165900_UpdateServiceController : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Update.Table("menu_master")
                .Set(new { controller = "service-request" })
                .Where(new { id = 10 });
        }
    }
}
