using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251023103400)]
    public class DB_20251023_103400_RoleUserLink : AutoReversingMigration
    {
        public override void Up()
        {
            Create.ForeignKey("FK_ROLE_MASTER_USER_MASTER_ID")
                .FromTable("user_master").ForeignColumn("role_id")
                .ToTable("role_master").PrimaryColumn("id");
        }
    }
}
