using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107120000)]
    public class DB_20251107_120000_AddressProofInCustomer : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("address_proof_image")
                .OnTable("customer")
                .AsString()
                .Nullable();
        }
    }
}
