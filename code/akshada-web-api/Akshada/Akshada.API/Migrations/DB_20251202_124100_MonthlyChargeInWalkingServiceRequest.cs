using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251202124100)]
    public class DB_20251202_124100_MonthlyChargeInWalkingServiceRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("is_charged_monthly")
                .OnTable("walking_service_request")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false);
        }
    }
}
