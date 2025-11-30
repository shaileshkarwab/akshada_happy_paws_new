using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107135900)]
    public class DB_20251107_135900_CustomerAddressProofInSrvRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("customer_address_proof")
                .OnTable("other_service_request")
                .AsString()
                .Nullable();
        }
    }
}
