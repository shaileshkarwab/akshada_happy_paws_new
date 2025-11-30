using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251028222600)]
    public class DB_20251028_222600_ServiceIdToAssignUser : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("walking_service_request_master_id")
                .OnTable("walking_service_request_day_schedule_assigned_to_user")
                .AsInt32()
                .NotNullable();
        }
    }
}
