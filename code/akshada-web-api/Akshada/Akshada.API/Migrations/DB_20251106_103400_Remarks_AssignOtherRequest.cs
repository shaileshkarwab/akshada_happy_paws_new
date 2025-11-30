using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251106103400)]
    public class DB_20251106_103400_Remarks_AssignOtherRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("remarks")
                .OnTable("assign_other_service_request_user")
                .AsString()
                .Nullable();
        }
    }
}
