using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251118071200)]
    public class DB_20251118_071200_NatureOfPetInCustomerPets : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("nature_of_pet_system_id")
                .OnTable("customer_pets")
                .AsInt32()
                .Nullable();

            Create.ForeignKey("FK_CUST_PET_NATURE_SYS_PARAM_ID")
                .FromTable("customer_pets").ForeignColumn("nature_of_pet_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
