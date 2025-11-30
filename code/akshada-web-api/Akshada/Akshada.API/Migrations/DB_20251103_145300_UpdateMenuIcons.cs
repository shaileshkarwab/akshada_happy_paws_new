using FluentMigrator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Akshada.API.Migrations
{
    [Migration(20251103145300)]
    public class DB_20251103_145300_UpdateMenuIcons : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            //update icons
            Update.Table("menu_master")
                .Set(new { fa_icon = "fas fa-ruler-combined" })
                .Where(new { id = 1 });

            Update.Table("menu_master")
                .Set(new { fa_icon = "fas fa-tasks" })
                .Where(new { id = 2 });

            Update.Table("menu_master")
               .Set(new { fa_icon = "fas fa-users" })
               .Where(new { id = 3 });


            Update.Table("menu_master")
               .Set(new { fa_icon = "fas fa-calendar-day" })
               .Where(new { id = 5 });

            Update.Table("menu_master")
               .Set(new { fa_icon = "fas fa-user-tag" })
               .Where(new { id = 11 });

            Update.Table("menu_master")
              .Set(new { fa_icon = "fas fa-calendar-day" })
              .Where(new { id = 7 });
        }
    }
}
