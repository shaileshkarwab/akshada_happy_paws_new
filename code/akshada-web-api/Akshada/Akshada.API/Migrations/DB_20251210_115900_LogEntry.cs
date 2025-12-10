using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251210115900)]
    public class DB_20251210_115900_LogEntry : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("log_entry")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("log_level").AsString().NotNullable()
                .WithColumn("message").AsString(Int32.MaxValue).NotNullable()
                .WithColumn("logger").AsString(Int32.MaxValue).NotNullable()
                .WithColumn("logged_at").AsDateTime().NotNullable();
        }
    }
}
