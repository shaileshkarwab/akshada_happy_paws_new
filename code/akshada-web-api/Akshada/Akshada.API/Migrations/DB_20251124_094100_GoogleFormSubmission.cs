using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251124094100)]
    public class DB_20251124_094100_GoogleFormSubmission : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("google_form_submission")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_GFS_ROW_ID")
                .WithColumn("json_data").AsString(Int32.MaxValue).NotNullable()
                .WithColumn("customer_id").AsInt32().Nullable()
                .WithColumn("pet_id").AsInt32().Nullable().Unique("UK_REFRENCE_CUSTOMER_PET_ID");

            Create.ForeignKey("FK_GFS_CUSTOMER_ID")
                .FromTable("google_form_submission").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");

            Create.ForeignKey("FK_GFS_CUSTOMER_PET_ID")
                .FromTable("google_form_submission").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");
        }
    }
}
