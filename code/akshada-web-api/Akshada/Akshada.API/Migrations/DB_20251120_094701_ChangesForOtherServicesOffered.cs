using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251120094701)]
    public class DB_20251120_094701_ChangesForOtherServicesOffered : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("from_time")
                 .OnTable("other_services_offered")
                 .AsTime()
                 .NotNullable();

            Create.Column("to_time")
                 .OnTable("other_services_offered")
                 .AsTime()
                 .NotNullable();
        }
    }
}
