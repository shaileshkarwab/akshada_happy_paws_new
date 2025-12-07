using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251205191700)]
    public class DB_20251205_191700_GeoAddresFromLatLong : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("address")
                .OnTable("walking_service_record_images")
                .AsString(Int32.MaxValue)
                .Nullable();
        }
    }
}
