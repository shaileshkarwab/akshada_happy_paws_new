using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251029171800)]
    public class DB_20251029_171800_AssignNewUserForWalkingRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("remarks")
                .OnTable("new_user_assign_to_walking_service")
                .AsString()
                .Nullable();

            Create.UniqueConstraint("UK_USER_ID_DATE_WALKING_SCH")
                .OnTable("new_user_assign_to_walking_service")
                .Columns("assign_date", "walking_request_schedule_master_id");
        }
    }
}
