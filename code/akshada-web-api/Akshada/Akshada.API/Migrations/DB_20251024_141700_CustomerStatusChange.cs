using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251024141700)]
    public class DB_20251024_141700_CustomerStatusChange : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Column("is_active")
                .OnTable("customer")
                .AsBoolean().NotNullable().WithDefaultValue(true);

            Create.Column("is_active")
                .OnTable("customer_pets")
                .AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}
