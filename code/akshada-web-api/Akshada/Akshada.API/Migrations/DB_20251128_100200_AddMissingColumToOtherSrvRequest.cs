using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251128100200)]
    public class DB_20251128_100200_AddMissingColumToOtherSrvRequest : AutoReversingMigration
    {
        public override void Up()
        {
            if(!Schema.Table("other_service_request").Column("other_service_request_master_id").Exists())
            {
                Create.Column("other_service_request_master_id")
                    .OnTable("other_service_request")
                    .AsInt32()
                    .Nullable();
            }
        }
    }
}
