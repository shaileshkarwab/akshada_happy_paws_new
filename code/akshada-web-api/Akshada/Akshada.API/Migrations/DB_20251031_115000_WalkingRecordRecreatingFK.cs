using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251031115000)]
    public class DB_20251031_115000_WalkingRecordRecreatingFK : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            if (Schema.Table("walking_service_record").Constraint("FK_WALK_SR_CUSTOMER_WSREQ_ID").Exists())
            {
                Delete.ForeignKey("FK_WALK_SR_CUSTOMER_WSREQ_ID")
                    .OnTable("walking_service_record");

                //walking_service_master_id
                Create.ForeignKey("FK_WALK_SR_CUSTOMER_WSREQ_ID")
                    .FromTable("walking_service_record").ForeignColumn("walking_service_master_id")
                    .ToTable("walking_service_request").PrimaryColumn("id");
            }


            if (Schema.Table("walking_service_record").Constraint("FK_WALK_SR_CUSTOMER_WSREQ_DAY_ID").Exists())
            {
                Delete.ForeignKey("FK_WALK_SR_CUSTOMER_WSREQ_DAY_ID")
                .OnTable("walking_service_record");



                //walking_service_day_master_id
                Create.ForeignKey("FK_WALK_SR_CUSTOMER_WSREQ_DAY_ID")
                    .FromTable("walking_service_record").ForeignColumn("walking_service_day_master_id")
                    .ToTable("walking_service_request_days").PrimaryColumn("id");
            }
        }
    }
}
