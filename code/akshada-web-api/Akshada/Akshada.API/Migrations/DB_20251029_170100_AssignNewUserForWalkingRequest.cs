using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251029170100)]
    public class DB_20251029_170100_AssignNewUserForWalkingRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("new_user_assign_to_walking_service")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("assign_date").AsDateTime().NotNullable()
                .WithColumn("customer_id").AsInt32().NotNullable()
                .WithColumn("pet_id").AsInt32().NotNullable()
                .WithColumn("walking_request_master_id").AsInt32().NotNullable()
                .WithColumn("walking_request_day_master_id").AsInt32().NotNullable()
                .WithColumn("walking_request_schedule_master_id").AsInt32().NotNullable()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_NUATWS_USR_CRT_BY")
                .FromTable("new_user_assign_to_walking_service").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_NUATWS_USR_UPDT_BY")
                .FromTable("new_user_assign_to_walking_service").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //customer_id
            Create.ForeignKey("FK_NUATWS_CUSTOMER_ID")
                .FromTable("new_user_assign_to_walking_service").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");

            //pet_id
            Create.ForeignKey("FK_NUATWS_PET_ID")
                .FromTable("new_user_assign_to_walking_service").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");


            //walking_request_master_id
            Create.ForeignKey("FK_NUATWS_WRM_ID")
                .FromTable("new_user_assign_to_walking_service").ForeignColumn("walking_request_master_id")
                .ToTable("walking_service_request").PrimaryColumn("id");

            //walking_request_day_master_id
            Create.ForeignKey("FK_NUATWS_WRMDAY_ID")
               .FromTable("new_user_assign_to_walking_service").ForeignColumn("walking_request_day_master_id")
               .ToTable("walking_service_request_days").PrimaryColumn("id");


            //walking_request_schedule_master_id
            Create.ForeignKey("FK_NUATWS_WRMDAY_SCH_ID")
               .FromTable("new_user_assign_to_walking_service").ForeignColumn("walking_request_schedule_master_id")
               .ToTable("walking_service_request_day_schedule").PrimaryColumn("id");

            //user_id
            Create.ForeignKey("FK_NUATWS_USR_ID")
               .FromTable("new_user_assign_to_walking_service").ForeignColumn("user_id")
               .ToTable("user_master").PrimaryColumn("id");

        }
    }
}
