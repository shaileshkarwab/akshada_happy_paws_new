using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251021183300)]
    public class DB_20251021_183300_StatusInSystemParameter : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("status")
                .OnTable("system_parameter")
                .AsBoolean()
                .WithDefaultValue(true)
                .NotNullable();
        }
    }
}
