using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251127214200)]
    public class DB_20251127_214200_AccountHolderName : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("account_holder_name")
                .OnTable("company_information_bank_account")
                .AsString()
                .Nullable();
        }
    }
}
