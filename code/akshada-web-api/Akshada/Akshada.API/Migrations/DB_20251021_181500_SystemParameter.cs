using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251021181500)]
    public class DB_20251021_181500_SystemParameter : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("system_parameter")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_SYS_PARAM_ROW_ID")
                .WithColumn("enum_id").AsInt32().NotNullable()
                .WithColumn("param_value").AsString().NotNullable().Unique("UK_SYS_PARAM_VALUE")
                .WithColumn("param_abbrivation").AsString(4).NotNullable()
                .WithColumn("identifier_1").AsString().Nullable()
                .WithColumn("identifier_2").AsString().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_SYS_PARAM_USR_CRT_BY")
                .FromTable("system_parameter").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_SYS_PARAM_USR_UPDT_BY")
                .FromTable("system_parameter").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
