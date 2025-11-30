using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251031080401)]
    public class DB_20251031_080400_WalkingServiceRecord : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("walking_service_record")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("service_offered_date").AsDateTime().NotNullable()
                .WithColumn("from_time").AsDateTime().NotNullable()
                .WithColumn("to_time").AsDateTime().NotNullable()
                .WithColumn("customer_id").AsInt32().NotNullable()
                .WithColumn("pet_id").AsInt32().NotNullable()
                .WithColumn("walking_service_master_id").AsInt32().NotNullable()
                .WithColumn("walking_service_day_master_id").AsInt32().NotNullable()
                .WithColumn("walking_service_day_schedule_master_id").AsInt32().NotNullable()
                .WithColumn("remarks").AsString().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_WALK_SR_CRT_BY")
                .FromTable("walking_service_record").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_WALK_SR_UPDT_BY")
                .FromTable("walking_service_record").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //customer_id
            Create.ForeignKey("FK_WALK_SR_CUSTOMER_ID")
                .FromTable("walking_service_record").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");
            //pet_id
            Create.ForeignKey("FK_WALK_SR_CUSTOMER_PET_ID")
                .FromTable("walking_service_record").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");

            

            //walking_service_day_schedule_master_id
            Create.ForeignKey("FK_WALK_SR_CUSTOMER_WSREQ_DAY_SCH_ID")
                .FromTable("walking_service_record").ForeignColumn("walking_service_day_schedule_master_id")
                .ToTable("walking_service_request_day_schedule").PrimaryColumn("id");
        }
    }
}
