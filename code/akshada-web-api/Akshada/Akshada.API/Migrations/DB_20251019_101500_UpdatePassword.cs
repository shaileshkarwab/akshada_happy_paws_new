using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251019101500)]
    public class DB_20251019_101500_UpdatePassword : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Execute.Sql("update user_master set password = '$2a$11$DXvANeoltjkziUJt61Vo7.Pnjbck8X/Q5GWh2mOBFDDj3BleFL8z2' where id = 1 ");
        }
    }
}
