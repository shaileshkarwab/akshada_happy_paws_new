using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251129185500)]
    public class DB_20251129_185500_GeoInfoWalkingServiceRecordImages : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("walking_service_record_images")
                .AddColumn("lattitude")
                .AsDouble()
                .NotNullable()
                .WithDefaultValue(0);

            Alter.Table("walking_service_record_images")
                .AddColumn("longitude")
                .AsDouble()
                .NotNullable()
                .WithDefaultValue(0);

            Alter.Table("walking_service_record_images")
                .AddColumn("record_time")
                .AsDateTime()
                .NotNullable()
                .WithDefaultValue(System.DateTime.Now);
        }
    }
}
