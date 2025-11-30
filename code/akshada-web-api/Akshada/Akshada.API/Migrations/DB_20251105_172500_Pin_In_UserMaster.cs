using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251105172500)]
    public class DB_20251105_172500_Pin_In_UserMaster : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("mobile_pin")
                .OnTable("user_master")
                .AsString()
                .Nullable();
        }
    }
}
