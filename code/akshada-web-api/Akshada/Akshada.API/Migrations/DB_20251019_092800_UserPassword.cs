using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251019092800)]
    public class DB_20251019_092800_UserPassword : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("user_master")
                .AddColumn("password").AsString().NotNullable().WithDefaultValue("Admin@1234");
        }
    }
}
