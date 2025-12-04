using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251204170600)]
    public class DB_20251204_170600_HtmlEmailTemplate : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("html_email_template")
                .OnTable("email_template_master")
                .AsString(Int32.MaxValue)
                .Nullable();
        }
    }
}
