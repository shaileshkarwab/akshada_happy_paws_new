using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251028210700)]
    public class DB_20251028_210700_AssignUserToWalkingServiceRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("walking_service_request_day_schedule_assigned_to_user")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_WSRD_ASSIGN_USER_ROW_ID")
                .WithColumn("walking_service_request_day_schedule_id").AsInt32().NotNullable()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();


            Create.ForeignKey("FK_WSRDSATU_USR_CRT_BY")
                .FromTable("walking_service_request_day_schedule_assigned_to_user").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_WSRDSATU_USR_UPDT_BY")
                .FromTable("walking_service_request_day_schedule_assigned_to_user").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_WSRDSH_ASSIGN_USER_SCH_ID")
                .FromTable("walking_service_request_day_schedule_assigned_to_user").ForeignColumn("walking_service_request_day_schedule_id")
                .ToTable("walking_service_request_day_schedule").PrimaryColumn("id");

            Create.ForeignKey("FK_WSRDSH_ASSIGN_USER_ID")
                .FromTable("walking_service_request_day_schedule_assigned_to_user").ForeignColumn("user_id")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
