using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251108071201)]
    public class DB_20251108_071200_ImportantContact : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("important_contact")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("contact_type_system_id").AsInt32().NotNullable()
                .WithColumn("contact_name").AsString().NotNullable().Unique("UK_IMP_CONTACT_NAME")
                .WithColumn("email").AsString().Nullable()
                .WithColumn("mobile").AsString().NotNullable()
                .WithColumn("is_Active").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_IMP_CON_CRT_BY")
                .FromTable("important_contact").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_IMP_CON_UPDT_BY")
                .FromTable("important_contact").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //contact_type_system_id
            Create.ForeignKey("FK_IMP_CON_TYPE_SYSTEM_ID")
                .FromTable("important_contact").ForeignColumn("contact_type_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            Create.Table("important_contact_address_detail")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("important_contact_master_id").AsInt32().NotNullable()
                .WithColumn("contact_address_type_system_id").AsInt32().NotNullable()
                .WithColumn("address_name").AsString().Nullable()
                .WithColumn("address_1").AsString().NotNullable()
                .WithColumn("address_2").AsString().NotNullable()
                .WithColumn("city_town").AsString().Nullable()
                .WithColumn("pin_code").AsString().Nullable()
                .WithColumn("email").AsString().Nullable()
                .WithColumn("mobile").AsString().Nullable()
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_IMP_CON_ADD_CRT_BY")
                .FromTable("important_contact_address_detail").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_IMP_CON_ADD_UPDT_BY")
                .FromTable("important_contact_address_detail").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //important_contact_master_id
            Create.ForeignKey("FK_IMP_CON_ADD_IMP_CON_ID")
                .FromTable("important_contact_address_detail").ForeignColumn("important_contact_master_id")
                .ToTable("important_contact").PrimaryColumn("id");

            //contact_address_type_system_id
            Create.ForeignKey("FK_IMP_CON_ADD_TYPE_SYSTEM_ID")
                .FromTable("important_contact_address_detail").ForeignColumn("contact_address_type_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
