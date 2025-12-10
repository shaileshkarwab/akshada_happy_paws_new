using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251209175700)]
    public class DB_20251209_175700_RefreshTokenInUser : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("refresh_token")
                .OnTable("user_master")
                .AsString()
                .NotNullable()
                .WithDefaultValue(string.Empty);

            Create.Column("refresh_token_expiry")
                .OnTable("user_master")
                .AsDate()
                .NotNullable()
                .WithDefaultValue(System.DateTime.Now.Date);
        }
    }
}
