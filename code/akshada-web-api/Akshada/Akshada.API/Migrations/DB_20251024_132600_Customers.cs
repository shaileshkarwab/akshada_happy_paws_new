using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251024132600)]
    public class DB_20251024_132600_Customers : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("customer")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("customer_name").AsString().NotNullable()
                .WithColumn("email").AsString().NotNullable().Unique("UK_CUSTOMER_EMAIL")
                .WithColumn("mobile").AsString().NotNullable()
                .WithColumn("address").AsString().NotNullable()
                .WithColumn("user_name").AsString().NotNullable()
                .WithColumn("is_active").AsString().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_CUSTOMER_CRT_BY")
                .FromTable("customer").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_CUSTOMER_UPDT_BY")
                .FromTable("customer").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.Table("customer_pets")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("pet_name").AsString().Nullable()
                .WithColumn("breed_system_id").AsInt32().Nullable()
                .WithColumn("colour_id").AsInt32().Nullable()
                .WithColumn("pet_age_year").AsInt32().Nullable()
                .WithColumn("pet_age_month").AsInt32().Nullable()
                .WithColumn("pet_weight").AsDouble().Nullable()
                .WithColumn("pet_and_owner_image").AsString().Nullable()
                .WithColumn("pet_vaccination_image").AsString().Nullable()
                .WithColumn("pet_past_illness").AsString().Nullable()
                .WithColumn("pet_date_of_birth").AsDateTime().Nullable()
                .WithColumn("import_data_id").AsInt32().Nullable()
                .WithColumn("customer_id").AsInt32().NotNullable();

            Create.ForeignKey("FK_CUSTOMER_CUST_PET_ID")
                .FromTable("customer_pets").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");


            Create.ForeignKey("FK_CUSTOMER_PET_SYS_BREED_ID")
                .FromTable("customer_pets").ForeignColumn("breed_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            Create.ForeignKey("FK_CUSTOMER_PET_SYS_COLOUR_ID")
                .FromTable("customer_pets").ForeignColumn("colour_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            Create.ForeignKey("FK_CUSTOMER_PET_IMPORT_DATA_ID")
               .FromTable("customer_pets").ForeignColumn("import_data_id")
               .ToTable("import_data").PrimaryColumn("id");


        }
    }
}
