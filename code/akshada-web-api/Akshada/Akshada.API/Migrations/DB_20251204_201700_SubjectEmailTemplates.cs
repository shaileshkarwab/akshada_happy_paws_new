using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251204201700)]
    public class DB_20251204_201700_SubjectEmailTemplates : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("email_subject")
                .OnTable("email_template_master")
                .AsString()
                .NotNullable();
        }
    }
}
