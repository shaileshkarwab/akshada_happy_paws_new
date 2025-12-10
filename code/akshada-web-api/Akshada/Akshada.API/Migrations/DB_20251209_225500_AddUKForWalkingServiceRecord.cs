using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251209225500)]
    public class DB_20251209_225500_AddUKForWalkingServiceRecord : AutoReversingMigration
    {
        public override void Up()
        {
            Create.UniqueConstraint("UK_WSR_DATE_SCHEDULE")
                .OnTable("walking_service_record")
                .Columns("service_offered_date",
                "walking_service_master_id",
                "walking_service_day_master_id",
                "walking_service_day_schedule_master_id"
                );
        }
    }
}
