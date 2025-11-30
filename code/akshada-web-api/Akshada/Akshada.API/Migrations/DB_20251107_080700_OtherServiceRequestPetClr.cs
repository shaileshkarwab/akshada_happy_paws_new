using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107080700)]
    public class DB_20251107_080700_OtherServiceRequestPetClr : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("pet_colour_system_id")
                .OnTable("other_service_request")
                .AsInt32()
                .Nullable();

            Create.Column("pet_colour_breed_id")
                .OnTable("other_service_request")
                .AsInt32()
                .Nullable();


            Create.ForeignKey("FK_OTH_SRV_REQ_CLR_SYSTEM_ID")
                .FromTable("other_service_request").ForeignColumn("pet_colour_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");

            Create.ForeignKey("FK_OTH_SRV_REQ_BREED_SYSTEM_ID")
                .FromTable("other_service_request").ForeignColumn("pet_colour_breed_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
