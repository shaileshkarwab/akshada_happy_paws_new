using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251028222601)]
    public class DB_20251028_222601_ServiceIdToAssignUser : AutoReversingMigration
    {
        public override void Up()
        {
            Create.ForeignKey("FK_WLK_SRV_MST_ASSIGN_USER")
                .FromTable("walking_service_request_day_schedule_assigned_to_user").ForeignColumn("walking_service_request_master_id")
                .ToTable("walking_service_request").PrimaryColumn("id");
        }
    }
}
