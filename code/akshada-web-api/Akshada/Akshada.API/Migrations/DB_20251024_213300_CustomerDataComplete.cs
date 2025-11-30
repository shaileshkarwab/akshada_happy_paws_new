using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251024213300)]
    public class DB_20251024_213300_CustomerDataComplete : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("is_data_complete")
                .OnTable("customer_pets")
                .AsBoolean()
                .WithDefaultValue(false)
                .NotNullable();
        }
    }
}
