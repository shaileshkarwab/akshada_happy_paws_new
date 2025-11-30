using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251031163000)]
    public class DB_20251031_163000_ServiceOfferedUserID_InWalkingRecord : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("service_offered_by_user_id")
                .OnTable("walking_service_record")
                .AsInt32()
                .Nullable();

            Create.ForeignKey("FK_WSRECORD_SRV_OFFER_USER_ID")
                .FromTable("walking_service_record").ForeignColumn("service_offered_by_user_id")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
