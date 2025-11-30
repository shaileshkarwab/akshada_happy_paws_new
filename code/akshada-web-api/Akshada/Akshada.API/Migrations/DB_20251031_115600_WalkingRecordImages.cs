using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251031115600)]
    public class DB_20251031_115600_WalkingRecordImages : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("walking_service_record_images")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().Nullable().Unique()
                .WithColumn("walking_service_record_master_id").AsInt32().NotNullable()
                .WithColumn("image_upload_system_id").AsInt32().NotNullable()
                .WithColumn("image_name").AsString().NotNullable();

            Create.ForeignKey("FK_WALK_SR_IMAGES")
                .FromTable("walking_service_record_images").ForeignColumn("walking_service_record_master_id")
                .ToTable("walking_service_record").PrimaryColumn("id");

            Create.ForeignKey("FK_WALK_SR_IMAGES_SYS_ID")
                .FromTable("walking_service_record_images").ForeignColumn("image_upload_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
