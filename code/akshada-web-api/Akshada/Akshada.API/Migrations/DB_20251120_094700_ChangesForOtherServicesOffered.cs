using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251120094700)]
    public class DB_20251120_094700_ChangesForOtherServicesOffered:Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            //drop columns customer_id, pet_id, other_service_system_id

            if (Schema.Table("other_services_offered").Constraint("FK_OTSRV_OFF_CUSTOMER_ID").Exists())
                Delete.ForeignKey("FK_OTSRV_OFF_CUSTOMER_ID").OnTable("other_services_offered");

            if(Schema.Table("other_services_offered").Column("customer_id").Exists())
                Delete.Column("customer_id").FromTable("other_services_offered");

            if(Schema.Table("other_services_offered").Constraint("FK_OTSRV_OFF_PET_ID").Exists())
                Delete.ForeignKey("FK_OTSRV_OFF_PET_ID").OnTable("other_services_offered");

            if (Schema.Table("other_services_offered").Column("pet_id").Exists())
                Delete.Column("pet_id").FromTable("other_services_offered");

            if(Schema.Table("other_services_offered").Constraint("FK_OTSRV_OFF_SRV_SYSTEM_ID").Exists())
                Delete.ForeignKey("FK_OTSRV_OFF_SRV_SYSTEM_ID").OnTable("other_services_offered");

            if (Schema.Table("other_services_offered").Column("other_service_system_id").Exists())
                Delete.Column("other_service_system_id").FromTable("other_services_offered");


        }

        

        
    }
}
