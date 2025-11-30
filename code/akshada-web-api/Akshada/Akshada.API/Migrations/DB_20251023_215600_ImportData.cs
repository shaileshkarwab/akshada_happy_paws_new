using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251023215600)]
    public class DB_20251023_215600_ImportData : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("import_data")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("row_id").AsString().Unique()
                .WithColumn("identifier").AsString().NotNullable()
                .WithColumn("json_data").AsString(int.MaxValue).NotNullable()
                .WithColumn("is_processed").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("operation_key").AsString().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_IMP_DATA_CRT_BY")
                .FromTable("import_data").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_IMP_DATA_UPDT_BY")
                .FromTable("import_data").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
