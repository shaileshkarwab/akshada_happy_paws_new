using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20252611162300)]
    public class DB_20251126_162300_CompanyBankAndUPI:AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("company_information_bank_account")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_COMP_BNK_INFO_ROW_ID")
                .WithColumn("company_information_master_id").AsInt32().NotNullable()
                .WithColumn("bank_name").AsString().NotNullable()
                .WithColumn("bank_account").AsString().NotNullable()
                .WithColumn("bank_ifsc_code").AsString().NotNullable()
                .WithColumn("bank_branch").AsString().NotNullable();

            Create.UniqueConstraint("UK_CIBA_BANK_NAME_AND_ACCOUNT")
                .OnTable("company_information_bank_account")
                .Columns("bank_name", "bank_account");

            Create.ForeignKey("FK_CIBA_COMP_INFORMATION")
                .FromTable("company_information_bank_account").ForeignColumn("company_information_master_id")
                .ToTable("company_information").PrimaryColumn("id");


            Create.Table("company_information_upi_account")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_COMP_UPI_INFO_ROW_ID")
                .WithColumn("company_information_master_id").AsInt32().NotNullable()
                .WithColumn("upi_id").AsString().NotNullable()
                .WithColumn("upi_name").AsString();

            Create.ForeignKey("FK_CIUA_COMP_INFORMATION")
                .FromTable("company_information_upi_account").ForeignColumn("company_information_master_id")
                .ToTable("company_information").PrimaryColumn("id");

        }

        
    }
}
