using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251102093000)]
    public class DB_20251102_093000_OtherServiceMenu : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("other_services_offered")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("service_offered_date").AsDateTime().NotNullable()
                .WithColumn("customer_id").AsInt32().NotNullable()
                .WithColumn("pet_id").AsInt32().NotNullable()
                .WithColumn("other_service_system_id").AsInt32().NotNullable()
                .WithColumn("service_offered_user_id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.UniqueConstraint("UK_OTH_SRV")
                .OnTable("other_services_offered")
                .Columns("service_offered_date", "customer_id", "pet_id", "other_service_system_id");



            Create.ForeignKey("FK_OTSRV_OFF_SR_CRT_BY")
                .FromTable("other_services_offered").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_OTSRV_OFF_UPDT_BY")
                .FromTable("other_services_offered").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //customer_id
            Create.ForeignKey("FK_OTSRV_OFF_CUSTOMER_ID")
                .FromTable("other_services_offered").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");
            //pet_id
            Create.ForeignKey("FK_OTSRV_OFF_PET_ID")
                .FromTable("other_services_offered").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");
            //other_service_system_id
            Create.ForeignKey("FK_OTSRV_OFF_SRV_SYSTEM_ID")
                .FromTable("other_services_offered").ForeignColumn("other_service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
            //service_offered_user_id
            Create.ForeignKey("FK_OTSRV_OFF_SRV_USER_ID")
                .FromTable("other_services_offered").ForeignColumn("service_offered_user_id")
                .ToTable("user_master").PrimaryColumn("id");


            // images
            Create.Table("other_services_offered_images")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("other_services_offered_master_id").AsInt32().NotNullable()
                .WithColumn("image_type_system_id").AsInt32().NotNullable()
                .WithColumn("image_name").AsInt32().NotNullable()
                .WithColumn("upload_image_name").AsInt32().NotNullable();

            //other_services_offered_master_id
            Create.ForeignKey("FK_OTSRV_OFF_IMAGES_ID")
                .FromTable("other_services_offered_images").ForeignColumn("other_services_offered_master_id")
                .ToTable("other_services_offered").PrimaryColumn("id");

        }
    }
}
