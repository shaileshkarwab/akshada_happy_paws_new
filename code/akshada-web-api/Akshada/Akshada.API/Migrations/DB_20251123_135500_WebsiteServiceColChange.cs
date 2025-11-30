using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251123135501)]
    public class DB_20251123_135500_WebsiteServiceColChange : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Column("body")
                .OnTable("website_service")
                .AsString(Int32.MaxValue)
                .NotNullable();


            Alter.Column("json_data")
                .OnTable("website_service")
                .AsString(Int32.MaxValue)
                .NotNullable();
        }
    }
}
