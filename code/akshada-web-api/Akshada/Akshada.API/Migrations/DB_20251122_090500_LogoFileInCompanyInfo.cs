using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251122090500)]
    public class DB_20251122_090500_LogoFileInCompanyInfo : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("logo")
                .OnTable("company_information")
                .AsString()
                .NotNullable();
        }
    }
}
