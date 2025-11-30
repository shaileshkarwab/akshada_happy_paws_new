using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251106100800)]
    public class DB_20251106_100800_AssignOtherServiceRequest : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("assign_other_service_request_user")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("assign_date").AsDateTime().NotNullable()
                .WithColumn("other_service_request_master_id").AsInt32().Unique("UK_SERV_REQ_MASTER_ID")
                .WithColumn("changed_request_date").AsDateTime().NotNullable()
                .WithColumn("from_time").AsDateTime().NotNullable()
                .WithColumn("to_time").AsDateTime().NotNullable()
                .WithColumn("assigned_to_user_id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_OTSRV_REQUEST_ASSIGN_SR_CRT_BY")
                .FromTable("assign_other_service_request_user").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_OTSRV_REQUEST_ASSIGN_UPDT_BY")
                .FromTable("assign_other_service_request_user").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //other_service_request_master_id
            Create.ForeignKey("FK_OTSRV_REQUEST_MASTER_ID")
                .FromTable("assign_other_service_request_user").ForeignColumn("other_service_request_master_id")
                .ToTable("other_service_request").PrimaryColumn("id");

            //assigned_to_user_id
            Create.ForeignKey("FK_OTSRV_REQUEST_ASSIGN_USER_MASTER_ID")
                .FromTable("assign_other_service_request_user").ForeignColumn("assigned_to_user_id")
                .ToTable("user_master").PrimaryColumn("id");
        }
    }
}
