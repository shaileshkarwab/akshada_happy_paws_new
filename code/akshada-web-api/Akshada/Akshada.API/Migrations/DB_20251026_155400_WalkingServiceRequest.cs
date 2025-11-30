using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251026155400)]
    public class DB_20251026_155400_WalkingServiceRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("walking_service_request")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_WSR_ROW_ID")
                .WithColumn("customer_id").AsInt32().NotNullable()
                .WithColumn("pet_id").AsInt32().NotNullable()
                .WithColumn("service_system_id").AsInt32().NotNullable()
                .WithColumn("frequency_system_id").AsInt32().NotNullable()
                .WithColumn("from_date").AsDateTime().NotNullable()
                .WithColumn("to_date").AsDateTime().NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            //customer id fk
            Create.ForeignKey("FK_WSR_CUSTOMER_ID")
                .FromTable("walking_service_request").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");

            //pet id fk
            Create.ForeignKey("FK_WSR_CUSTOMER_PET_ID")
                .FromTable("walking_service_request").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");

            // service id fk
            Create.ForeignKey("FK_WSR_SRV_SYSTEM_ID")
                .FromTable("walking_service_request").ForeignColumn("service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            // frequency system id
            Create.ForeignKey("FK_WSR_SRV_FREQ_SYSTEM_ID")
                .FromTable("walking_service_request").ForeignColumn("frequency_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            //created and updated by
            Create.ForeignKey("FK_WSR_CRT_BY")
                .FromTable("walking_service_request").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_WSR_UPDT_BY")
                .FromTable("walking_service_request").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");


            Create.Table("walking_service_request_days")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_WSRD_ROW_ID")
                .WithColumn("walking_service_request_master_id").AsInt32().NotNullable()
                .WithColumn("is_selected").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("week_day_name").AsString().NotNullable();

            //link WSR and WSR Days
            Create.ForeignKey("FK_WSR_WSRD_ID")
                .FromTable("walking_service_request_days").ForeignColumn("walking_service_request_master_id")
                .ToTable("walking_service_request").PrimaryColumn("id");

            Create.Table("walking_service_request_day_schedule")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_WSRD_SHD_ROW_ID")
                .WithColumn("walking_service_request_days_master_id").AsInt32().NotNullable()
                .WithColumn("from_time").AsTime().NotNullable()
                .WithColumn("to_time").AsTime().NotNullable();

            //link wsr schd and days
            Create.ForeignKey("FK_WSRD_SCH_WSRD_ID")
                .FromTable("walking_service_request_day_schedule").ForeignColumn("walking_service_request_days_master_id")
                .ToTable("walking_service_request_days").PrimaryColumn("id");
        }
    }
}
