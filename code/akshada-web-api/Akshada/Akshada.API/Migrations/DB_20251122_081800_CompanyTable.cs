using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251122081800)]
    public class DB_20251122_081800_CompanyTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("company_information")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_COMPANY_ROW_ID")
                .WithColumn("company_name").AsString().NotNullable().Unique("UK_COMPANY_NAME")
                .WithColumn("address_1").AsString().NotNullable()
                .WithColumn("address_2").AsString().Nullable()
                .WithColumn("city_town").AsString().NotNullable()
                .WithColumn("pin_code").AsString(7).NotNullable()
                .WithColumn("contact_no_1").AsString().NotNullable()
                .WithColumn("contact_no_2").AsString().Nullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("website").AsString().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_COMPANY_INFO_CRT_BY")
                .FromTable("company_information").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_COMPANY_INFO_UPDT_BY")
                .FromTable("company_information").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
