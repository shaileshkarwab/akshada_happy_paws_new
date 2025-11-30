using FluentMigrator;

namespace Akshada.API.Migrations
{
    [Migration(20251108195000)]
    public class DB_20251108_195000_VaccinationRecord : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("vaccination_record")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("record_entry_date").AsDate().NotNullable()
                .WithColumn("vaccination_date").AsDate().NotNullable()
                .WithColumn("customer_id").AsInt32().NotNullable()
                .WithColumn("pet_id").AsInt32().NotNullable()
                .WithColumn("vet_or_clinic_name").AsString().Nullable()
                .WithColumn("vet_or_clinic_contact_number").AsString().Nullable()
                .WithColumn("remarks").AsString().Nullable()
                .WithColumn("vaccination_proof_image").AsString().NotNullable()
                .WithColumn("vet_or_clinic_name_imp_contact_id").AsInt32().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Create.ForeignKey("FK_VR_CRT_BY")
                .FromTable("vaccination_record").ForeignColumn("created_by")
                .ToTable("user_master").PrimaryColumn("id");

            Create.ForeignKey("FK_VR_UPDT_BY")
                .FromTable("vaccination_record").ForeignColumn("updated_by")
                .ToTable("user_master").PrimaryColumn("id");

            //customer_id
            Create.ForeignKey("FK_VR_CUST_ID")
                .FromTable("vaccination_record").ForeignColumn("customer_id")
                .ToTable("customer").PrimaryColumn("id");
            //pet_id
            Create.ForeignKey("FK_VR_PET_ID")
                .FromTable("vaccination_record").ForeignColumn("pet_id")
                .ToTable("customer_pets").PrimaryColumn("id");
            //vet_or_clinic_name_imp_contact_id
            Create.ForeignKey("FK_VR_IMP_CON_ID")
                .FromTable("vaccination_record").ForeignColumn("vet_or_clinic_name_imp_contact_id")
                .ToTable("important_contact").PrimaryColumn("id");

            Create.Table("vaccination_record_detail")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique()
                .WithColumn("vaccination_record_master_id").AsInt32().NotNullable()
                .WithColumn("vaccination_system_id").AsInt32().NotNullable()
                .WithColumn("vaccinated_date").AsDate().NotNullable()
                .WithColumn("due_date").AsDate().NotNullable();

            //vaccination_record_master_id
            Create.ForeignKey("FK_VRD_VR_ID")
                .FromTable("vaccination_record_detail").ForeignColumn("vaccination_record_master_id")
                .ToTable("vaccination_record").PrimaryColumn("id");
            //vaccination_system_id
            Create.ForeignKey("FK_VRD_VCC_SYS_ID")
                .FromTable("vaccination_record_detail").ForeignColumn("vaccination_system_id")
                .ToTable("system_parameter").PrimaryColumn("id");
        }
    }
}
