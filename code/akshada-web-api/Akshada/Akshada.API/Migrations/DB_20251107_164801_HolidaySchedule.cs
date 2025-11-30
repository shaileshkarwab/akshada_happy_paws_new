using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251107164801)]
    public class DB_20251107_164801_HolidaySchedule : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("holiday_date")
                .OnTable("holiday_schedule")
                .AsDate()
                .NotNullable()
                .Unique();
        }
    }
}
