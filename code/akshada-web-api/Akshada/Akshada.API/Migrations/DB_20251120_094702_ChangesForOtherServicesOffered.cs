using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251120094702)]
    public class DB_20251120_094702_ChangesForOtherServicesOffered : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("remarks")
                .OnTable("other_services_offered")
                .AsString().Nullable();
        }
    }
}
