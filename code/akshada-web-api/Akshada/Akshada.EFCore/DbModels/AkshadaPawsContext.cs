using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Akshada.EFCore.DbModels;

public partial class AkshadaPawsContext : DbContext
{
    public AkshadaPawsContext()
    {
    }

    public AkshadaPawsContext(DbContextOptions<AkshadaPawsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssignOtherServiceRequestUser> AssignOtherServiceRequestUsers { get; set; }

    public virtual DbSet<CompanyInformation> CompanyInformations { get; set; }

    public virtual DbSet<CompanyInformationBankAccount> CompanyInformationBankAccounts { get; set; }

    public virtual DbSet<CompanyInformationUpiAccount> CompanyInformationUpiAccounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerPet> CustomerPets { get; set; }

    public virtual DbSet<GoogleFormSubmission> GoogleFormSubmissions { get; set; }

    public virtual DbSet<HolidaySchedule> HolidaySchedules { get; set; }

    public virtual DbSet<ImportDatum> ImportData { get; set; }

    public virtual DbSet<ImportantContact> ImportantContacts { get; set; }

    public virtual DbSet<ImportantContactAddressDetail> ImportantContactAddressDetails { get; set; }

    public virtual DbSet<MenuMaster> MenuMasters { get; set; }

    public virtual DbSet<NewUserAssignToWalkingService> NewUserAssignToWalkingServices { get; set; }

    public virtual DbSet<NotificationSchedule> NotificationSchedules { get; set; }

    public virtual DbSet<OtherServiceRate> OtherServiceRates { get; set; }

    public virtual DbSet<OtherServiceRateDetail> OtherServiceRateDetails { get; set; }

    public virtual DbSet<OtherServiceRequest> OtherServiceRequests { get; set; }

    public virtual DbSet<OtherServicesOffered> OtherServicesOffereds { get; set; }

    public virtual DbSet<OtherServicesOfferedImage> OtherServicesOfferedImages { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<ServiceRateMaster> ServiceRateMasters { get; set; }

    public virtual DbSet<ServiceRateMasterDetail> ServiceRateMasterDetails { get; set; }

    public virtual DbSet<SystemParameter> SystemParameters { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    public virtual DbSet<VaccinationRecord> VaccinationRecords { get; set; }

    public virtual DbSet<VaccinationRecordDetail> VaccinationRecordDetails { get; set; }

    public virtual DbSet<Versioninfo> Versioninfos { get; set; }

    public virtual DbSet<WalkingServiceRecord> WalkingServiceRecords { get; set; }

    public virtual DbSet<WalkingServiceRecordImage> WalkingServiceRecordImages { get; set; }

    public virtual DbSet<WalkingServiceRequest> WalkingServiceRequests { get; set; }

    public virtual DbSet<WalkingServiceRequestDay> WalkingServiceRequestDays { get; set; }

    public virtual DbSet<WalkingServiceRequestDaySchedule> WalkingServiceRequestDaySchedules { get; set; }

    public virtual DbSet<WalkingServiceRequestDayScheduleAssignedToUser> WalkingServiceRequestDayScheduleAssignedToUsers { get; set; }

    public virtual DbSet<WebsiteService> WebsiteServices { get; set; }

    public virtual DbSet<WebsiteServiceProcess> WebsiteServiceProcesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AssignOtherServiceRequestUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("assign_other_service_request_user");

            entity.HasIndex(e => e.CreatedBy, "FK_OTSRV_REQUEST_ASSIGN_SR_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_OTSRV_REQUEST_ASSIGN_UPDT_BY");

            entity.HasIndex(e => e.AssignedToUserId, "FK_OTSRV_REQUEST_ASSIGN_USER_MASTER_ID");

            entity.HasIndex(e => e.RowId, "IX_assign_other_service_request_user_row_id").IsUnique();

            entity.HasIndex(e => e.OtherServiceRequestMasterId, "UK_SERV_REQ_MASTER_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignDate)
                .HasColumnType("datetime")
                .HasColumnName("assign_date");
            entity.Property(e => e.AssignedToUserId).HasColumnName("assigned_to_user_id");
            entity.Property(e => e.ChangedRequestDate)
                .HasColumnType("datetime")
                .HasColumnName("changed_request_date");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FromTime)
                .HasColumnType("datetime")
                .HasColumnName("from_time");
            entity.Property(e => e.IsAmountToBeCollectedAfterService)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_amount_to_be_collected_after_service");
            entity.Property(e => e.OtherServiceRequestMasterId).HasColumnName("other_service_request_master_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceCharge).HasColumnName("service_charge");
            entity.Property(e => e.ToTime)
                .HasColumnType("datetime")
                .HasColumnName("to_time");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.AssignOtherServiceRequestUserAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_REQUEST_ASSIGN_USER_MASTER_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AssignOtherServiceRequestUserCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_REQUEST_ASSIGN_SR_CRT_BY");

            entity.HasOne(d => d.OtherServiceRequestMaster).WithOne(p => p.AssignOtherServiceRequestUser)
                .HasForeignKey<AssignOtherServiceRequestUser>(d => d.OtherServiceRequestMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_REQUEST_MASTER_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.AssignOtherServiceRequestUserUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_REQUEST_ASSIGN_UPDT_BY");
        });

        modelBuilder.Entity<CompanyInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company_information");

            entity.HasIndex(e => e.CreatedBy, "FK_COMPANY_INFO_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_COMPANY_INFO_UPDT_BY");

            entity.HasIndex(e => e.CompanyName, "UK_COMPANY_NAME").IsUnique();

            entity.HasIndex(e => e.RowId, "UK_COMPANY_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("address_1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("address_2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CityTown)
                .HasMaxLength(255)
                .HasColumnName("city_town")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyName)
                .HasColumnName("company_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ContactNo1)
                .HasMaxLength(255)
                .HasColumnName("contact_no_1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ContactNo2)
                .HasMaxLength(255)
                .HasColumnName("contact_no_2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PinCode)
                .HasMaxLength(7)
                .HasColumnName("pin_code")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasColumnName("website")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CompanyInformationCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPANY_INFO_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.CompanyInformationUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPANY_INFO_UPDT_BY");
        });

        modelBuilder.Entity<CompanyInformationBankAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company_information_bank_account");

            entity.HasIndex(e => e.CompanyInformationMasterId, "FK_CIBA_COMP_INFORMATION");

            entity.HasIndex(e => new { e.BankName, e.BankAccount }, "UK_CIBA_BANK_NAME_AND_ACCOUNT").IsUnique();

            entity.HasIndex(e => e.RowId, "UK_COMP_BNK_INFO_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountHolderName)
                .HasMaxLength(255)
                .HasColumnName("account_holder_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BankAccount)
                .HasColumnName("bank_account")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BankBranch)
                .HasMaxLength(255)
                .HasColumnName("bank_branch")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BankIfscCode)
                .HasMaxLength(255)
                .HasColumnName("bank_ifsc_code")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.BankName)
                .HasColumnName("bank_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CompanyInformationMasterId).HasColumnName("company_information_master_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.CompanyInformationMaster).WithMany(p => p.CompanyInformationBankAccounts)
                .HasForeignKey(d => d.CompanyInformationMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CIBA_COMP_INFORMATION");
        });

        modelBuilder.Entity<CompanyInformationUpiAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company_information_upi_account");

            entity.HasIndex(e => e.CompanyInformationMasterId, "FK_CIUA_COMP_INFORMATION");

            entity.HasIndex(e => e.RowId, "UK_COMP_UPI_INFO_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyInformationMasterId).HasColumnName("company_information_master_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpiId)
                .HasMaxLength(255)
                .HasColumnName("upi_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpiName)
                .HasMaxLength(255)
                .HasColumnName("upi_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.CompanyInformationMaster).WithMany(p => p.CompanyInformationUpiAccounts)
                .HasForeignKey(d => d.CompanyInformationMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CIUA_COMP_INFORMATION");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.CreatedBy, "FK_CUSTOMER_CRT_BY");

            entity.HasIndex(e => e.AreaLocationSystemId, "FK_CUSTOMER_SYS_PARAM_LOCATION_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_CUSTOMER_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_customer_row_id").IsUnique();

            entity.HasIndex(e => e.Email, "UK_CUSTOMER_EMAIL").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AddressProofImage)
                .HasMaxLength(255)
                .HasColumnName("address_proof_image")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AreaLocationSystemId).HasColumnName("area_location_system_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Email)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Mobile)
                .HasMaxLength(255)
                .HasColumnName("mobile")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("user_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.AreaLocationSystem).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AreaLocationSystemId)
                .HasConstraintName("FK_CUSTOMER_SYS_PARAM_LOCATION_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.CustomerUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_UPDT_BY");
        });

        modelBuilder.Entity<CustomerPet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer_pets");

            entity.HasIndex(e => e.CustomerId, "FK_CUSTOMER_CUST_PET_ID");

            entity.HasIndex(e => e.ImportDataId, "FK_CUSTOMER_PET_IMPORT_DATA_ID");

            entity.HasIndex(e => e.BreedSystemId, "FK_CUSTOMER_PET_SYS_BREED_ID");

            entity.HasIndex(e => e.ColourId, "FK_CUSTOMER_PET_SYS_COLOUR_ID");

            entity.HasIndex(e => e.NatureOfPetSystemId, "FK_CUST_PET_NATURE_SYS_PARAM_ID");

            entity.HasIndex(e => e.RowId, "IX_customer_pets_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BreedSystemId).HasColumnName("breed_system_id");
            entity.Property(e => e.ColourId).HasColumnName("colour_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ImportDataId).HasColumnName("import_data_id");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDataComplete).HasColumnName("is_data_complete");
            entity.Property(e => e.NatureOfPetSystemId).HasColumnName("nature_of_pet_system_id");
            entity.Property(e => e.PetAgeMonth).HasColumnName("pet_age_month");
            entity.Property(e => e.PetAgeYear).HasColumnName("pet_age_year");
            entity.Property(e => e.PetAndOwnerImage)
                .HasMaxLength(255)
                .HasColumnName("pet_and_owner_image")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PetDateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("pet_date_of_birth");
            entity.Property(e => e.PetName)
                .HasMaxLength(255)
                .HasColumnName("pet_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PetPastIllness)
                .HasMaxLength(255)
                .HasColumnName("pet_past_illness")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PetVaccinationImage)
                .HasMaxLength(255)
                .HasColumnName("pet_vaccination_image")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PetWeight).HasColumnName("pet_weight");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.BreedSystem).WithMany(p => p.CustomerPetBreedSystems)
                .HasForeignKey(d => d.BreedSystemId)
                .HasConstraintName("FK_CUSTOMER_PET_SYS_BREED_ID");

            entity.HasOne(d => d.Colour).WithMany(p => p.CustomerPetColours)
                .HasForeignKey(d => d.ColourId)
                .HasConstraintName("FK_CUSTOMER_PET_SYS_COLOUR_ID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerPets)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER_CUST_PET_ID");

            entity.HasOne(d => d.ImportData).WithMany(p => p.CustomerPets)
                .HasForeignKey(d => d.ImportDataId)
                .HasConstraintName("FK_CUSTOMER_PET_IMPORT_DATA_ID");

            entity.HasOne(d => d.NatureOfPetSystem).WithMany(p => p.CustomerPetNatureOfPetSystems)
                .HasForeignKey(d => d.NatureOfPetSystemId)
                .HasConstraintName("FK_CUST_PET_NATURE_SYS_PARAM_ID");
        });

        modelBuilder.Entity<GoogleFormSubmission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("google_form_submission");

            entity.HasIndex(e => e.CustomerId, "FK_GFS_CUSTOMER_ID");

            entity.HasIndex(e => e.RowId, "UK_GFS_ROW_ID").IsUnique();

            entity.HasIndex(e => e.PetId, "UK_REFRENCE_CUSTOMER_PET_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.JsonData)
                .HasColumnName("json_data")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Customer).WithMany(p => p.GoogleFormSubmissions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_GFS_CUSTOMER_ID");

            entity.HasOne(d => d.Pet).WithOne(p => p.GoogleFormSubmission)
                .HasForeignKey<GoogleFormSubmission>(d => d.PetId)
                .HasConstraintName("FK_GFS_CUSTOMER_PET_ID");
        });

        modelBuilder.Entity<HolidaySchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("holiday_schedule");

            entity.HasIndex(e => e.CreatedBy, "FK_HS_CRT_BY");

            entity.HasIndex(e => e.HolidayTypeSystemId, "FK_HS_SYSTEM_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_HS_UPDT_BY");

            entity.HasIndex(e => e.HolidayDate, "IX_holiday_schedule_holiday_date").IsUnique();

            entity.HasIndex(e => e.RowId, "IX_holiday_schedule_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.HolidayDate).HasColumnName("holiday_date");
            entity.Property(e => e.HolidayName)
                .HasMaxLength(255)
                .HasColumnName("holiday_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.HolidayTypeSystemId).HasColumnName("holiday_type_system_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HolidayScheduleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HS_CRT_BY");

            entity.HasOne(d => d.HolidayTypeSystem).WithMany(p => p.HolidaySchedules)
                .HasForeignKey(d => d.HolidayTypeSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HS_SYSTEM_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.HolidayScheduleUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HS_UPDT_BY");
        });

        modelBuilder.Entity<ImportDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("import_data");

            entity.HasIndex(e => e.CreatedBy, "FK_IMP_DATA_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_IMP_DATA_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_import_data_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Identifier)
                .HasMaxLength(255)
                .HasColumnName("identifier")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsProcessed).HasColumnName("is_processed");
            entity.Property(e => e.JsonData)
                .HasColumnName("json_data")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OperationKey)
                .HasMaxLength(255)
                .HasColumnName("operation_key")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ImportDatumCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_DATA_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ImportDatumUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_DATA_UPDT_BY");
        });

        modelBuilder.Entity<ImportantContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("important_contact");

            entity.HasIndex(e => e.CreatedBy, "FK_IMP_CON_CRT_BY");

            entity.HasIndex(e => e.ContactTypeSystemId, "FK_IMP_CON_TYPE_SYSTEM_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_IMP_CON_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_important_contact_row_id").IsUnique();

            entity.HasIndex(e => e.ContactName, "UK_IMP_CONTACT_NAME").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactName)
                .HasColumnName("contact_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ContactTypeSystemId).HasColumnName("contact_type_system_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.IsActive).HasColumnName("is_Active");
            entity.Property(e => e.Mobile)
                .HasMaxLength(255)
                .HasColumnName("mobile")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ContactTypeSystem).WithMany(p => p.ImportantContacts)
                .HasForeignKey(d => d.ContactTypeSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_TYPE_SYSTEM_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ImportantContactCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ImportantContactUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_UPDT_BY");
        });

        modelBuilder.Entity<ImportantContactAddressDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("important_contact_address_detail");

            entity.HasIndex(e => e.CreatedBy, "FK_IMP_CON_ADD_CRT_BY");

            entity.HasIndex(e => e.ImportantContactMasterId, "FK_IMP_CON_ADD_IMP_CON_ID");

            entity.HasIndex(e => e.ContactAddressTypeSystemId, "FK_IMP_CON_ADD_TYPE_SYSTEM_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_IMP_CON_ADD_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_important_contact_address_detail_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("address_1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("address_2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AddressName)
                .HasMaxLength(255)
                .HasColumnName("address_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CityTown)
                .HasMaxLength(255)
                .HasColumnName("city_town")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ContactAddressTypeSystemId).HasColumnName("contact_address_type_system_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImportantContactMasterId).HasColumnName("important_contact_master_id");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.Mobile)
                .HasMaxLength(255)
                .HasColumnName("mobile")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PinCode)
                .HasMaxLength(255)
                .HasColumnName("pin_code")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.ContactAddressTypeSystem).WithMany(p => p.ImportantContactAddressDetails)
                .HasForeignKey(d => d.ContactAddressTypeSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_ADD_TYPE_SYSTEM_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ImportantContactAddressDetailCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_ADD_CRT_BY");

            entity.HasOne(d => d.ImportantContactMaster).WithMany(p => p.ImportantContactAddressDetails)
                .HasForeignKey(d => d.ImportantContactMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_ADD_IMP_CON_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ImportantContactAddressDetailUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMP_CON_ADD_UPDT_BY");
        });

        modelBuilder.Entity<MenuMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu_master");

            entity.HasIndex(e => e.RowId, "UK_MENU_MASTER_ROW_ID").IsUnique();

            entity.HasIndex(e => e.MenuText, "UK_MENU_MASTER_TEXT").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Controller)
                .HasMaxLength(255)
                .HasColumnName("controller")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FaIcon)
                .HasMaxLength(255)
                .HasColumnName("fa_icon")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MenuText)
                .HasColumnName("menu_text")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Page)
                .HasMaxLength(255)
                .HasColumnName("page")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SeqNo).HasColumnName("seq_no");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<NewUserAssignToWalkingService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("new_user_assign_to_walking_service");

            entity.HasIndex(e => e.CustomerId, "FK_NUATWS_CUSTOMER_ID");

            entity.HasIndex(e => e.PetId, "FK_NUATWS_PET_ID");

            entity.HasIndex(e => e.CreatedBy, "FK_NUATWS_USR_CRT_BY");

            entity.HasIndex(e => e.UserId, "FK_NUATWS_USR_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_NUATWS_USR_UPDT_BY");

            entity.HasIndex(e => e.WalkingRequestDayMasterId, "FK_NUATWS_WRMDAY_ID");

            entity.HasIndex(e => e.WalkingRequestScheduleMasterId, "FK_NUATWS_WRMDAY_SCH_ID");

            entity.HasIndex(e => e.WalkingRequestMasterId, "FK_NUATWS_WRM_ID");

            entity.HasIndex(e => e.RowId, "IX_new_user_assign_to_walking_service_row_id").IsUnique();

            entity.HasIndex(e => new { e.AssignDate, e.WalkingRequestScheduleMasterId }, "UK_USER_ID_DATE_WALKING_SCH").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignDate)
                .HasColumnType("datetime")
                .HasColumnName("assign_date");
            entity.Property(e => e.ChangeFromTime)
                .HasColumnType("datetime")
                .HasColumnName("change_from_time");
            entity.Property(e => e.ChangeToTime)
                .HasColumnType("datetime")
                .HasColumnName("change_to_time");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WalkingRequestDayMasterId).HasColumnName("walking_request_day_master_id");
            entity.Property(e => e.WalkingRequestMasterId).HasColumnName("walking_request_master_id");
            entity.Property(e => e.WalkingRequestScheduleMasterId).HasColumnName("walking_request_schedule_master_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NewUserAssignToWalkingServiceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_USR_CRT_BY");

            entity.HasOne(d => d.Customer).WithMany(p => p.NewUserAssignToWalkingServices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_CUSTOMER_ID");

            entity.HasOne(d => d.Pet).WithMany(p => p.NewUserAssignToWalkingServices)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_PET_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.NewUserAssignToWalkingServiceUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_USR_UPDT_BY");

            entity.HasOne(d => d.User).WithMany(p => p.NewUserAssignToWalkingServiceUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_USR_ID");

            entity.HasOne(d => d.WalkingRequestDayMaster).WithMany(p => p.NewUserAssignToWalkingServices)
                .HasForeignKey(d => d.WalkingRequestDayMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_WRMDAY_ID");

            entity.HasOne(d => d.WalkingRequestMaster).WithMany(p => p.NewUserAssignToWalkingServices)
                .HasForeignKey(d => d.WalkingRequestMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_WRM_ID");

            entity.HasOne(d => d.WalkingRequestScheduleMaster).WithMany(p => p.NewUserAssignToWalkingServices)
                .HasForeignKey(d => d.WalkingRequestScheduleMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NUATWS_WRMDAY_SCH_ID");
        });

        modelBuilder.Entity<NotificationSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification_schedule");

            entity.HasIndex(e => e.NotificationEnumId, "FK_NOTI_SYS_ID");

            entity.HasIndex(e => e.CreatedBy, "FK_NOTI_S_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_NOTI_S_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_notification_schedule_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeforeDays).HasColumnName("before_days");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.NotificationEnumId).HasColumnName("notification_enum_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ScheduleOnTime)
                .HasColumnType("datetime")
                .HasColumnName("schedule_on_time");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.NotificationScheduleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTI_S_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.NotificationScheduleUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTI_S_UPDT_BY");
        });

        modelBuilder.Entity<OtherServiceRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("other_service_rate");

            entity.HasIndex(e => e.CreatedBy, "FK_OTH_SRV_RM_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_OTH_SRV_RM_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_other_service_rate_row_id").IsUnique();

            entity.HasIndex(e => e.EffectiveDate, "UK_OSRM_EFF_DATE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EffectiveDate)
                .HasColumnType("datetime")
                .HasColumnName("effective_date");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OtherServiceRateCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTH_SRV_RM_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OtherServiceRateUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTH_SRV_RM_UPDT_BY");
        });

        modelBuilder.Entity<OtherServiceRateDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("other_service_rate_detail");

            entity.HasIndex(e => e.ServiceSystemId, "FK_OTH_SRV_RM_SRV_SYS_ID");

            entity.HasIndex(e => e.RowId, "IX_other_service_rate_detail_row_id").IsUnique();

            entity.HasIndex(e => new { e.OtherServiceRateMasterId, e.ServiceSystemId }, "UK_OTH_SRV_RT_DETAIL").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChargeableAmount).HasColumnName("chargeable_amount");
            entity.Property(e => e.OtherServiceRateMasterId).HasColumnName("other_service_rate_master_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceSystemId).HasColumnName("service_system_id");

            entity.HasOne(d => d.OtherServiceRateMaster).WithMany(p => p.OtherServiceRateDetails)
                .HasForeignKey(d => d.OtherServiceRateMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTH_SRV_RM_DETAIL_ID");

            entity.HasOne(d => d.ServiceSystem).WithMany(p => p.OtherServiceRateDetails)
                .HasForeignKey(d => d.ServiceSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTH_SRV_RM_SRV_SYS_ID");
        });

        modelBuilder.Entity<OtherServiceRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("other_service_request");

            entity.HasIndex(e => e.RequiredServiceSystemId, "FK_OSR_REQ_SRV_SYSTEM_ID");

            entity.HasIndex(e => e.PetColourBreedId, "FK_OTH_SRV_REQ_BREED_SYSTEM_ID");

            entity.HasIndex(e => e.PetColourSystemId, "FK_OTH_SRV_REQ_CLR_SYSTEM_ID");

            entity.HasIndex(e => e.WebsiteServiceProcessMasterId, "FK_OTH_SRV_REQ_WEB_SRV_PROC");

            entity.HasIndex(e => e.CustomerId, "FK_OTSRV_REQUEST_CUSTOMER");

            entity.HasIndex(e => e.PetId, "FK_OTSRV_REQUEST_CUSTOMER_PETS");

            entity.HasIndex(e => e.CreatedBy, "FK_OTSRV_REQUEST_SR_CRT_BY");

            entity.HasIndex(e => e.AddressLocationSystemId, "FK_OTSRV_REQUEST_SYS_LOC_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_OTSRV_REQUEST_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_other_service_request_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressLocationSystemId).HasColumnName("address_location_system_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .HasColumnName("customer_address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CustomerAddressProof)
                .HasMaxLength(255)
                .HasColumnName("customer_address_proof")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Mobile)
                .HasMaxLength(255)
                .HasColumnName("mobile")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OtherServiceRequestMasterId).HasColumnName("other_service_request_master_id");
            entity.Property(e => e.PetColourBreedId).HasColumnName("pet_colour_breed_id");
            entity.Property(e => e.PetColourSystemId).HasColumnName("pet_colour_system_id");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.PetImage)
                .HasMaxLength(255)
                .HasColumnName("pet_image")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.PetName)
                .HasMaxLength(255)
                .HasColumnName("pet_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RequiredServiceSystemId).HasColumnName("required_service_system_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceRequestDate)
                .HasColumnType("datetime")
                .HasColumnName("service_request_date");
            entity.Property(e => e.ServiceRequiredOnDate)
                .HasColumnType("datetime")
                .HasColumnName("service_required_on_date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WebsiteServiceProcessMasterId).HasColumnName("website_service_process_master_id");

            entity.HasOne(d => d.AddressLocationSystem).WithMany(p => p.OtherServiceRequestAddressLocationSystems)
                .HasForeignKey(d => d.AddressLocationSystemId)
                .HasConstraintName("FK_OTSRV_REQUEST_SYS_LOC_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OtherServiceRequestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_REQUEST_SR_CRT_BY");

            entity.HasOne(d => d.Customer).WithMany(p => p.OtherServiceRequests)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_OTSRV_REQUEST_CUSTOMER");

            entity.HasOne(d => d.PetColourBreed).WithMany(p => p.OtherServiceRequestPetColourBreeds)
                .HasForeignKey(d => d.PetColourBreedId)
                .HasConstraintName("FK_OTH_SRV_REQ_BREED_SYSTEM_ID");

            entity.HasOne(d => d.PetColourSystem).WithMany(p => p.OtherServiceRequestPetColourSystems)
                .HasForeignKey(d => d.PetColourSystemId)
                .HasConstraintName("FK_OTH_SRV_REQ_CLR_SYSTEM_ID");

            entity.HasOne(d => d.Pet).WithMany(p => p.OtherServiceRequests)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("FK_OTSRV_REQUEST_CUSTOMER_PETS");

            entity.HasOne(d => d.RequiredServiceSystem).WithMany(p => p.OtherServiceRequestRequiredServiceSystems)
                .HasForeignKey(d => d.RequiredServiceSystemId)
                .HasConstraintName("FK_OSR_REQ_SRV_SYSTEM_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OtherServiceRequestUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_REQUEST_UPDT_BY");

            entity.HasOne(d => d.WebsiteServiceProcessMaster).WithMany(p => p.OtherServiceRequests)
                .HasForeignKey(d => d.WebsiteServiceProcessMasterId)
                .HasConstraintName("FK_OTH_SRV_REQ_WEB_SRV_PROC");
        });

        modelBuilder.Entity<OtherServicesOffered>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("other_services_offered");

            entity.HasIndex(e => e.OtherServiceRequestMasterId, "FK_OSOFF_OSREQ_ID");

            entity.HasIndex(e => e.ServiceOfferedUserId, "FK_OTSRV_OFF_SRV_USER_ID");

            entity.HasIndex(e => e.CreatedBy, "FK_OTSRV_OFF_SR_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_OTSRV_OFF_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_other_services_offered_row_id").IsUnique();

            entity.HasIndex(e => e.ServiceOfferedDate, "UK_OTH_SRV").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FromTime)
                .HasColumnType("datetime")
                .HasColumnName("from_time");
            entity.Property(e => e.OtherServiceRequestMasterId).HasColumnName("other_service_request_master_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceOfferedDate)
                .HasColumnType("datetime")
                .HasColumnName("service_offered_date");
            entity.Property(e => e.ServiceOfferedUserId).HasColumnName("service_offered_user_id");
            entity.Property(e => e.ToTime)
                .HasColumnType("datetime")
                .HasColumnName("to_time");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OtherServicesOfferedCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_OFF_SR_CRT_BY");

            entity.HasOne(d => d.OtherServiceRequestMaster).WithMany(p => p.OtherServicesOffereds)
                .HasForeignKey(d => d.OtherServiceRequestMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OSOFF_OSREQ_ID");

            entity.HasOne(d => d.ServiceOfferedUser).WithMany(p => p.OtherServicesOfferedServiceOfferedUsers)
                .HasForeignKey(d => d.ServiceOfferedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_OFF_SRV_USER_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OtherServicesOfferedUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_OFF_UPDT_BY");
        });

        modelBuilder.Entity<OtherServicesOfferedImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("other_services_offered_images");

            entity.HasIndex(e => e.RowId, "IX_other_services_offered_images_row_id").IsUnique();

            entity.HasIndex(e => new { e.OtherServicesOfferedMasterId, e.ImageTypeSystemId }, "UK_OTH_SRV_OFF_IMG").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageName)
                .HasMaxLength(255)
                .HasColumnName("image_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageTypeSystemId).HasColumnName("image_type_system_id");
            entity.Property(e => e.OtherServicesOfferedMasterId).HasColumnName("other_services_offered_master_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UploadImageName)
                .HasMaxLength(255)
                .HasColumnName("upload_image_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.OtherServicesOfferedMaster).WithMany(p => p.OtherServicesOfferedImages)
                .HasForeignKey(d => d.OtherServicesOfferedMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTSRV_OFF_IMAGES_ID");
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role_master");

            entity.HasIndex(e => e.CreatedBy, "FK_USR_MST_ROLE_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_USR_MST_ROLE_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_role_master_row_id").IsUnique();

            entity.HasIndex(e => e.RoleName, "UK_ROLE_NAME").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.RoleName)
                .HasColumnName("role_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RoleMasterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USR_MST_ROLE_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RoleMasterUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USR_MST_ROLE_UPDT_BY");
        });

        modelBuilder.Entity<ServiceRateMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("service_rate_master");

            entity.HasIndex(e => e.CreatedBy, "FK_SRM_USR_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_SRM_USR_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_service_rate_master_row_id").IsUnique();

            entity.HasIndex(e => new { e.ServiceSystemId, e.EffectiveDate }, "UK_SRM_EFF_DATE_SERVICE_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EffectiveDate).HasColumnName("effective_date");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceSystemId).HasColumnName("service_system_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ServiceRateMasterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SRM_USR_CRT_BY");

            entity.HasOne(d => d.ServiceSystem).WithMany(p => p.ServiceRateMasters)
                .HasForeignKey(d => d.ServiceSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SRM_SYS_PARAM_SERVICE");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ServiceRateMasterUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SRM_USR_UPDT_BY");
        });

        modelBuilder.Entity<ServiceRateMasterDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("service_rate_master_detail");

            entity.HasIndex(e => e.LocationSystemId, "FK_SRMD_SYS_PARAM_LOCATION");

            entity.HasIndex(e => e.ServiceMasterId, "FK_SRM_SRM_DTL_ID");

            entity.HasIndex(e => e.RowId, "IX_service_rate_master_detail_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LocationSystemId).HasColumnName("location_system_id");
            entity.Property(e => e.RegularRate)
                .HasPrecision(19, 5)
                .HasColumnName("regular_rate");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceMasterId).HasColumnName("service_master_id");
            entity.Property(e => e.SpecialDayRate)
                .HasPrecision(19, 5)
                .HasColumnName("special_day_rate");

            entity.HasOne(d => d.LocationSystem).WithMany(p => p.ServiceRateMasterDetails)
                .HasForeignKey(d => d.LocationSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SRMD_SYS_PARAM_LOCATION");

            entity.HasOne(d => d.ServiceMaster).WithMany(p => p.ServiceRateMasterDetails)
                .HasForeignKey(d => d.ServiceMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SRM_SRM_DTL_ID");
        });

        modelBuilder.Entity<SystemParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("system_parameter");

            entity.HasIndex(e => e.CreatedBy, "FK_SYS_PARAM_USR_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_SYS_PARAM_USR_UPDT_BY");

            entity.HasIndex(e => e.RowId, "UK_SYS_PARAM_ROW_ID").IsUnique();

            entity.HasIndex(e => e.ParamValue, "UK_SYS_PARAM_VALUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EnumId).HasColumnName("enum_id");
            entity.Property(e => e.Identifier1)
                .HasMaxLength(255)
                .HasColumnName("identifier_1")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Identifier2)
                .HasMaxLength(255)
                .HasColumnName("identifier_2")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ParamAbbrivation)
                .HasMaxLength(4)
                .HasColumnName("param_abbrivation")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ParamValue)
                .HasColumnName("param_value")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SystemParameterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SYS_PARAM_USR_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.SystemParameterUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SYS_PARAM_USR_UPDT_BY");
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_master");

            entity.HasIndex(e => e.RoleId, "FK_ROLE_MASTER_USER_MASTER_ID");

            entity.HasIndex(e => e.CreatedBy, "FK_USER_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_USER_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_user_master_row_id").IsUnique();

            entity.HasIndex(e => e.LoginName, "UK_USER_LOGIN_NAME").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .HasColumnName("image_path")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.LoginName)
                .HasColumnName("login_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MobilePin)
                .HasMaxLength(255)
                .HasColumnName("mobile_pin")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasDefaultValueSql("'Admin@1234'")
                .HasColumnName("password")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_CRT_BY");

            entity.HasOne(d => d.Role).WithMany(p => p.UserMasters)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROLE_MASTER_USER_MASTER_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_UPDT_BY");
        });

        modelBuilder.Entity<VaccinationRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vaccination_record");

            entity.HasIndex(e => e.CreatedBy, "FK_VR_CRT_BY");

            entity.HasIndex(e => e.CustomerId, "FK_VR_CUST_ID");

            entity.HasIndex(e => e.VetOrClinicNameImpContactId, "FK_VR_IMP_CON_ID");

            entity.HasIndex(e => e.PetId, "FK_VR_PET_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_VR_UPDT_BY");

            entity.HasIndex(e => e.RowId, "IX_vaccination_record_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.RecordEntryDate).HasColumnName("record_entry_date");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.VaccinationDate).HasColumnName("vaccination_date");
            entity.Property(e => e.VaccinationProofImage)
                .HasMaxLength(255)
                .HasColumnName("vaccination_proof_image")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VetOrClinicContactNumber)
                .HasMaxLength(255)
                .HasColumnName("vet_or_clinic_contact_number")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VetOrClinicName)
                .HasMaxLength(255)
                .HasColumnName("vet_or_clinic_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VetOrClinicNameImpContactId).HasColumnName("vet_or_clinic_name_imp_contact_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VaccinationRecordCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VR_CRT_BY");

            entity.HasOne(d => d.Customer).WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VR_CUST_ID");

            entity.HasOne(d => d.Pet).WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VR_PET_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VaccinationRecordUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VR_UPDT_BY");

            entity.HasOne(d => d.VetOrClinicNameImpContact).WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.VetOrClinicNameImpContactId)
                .HasConstraintName("FK_VR_IMP_CON_ID");
        });

        modelBuilder.Entity<VaccinationRecordDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vaccination_record_detail");

            entity.HasIndex(e => e.VaccinationSystemId, "FK_VRD_VCC_SYS_ID");

            entity.HasIndex(e => e.VaccinationRecordMasterId, "FK_VRD_VR_ID");

            entity.HasIndex(e => e.RowId, "IX_vaccination_record_detail_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.VaccinatedDate).HasColumnName("vaccinated_date");
            entity.Property(e => e.VaccinationRecordMasterId).HasColumnName("vaccination_record_master_id");
            entity.Property(e => e.VaccinationSystemId).HasColumnName("vaccination_system_id");

            entity.HasOne(d => d.VaccinationRecordMaster).WithMany(p => p.VaccinationRecordDetails)
                .HasForeignKey(d => d.VaccinationRecordMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VRD_VR_ID");

            entity.HasOne(d => d.VaccinationSystem).WithMany(p => p.VaccinationRecordDetails)
                .HasForeignKey(d => d.VaccinationSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VRD_VCC_SYS_ID");
        });

        modelBuilder.Entity<Versioninfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("versioninfo");

            entity.HasIndex(e => e.Version, "UC_Version").IsUnique();

            entity.Property(e => e.AppliedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<WalkingServiceRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walking_service_record");

            entity.HasIndex(e => e.CreatedBy, "FK_WALK_SR_CRT_BY");

            entity.HasIndex(e => e.CustomerId, "FK_WALK_SR_CUSTOMER_ID");

            entity.HasIndex(e => e.PetId, "FK_WALK_SR_CUSTOMER_PET_ID");

            entity.HasIndex(e => e.WalkingServiceDayMasterId, "FK_WALK_SR_CUSTOMER_WSREQ_DAY_ID");

            entity.HasIndex(e => e.WalkingServiceDayScheduleMasterId, "FK_WALK_SR_CUSTOMER_WSREQ_DAY_SCH_ID");

            entity.HasIndex(e => e.WalkingServiceMasterId, "FK_WALK_SR_CUSTOMER_WSREQ_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_WALK_SR_UPDT_BY");

            entity.HasIndex(e => e.ServiceOfferedByUserId, "FK_WSRECORD_SRV_OFFER_USER_ID");

            entity.HasIndex(e => e.RowId, "IX_walking_service_record_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.FromTime)
                .HasColumnType("datetime")
                .HasColumnName("from_time");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceOfferedByUserId).HasColumnName("service_offered_by_user_id");
            entity.Property(e => e.ServiceOfferedDate)
                .HasColumnType("datetime")
                .HasColumnName("service_offered_date");
            entity.Property(e => e.ToTime)
                .HasColumnType("datetime")
                .HasColumnName("to_time");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WalkingServiceDayMasterId).HasColumnName("walking_service_day_master_id");
            entity.Property(e => e.WalkingServiceDayScheduleMasterId).HasColumnName("walking_service_day_schedule_master_id");
            entity.Property(e => e.WalkingServiceMasterId).HasColumnName("walking_service_master_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WalkingServiceRecordCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_CRT_BY");

            entity.HasOne(d => d.Customer).WithMany(p => p.WalkingServiceRecords)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_CUSTOMER_ID");

            entity.HasOne(d => d.Pet).WithMany(p => p.WalkingServiceRecords)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_CUSTOMER_PET_ID");

            entity.HasOne(d => d.ServiceOfferedByUser).WithMany(p => p.WalkingServiceRecordServiceOfferedByUsers)
                .HasForeignKey(d => d.ServiceOfferedByUserId)
                .HasConstraintName("FK_WSRECORD_SRV_OFFER_USER_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.WalkingServiceRecordUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_UPDT_BY");

            entity.HasOne(d => d.WalkingServiceDayMaster).WithMany(p => p.WalkingServiceRecords)
                .HasForeignKey(d => d.WalkingServiceDayMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_CUSTOMER_WSREQ_DAY_ID");

            entity.HasOne(d => d.WalkingServiceDayScheduleMaster).WithMany(p => p.WalkingServiceRecords)
                .HasForeignKey(d => d.WalkingServiceDayScheduleMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_CUSTOMER_WSREQ_DAY_SCH_ID");

            entity.HasOne(d => d.WalkingServiceMaster).WithMany(p => p.WalkingServiceRecords)
                .HasForeignKey(d => d.WalkingServiceMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_CUSTOMER_WSREQ_ID");
        });

        modelBuilder.Entity<WalkingServiceRecordImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walking_service_record_images");

            entity.HasIndex(e => e.WalkingServiceRecordMasterId, "FK_WALK_SR_IMAGES");

            entity.HasIndex(e => e.ImageUploadSystemId, "FK_WALK_SR_IMAGES_SYS_ID");

            entity.HasIndex(e => e.RowId, "IX_walking_service_record_images_row_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageName)
                .HasMaxLength(255)
                .HasColumnName("image_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ImageUploadSystemId).HasColumnName("image_upload_system_id");
            entity.Property(e => e.Lattitude).HasColumnName("lattitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.RecordTime)
                .HasDefaultValueSql("'2025-11-29 18:59:57'")
                .HasColumnType("datetime")
                .HasColumnName("record_time");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.WalkingServiceRecordMasterId).HasColumnName("walking_service_record_master_id");

            entity.HasOne(d => d.ImageUploadSystem).WithMany(p => p.WalkingServiceRecordImages)
                .HasForeignKey(d => d.ImageUploadSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_IMAGES_SYS_ID");

            entity.HasOne(d => d.WalkingServiceRecordMaster).WithMany(p => p.WalkingServiceRecordImages)
                .HasForeignKey(d => d.WalkingServiceRecordMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WALK_SR_IMAGES");
        });

        modelBuilder.Entity<WalkingServiceRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walking_service_request");

            entity.HasIndex(e => e.CreatedBy, "FK_WSR_CRT_BY");

            entity.HasIndex(e => e.CustomerId, "FK_WSR_CUSTOMER_ID");

            entity.HasIndex(e => e.PetId, "FK_WSR_CUSTOMER_PET_ID");

            entity.HasIndex(e => e.FrequencySystemId, "FK_WSR_SRV_FREQ_SYSTEM_ID");

            entity.HasIndex(e => e.ServiceSystemId, "FK_WSR_SRV_SYSTEM_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_WSR_UPDT_BY");

            entity.HasIndex(e => e.RowId, "UK_WSR_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.FrequencySystemId).HasColumnName("frequency_system_id");
            entity.Property(e => e.FromDate)
                .HasColumnType("datetime")
                .HasColumnName("from_date");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.RegularDayRate)
                .HasPrecision(19, 5)
                .HasColumnName("regular_day_rate");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceSystemId).HasColumnName("service_system_id");
            entity.Property(e => e.SpecialDayRate)
                .HasPrecision(19, 5)
                .HasColumnName("special_day_rate");
            entity.Property(e => e.ToDate)
                .HasColumnType("datetime")
                .HasColumnName("to_date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WalkingServiceRequestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_CRT_BY");

            entity.HasOne(d => d.Customer).WithMany(p => p.WalkingServiceRequests)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_CUSTOMER_ID");

            entity.HasOne(d => d.FrequencySystem).WithMany(p => p.WalkingServiceRequestFrequencySystems)
                .HasForeignKey(d => d.FrequencySystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_SRV_FREQ_SYSTEM_ID");

            entity.HasOne(d => d.Pet).WithMany(p => p.WalkingServiceRequests)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_CUSTOMER_PET_ID");

            entity.HasOne(d => d.ServiceSystem).WithMany(p => p.WalkingServiceRequestServiceSystems)
                .HasForeignKey(d => d.ServiceSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_SRV_SYSTEM_ID");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.WalkingServiceRequestUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_UPDT_BY");
        });

        modelBuilder.Entity<WalkingServiceRequestDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walking_service_request_days");

            entity.HasIndex(e => e.WalkingServiceRequestMasterId, "FK_WSR_WSRD_ID");

            entity.HasIndex(e => e.RowId, "UK_WSRD_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsSelected).HasColumnName("is_selected");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.WalkingServiceRequestMasterId).HasColumnName("walking_service_request_master_id");
            entity.Property(e => e.WeekDayName)
                .HasMaxLength(255)
                .HasColumnName("week_day_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.WalkingServiceRequestMaster).WithMany(p => p.WalkingServiceRequestDays)
                .HasForeignKey(d => d.WalkingServiceRequestMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSR_WSRD_ID");
        });

        modelBuilder.Entity<WalkingServiceRequestDaySchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walking_service_request_day_schedule");

            entity.HasIndex(e => e.WalkingServiceRequestDaysMasterId, "FK_WSRD_SCH_WSRD_ID");

            entity.HasIndex(e => e.RowId, "UK_WSRD_SHD_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromTime)
                .HasColumnType("datetime")
                .HasColumnName("from_time");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ToTime)
                .HasColumnType("datetime")
                .HasColumnName("to_time");
            entity.Property(e => e.WalkingServiceRequestDaysMasterId).HasColumnName("walking_service_request_days_master_id");

            entity.HasOne(d => d.WalkingServiceRequestDaysMaster).WithMany(p => p.WalkingServiceRequestDaySchedules)
                .HasForeignKey(d => d.WalkingServiceRequestDaysMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSRD_SCH_WSRD_ID");
        });

        modelBuilder.Entity<WalkingServiceRequestDayScheduleAssignedToUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("walking_service_request_day_schedule_assigned_to_user");

            entity.HasIndex(e => e.WalkingServiceRequestMasterId, "FK_WLK_SRV_MST_ASSIGN_USER");

            entity.HasIndex(e => e.CreatedBy, "FK_WSRDSATU_USR_CRT_BY");

            entity.HasIndex(e => e.UpdatedBy, "FK_WSRDSATU_USR_UPDT_BY");

            entity.HasIndex(e => e.UserId, "FK_WSRDSH_ASSIGN_USER_ID");

            entity.HasIndex(e => e.WalkingServiceRequestDayScheduleId, "FK_WSRDSH_ASSIGN_USER_SCH_ID");

            entity.HasIndex(e => e.RowId, "UK_WSRD_ASSIGN_USER_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WalkingServiceRequestDayScheduleId).HasColumnName("walking_service_request_day_schedule_id");
            entity.Property(e => e.WalkingServiceRequestMasterId).HasColumnName("walking_service_request_master_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WalkingServiceRequestDayScheduleAssignedToUserCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSRDSATU_USR_CRT_BY");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.WalkingServiceRequestDayScheduleAssignedToUserUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSRDSATU_USR_UPDT_BY");

            entity.HasOne(d => d.User).WithMany(p => p.WalkingServiceRequestDayScheduleAssignedToUserUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSRDSH_ASSIGN_USER_ID");

            entity.HasOne(d => d.WalkingServiceRequestDaySchedule).WithMany(p => p.WalkingServiceRequestDayScheduleAssignedToUsers)
                .HasForeignKey(d => d.WalkingServiceRequestDayScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSRDSH_ASSIGN_USER_SCH_ID");

            entity.HasOne(d => d.WalkingServiceRequestMaster).WithMany(p => p.WalkingServiceRequestDayScheduleAssignedToUsers)
                .HasForeignKey(d => d.WalkingServiceRequestMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WLK_SRV_MST_ASSIGN_USER");
        });

        modelBuilder.Entity<WebsiteService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("website_service");

            entity.HasIndex(e => e.RowId, "UK_WEB_SRV_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body)
                .HasColumnName("body")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.JsonData)
                .HasColumnName("json_data")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<WebsiteServiceProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("website_service_process");

            entity.HasIndex(e => e.AssignedToUserId, "FK_WEB_SRV_PRO_ASSIGNED_TO_ID");

            entity.HasIndex(e => e.CreatedBy, "FK_WEB_SRV_PRO_CRT_BY");

            entity.HasIndex(e => e.WebsiteServiceMasterId, "FK_WEB_SRV_PRO_ID");

            entity.HasIndex(e => e.UpdatedBy, "FK_WEB_SRV_PRO_UPD_BY");

            entity.HasIndex(e => e.RequestNotParamSystemId, "FK_WEB_SRV_SYS_PARAM_NOT_ACCEPTED");

            entity.HasIndex(e => e.ServiceSystemId, "FK_WSP_SRV_SYSTEM_PARAM");

            entity.HasIndex(e => e.RowId, "UK_WEB_SRV_ROW_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AssignedToUserId).HasColumnName("assigned_to_user_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.MobileContactNumber)
                .HasMaxLength(255)
                .HasColumnName("mobile_contact_number")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RequestAccepted).HasColumnName("request_accepted");
            entity.Property(e => e.RequestNotParamSystemId).HasColumnName("request_not_param_system_id");
            entity.Property(e => e.RowId)
                .HasColumnName("row_id")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ServiceDate)
                .HasColumnType("datetime")
                .HasColumnName("service_date");
            entity.Property(e => e.ServiceFromTime)
                .HasColumnType("datetime")
                .HasColumnName("service_from_time");
            entity.Property(e => e.ServiceSystemId).HasColumnName("service_system_id");
            entity.Property(e => e.ServiceToTime)
                .HasColumnType("datetime")
                .HasColumnName("service_to_time");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WebsiteServiceMasterId).HasColumnName("website_service_master_id");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.WebsiteServiceProcessAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .HasConstraintName("FK_WEB_SRV_PRO_ASSIGNED_TO_ID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WebsiteServiceProcessCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WEB_SRV_PRO_CRT_BY");

            entity.HasOne(d => d.RequestNotParamSystem).WithMany(p => p.WebsiteServiceProcessRequestNotParamSystems)
                .HasForeignKey(d => d.RequestNotParamSystemId)
                .HasConstraintName("FK_WEB_SRV_SYS_PARAM_NOT_ACCEPTED");

            entity.HasOne(d => d.ServiceSystem).WithMany(p => p.WebsiteServiceProcessServiceSystems)
                .HasForeignKey(d => d.ServiceSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WSP_SRV_SYSTEM_PARAM");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.WebsiteServiceProcessUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WEB_SRV_PRO_UPD_BY");

            entity.HasOne(d => d.WebsiteServiceMaster).WithMany(p => p.WebsiteServiceProcessWebsiteServiceMasters)
                .HasForeignKey(d => d.WebsiteServiceMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WEB_SRV_PRO_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
