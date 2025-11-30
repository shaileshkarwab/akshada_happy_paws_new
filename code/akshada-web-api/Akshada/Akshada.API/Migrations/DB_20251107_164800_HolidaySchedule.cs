using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107164800)]
    public class DB_20251107_164800_HolidaySchedule : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("holiday_schedule")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("holiday_name").AsString().NotNullable()
                .WithColumn("holiday_type_system_id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_HS_CRT_BY")
                .FromTable("holiday_schedule").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_HS_UPDT_BY")
                .FromTable("holiday_schedule").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_HS_SYSTEM_ID")
                .FromTable("holiday_schedule").ForeignColumn("holiday_type_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
