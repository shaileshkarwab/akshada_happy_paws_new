using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251030095300)]
    public class DB_20251030_095300_ChangeTimeInAssignNewForWalking : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("change_from_time")
                .OnTable("new_user_assign_to_walking_service")
                .AsDateTime()
                .Nullable();

            Create.Column("change_to_time")
                .OnTable("new_user_assign_to_walking_service")
                .AsDateTime()
                .Nullable();
        }
    }
}
