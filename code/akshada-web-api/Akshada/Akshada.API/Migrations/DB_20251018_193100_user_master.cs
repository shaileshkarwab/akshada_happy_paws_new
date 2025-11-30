using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251018193100)]
    public class DB_20251018_193100_user_master : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_master")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("row_id").AsString().Unique().NotNullable()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("role_id").AsInt32().NotNullable()
                .WithColumn("login_name").AsString().NotNullable().Unique("UK_USER_LOGIN_NAME")
                .WithColumn("status").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("image_path").AsString().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_USER_CRT_BY")
                .FromTable("user_master").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_USER_UPDT_BY")
                .FromTable("user_master").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
