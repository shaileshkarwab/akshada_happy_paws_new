using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251129093200)]
    public class DB_20251129_093200_ServiceChargeInAssignedOtherRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("assign_other_service_request_user")
                .AddColumn("service_charge")
                .AsDouble()
                .NotNullable()
                .WithDefaultValue(0);

            Alter.Table("assign_other_service_request_user")
                .AddColumn("is_amount_to_be_collected_after_service")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(true);
        }
    }
}
