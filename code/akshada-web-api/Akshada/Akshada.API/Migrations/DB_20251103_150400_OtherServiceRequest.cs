using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251103150402)]
    public class DB_20251103_150400_OtherServiceRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("other_service_request")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("service_request_date").AsDateTime().NotNullable()
                .WithColumn("service_required_on_date").AsDateTime().NotNullable()
                .WithColumn("customer_id").AsInt32().Nullable()
                .WithColumn("pet_id").AsInt32().Nullable()
                .WithColumn("customer_name").AsString().Nullable()
                .WithColumn("customer_address").AsString().Nullable()
                .WithColumn("address_location_system_id").AsInt32().Nullable()
                .WithColumn("mobile").AsString().Nullable()
                .WithColumn("email").AsString().Nullable()
                .WithColumn("pet_name").AsString().Nullable()
                .WithColumn("pet_image").AsString().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_OTSRV_REQUEST_SR_CRT_BY")
                .FromTable("other_service_request").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_OTSRV_REQUEST_UPDT_BY")
                .FromTable("other_service_request").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //customer_id
            Create.ForeignKey("FK_OTSRV_REQUEST_CUSTOMER")
                .FromTable("other_service_request").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");
            //pet_id
            Create.ForeignKey("FK_OTSRV_REQUEST_CUSTOMER_PETS")
                .FromTable("other_service_request").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");
            //address_location_system_id
            Create.ForeignKey("FK_OTSRV_REQUEST_SYS_LOC_ID")
                .FromTable("other_service_request").ForeignColumn("address_location_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
