using Akshada.EFCore.DbModels;
using FluentMigrator;
using FluentMigrator.Expressions;

namespace Akshada.API.Migrations
{
    [Migration(20251202074900)]
    public class DB_20251202_074900_IsMonthlyChage_ServiceRateMaster : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Column("is_charged_monthly")
                .OnTable("service_rate_master")
                .AsBoolean()
                .WithDefaultValue(false)
                .NotNullable();


            IfDatabase("mysql").Execute.WithConnection((conn, tran) => {

                using var cmd = conn.CreateCommand();
                cmd.Transaction = tran;
                cmd.CommandText = @"
        SELECT COUNT(1)
        FROM information_schema.statistics
        WHERE table_schema = SCHEMA()
          AND table_name = 'service_rate_master'
          AND index_name in( 'UK_SRM_EFF_DATE_SERVICE_ID');
    ";

                var count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    cmd.CommandText = "ALTER TABLE service_rate_master DROP FOREIGN KEY FK_SRM_SYS_PARAM_SERVICE";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "ALTER TABLE service_rate_master DROP INDEX UK_SRM_EFF_DATE_SERVICE_ID";
                    cmd.ExecuteNonQuery();
                }

            });

            Create.UniqueConstraint("UK_SRM_EFF_DATE_SERVICE_ID")
                .OnTable("service_rate_master")
                .Columns("service_system_id", "effective_date", "is_charged_monthly");

            Create.ForeignKey("FK_SRM_SYS_PARAM_SERVICE")
                .FromTable("service_rate_master").ForeignColumn("service_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
