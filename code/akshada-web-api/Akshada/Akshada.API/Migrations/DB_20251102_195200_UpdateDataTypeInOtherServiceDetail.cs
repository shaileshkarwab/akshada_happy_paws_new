using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251102195200)]
    public class DB_20251102_195200_UpdateDataTypeInOtherServiceDetail : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Column("image_name")
                .OnTable("other_services_offered_images")
                .AsString()
                .NotNullable();

            Alter.Column("upload_image_name")
                .OnTable("other_services_offered_images")
                .AsString()
                .NotNullable();

            // adding UK
            Create.UniqueConstraint("UK_OTH_SRV_OFF_IMG")
                .OnTable("other_services_offered_images")
                .Columns("other_services_offered_master_id", "image_type_system_id");
        }
    }
}
