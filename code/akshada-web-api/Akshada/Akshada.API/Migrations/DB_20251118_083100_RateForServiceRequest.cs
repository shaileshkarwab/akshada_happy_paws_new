using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251118083100)]
    public class DB_20251118_083100_RateForServiceRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("regular_day_rate")
                .OnTable("walking_service_request")
                .AsDecimal()
                .NotNullable().WithDefaultValue(0);


            Create.Column("special_day_rate")
                .OnTable("walking_service_request")
                .AsDecimal()
                .NotNullable().WithDefaultValue(0);
        }
    }
}
