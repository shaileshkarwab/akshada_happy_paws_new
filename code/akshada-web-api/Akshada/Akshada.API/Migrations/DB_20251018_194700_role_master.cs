using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251018194700)]
    public class DB_20251018_194700_role_master : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("role_master")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("role_name").AsString().Unique("UK_ROLE_NAME")
                .WithColumn("status").AsBoolean().WithDefaultValue(true)
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_USR_MST_ROLE_CRT_BY")
                .FromTable("role_master").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");


            Create.ForeignKey("FK_USR_MST_ROLE_UPDT_BY")
                .FromTable("role_master").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
