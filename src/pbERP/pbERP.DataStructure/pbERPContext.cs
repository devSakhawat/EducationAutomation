using Microsoft.EntityFrameworkCore;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.BSecurity;
using pbERP.Domain.Models.CCompany;
using pbERP.Domain.Models.DHR;
using pbERP.Domain.Models.EAccounts;
using pbERP.Domain.Models.FEducation;

namespace pbERP.DataStructure;

public partial class pbERPContext : DbContext
{
   public pbERPContext()
   {
   }

   public pbERPContext(DbContextOptions<pbERPContext> options) : base(options)
   {
   }

   public virtual DbSet<AGenConfigACountry> AGenConfigACountries { get; set; }

   public virtual DbSet<AGenConfigBDivisionOrState> AGenConfigBDivisionOrStates { get; set; }

   public virtual DbSet<AGenConfigCDistrictOrCity> AGenConfigCDistrictOrCities { get; set; }

   public virtual DbSet<AGenConfigDPoliceStation> AGenConfigDPoliceStations { get; set; }

   public virtual DbSet<AGenConfigEBusinessType> AGenConfigEBusinessTypes { get; set; }

   public virtual DbSet<AGenConfigEGender> AGenConfigEGenders { get; set; }

   public virtual DbSet<AGenConfigFBloodGroup> AGenConfigFBloodGroups { get; set; }

   public virtual DbSet<AGenConfigFLanguage> AGenConfigFLanguages { get; set; }

   public virtual DbSet<AGenConfigGFont> AGenConfigGFonts { get; set; }

   public virtual DbSet<AGenConfigGReligion> AGenConfigGReligions { get; set; }

   public virtual DbSet<AGenConfigHInvoiceMode> AGenConfigHInvoiceModes { get; set; }

   public virtual DbSet<AGenConfigIModule> AGenConfigIModules { get; set; }

   public virtual DbSet<AGenConfigJCompanyLinkModule> AGenConfigJCompanyLinkModules { get; set; }

   public virtual DbSet<AGenConfigKBooth> AGenConfigKBooths { get; set; }

   public virtual DbSet<AGenConfigLBoothLinkBranch> AGenConfigLBoothLinkBranches { get; set; }

   public virtual DbSet<AGenConfigMGlobalOption> AGenConfigMGlobalOptions { get; set; }

   public virtual DbSet<BSecAUserGroup> BSecAUserGroups { get; set; }

   public virtual DbSet<BSecBUser> BSecBUsers { get; set; }

   public virtual DbSet<BSecCLinkUserBranch> BSecCLinkUserBranches { get; set; }

   public virtual DbSet<BSecDScreen> BSecDScreens { get; set; }

   public virtual DbSet<BSecELinkUserGroupScreen> BSecELinkUserGroupScreens { get; set; }

   public virtual DbSet<BSecFScreenCommand> BSecFScreenCommands { get; set; }

   public virtual DbSet<BSecGLinkUserGroupScreenCommand> BSecGLinkUserGroupScreenCommands { get; set; }

   public virtual DbSet<BSecHUserLoginTracking> BSecHUserLoginTrackings { get; set; }

   public virtual DbSet<CCompACompany> CCompACompanies { get; set; }

   public virtual DbSet<CCompBBranch> CCompBBranches { get; set; }

   public virtual DbSet<CCompCTransportType> CCompCTransportTypes { get; set; }

   public virtual DbSet<CCompDTransport> CCompDTransports { get; set; }

   public virtual DbSet<DHrASalaryPayScal> DHrASalaryPayScals { get; set; }

   public virtual DbSet<DHrBEmployeeCategory> DHrBEmployeeCategories { get; set; }

   public virtual DbSet<DHrCEmployeeDivision> DHrCEmployeeDivisions { get; set; }

   public virtual DbSet<DHrDEmployeeDepartment> DHrDEmployeeDepartments { get; set; }

   public virtual DbSet<DHrEEmployeeDesignation> DHrEEmployeeDesignations { get; set; }

   public virtual DbSet<DHrFEmployeeJobRefNo> DHrFEmployeeJobRefNos { get; set; }

   public virtual DbSet<DHrJEmployee> DHrJEmployees { get; set; }

   public virtual DbSet<DHrKReferenceType> DHrKReferenceTypes { get; set; }

   public virtual DbSet<DHrLPresentAddress> DHrLPresentAddresses { get; set; }

   public virtual DbSet<DHrMPermanentAddress> DHrMPermanentAddresses { get; set; }

   public virtual DbSet<EAccAFinancialYear> EAccAFinancialYears { get; set; }

   public virtual DbSet<EAccBChartOfAccount> EAccBChartOfAccounts { get; set; }

   public virtual DbSet<EAccCJournalMaster> EAccCJournalMasters { get; set; }

   public virtual DbSet<EAccDJournalDetail> EAccDJournalDetails { get; set; }

   public virtual DbSet<FEduAClass> FEduAClasses { get; set; }

   public virtual DbSet<FEduAStudent> FEduAStudents { get; set; }

   public virtual DbSet<FEduAaStudentAdmission> FEduAaStudentAdmissions { get; set; }

   public virtual DbSet<FEduBBuilding> FEduBBuildings { get; set; }

   public virtual DbSet<FEduBClassOrHallRoom> FEduBClassOrHallRooms { get; set; }

   public virtual DbSet<FEduCClassOrHall> FEduCClassOrHalls { get; set; }

   public virtual DbSet<FEduDStudentAllocateHallSeat> FEduDStudentAllocateHallSeats { get; set; }

   public virtual DbSet<FEduETransportArea> FEduETransportAreas { get; set; }

   public virtual DbSet<FEduExamAGradePoint> FEduExamAGradePoints { get; set; }

   public virtual DbSet<FEduExamBExamShortCode> FEduExamBExamShortCodes { get; set; }

   public virtual DbSet<FEduFTransportCharge> FEduFTransportCharges { get; set; }

   public virtual DbSet<FEduGLinkTransportArea> FEduGLinkTransportAreas { get; set; }

   public virtual DbSet<FEduHStudentAllocateTransport> FEduHStudentAllocateTransports { get; set; }

   public virtual DbSet<FEduJClassSection> FEduJClassSections { get; set; }

   public virtual DbSet<FEduKClassGroup> FEduKClassGroups { get; set; }

   public virtual DbSet<FEduLClassShift> FEduLClassShifts { get; set; }

   public virtual DbSet<FEduMClassSubject> FEduMClassSubjects { get; set; }

   public virtual DbSet<FEduNLinkClassGroup> FEduNLinkClassGroups { get; set; }

   public virtual DbSet<FEduOLinkClassSection> FEduOLinkClassSections { get; set; }

   public virtual DbSet<FEduPLinkClassShift> FEduPLinkClassShifts { get; set; }

   public virtual DbSet<FEduQLinkClassSubject> FEduQLinkClassSubjects { get; set; }

   public virtual DbSet<FEduRExam> FEduRExams { get; set; }

   public virtual DbSet<FEduSAcademicYear> FEduSAcademicYears { get; set; }

   public virtual DbSet<FEduTAcademicSession> FEduTAcadeicSessions { get; set; }

   public virtual DbSet<FEduUClassPeriod> FEduUClassPeriods { get; set; }

   //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
   //      => optionsBuilder.UseSqlServer("Server=103.125.254.20,9433;Database=pberp_development;TrustServerCertificate=True;User ID=pberp_web;Password=pbd@@123!@#");

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.HasDefaultSchema("pberp_web");

      modelBuilder.Entity<AGenConfigACountry>(entity =>
      {
         entity.HasKey(e => e.CountryId).HasName("PK_Comp_1_CountryInfo");

         entity.ToTable("A_GenConfig_A_Country", "dbo");

         entity.Property(e => e.CountryId)
               .ValueGeneratedNever()
               .HasColumnName("CountryID");
         entity.Property(e => e.CountryName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CountryNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<AGenConfigBDivisionOrState>(entity =>
      {
         entity.HasKey(e => e.DivisionId).HasName("PK_Comp_2_DivisionInfo");

         entity.ToTable("A_GenConfig_B_DivisionOrState", "dbo");

         entity.Property(e => e.DivisionId)
               .ValueGeneratedNever()
               .HasColumnName("DivisionID");
         entity.Property(e => e.CountryId).HasColumnName("CountryID");
         entity.Property(e => e.DivisionName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.DivisionNameLocal).HasMaxLength(50);

         entity.HasOne(d => d.Country).WithMany(p => p.AGenConfigBDivisionOrStates)
               .HasForeignKey(d => d.CountryId)
               .HasConstraintName("FK_SoftConfig_B_DivisionOrStateInfo_SoftConfig_A_CountryInfo");
      });

      modelBuilder.Entity<AGenConfigCDistrictOrCity>(entity =>
      {
         entity.HasKey(e => e.DistrictId).HasName("PK_Comp_2_DistrictInfo");

         entity.ToTable("A_GenConfig_C_DistrictOrCity", "dbo");

         entity.Property(e => e.DistrictId)
               .ValueGeneratedNever()
               .HasColumnName("DistrictID");
         entity.Property(e => e.DistrictName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.DistrictNameLocal).HasMaxLength(50);
         entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

         entity.HasOne(d => d.Division).WithMany(p => p.AGenConfigCDistrictOrCities)
               .HasForeignKey(d => d.DivisionId)
               .HasConstraintName("FK_SoftConfig_C_DistrictOrCityInfo_SoftConfig_B_DivisionOrStateInfo");
      });

      modelBuilder.Entity<AGenConfigDPoliceStation>(entity =>
      {
         entity.HasKey(e => e.PoliceStationId).HasName("PK_Comp_3_PoliceStationInfo");

         entity.ToTable("A_GenConfig_D_PoliceStation", "dbo");

         entity.Property(e => e.PoliceStationId)
               .ValueGeneratedNever()
               .HasColumnName("PoliceStationID");
         entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
         entity.Property(e => e.PoliceStationName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.PoliceStationNameLocal).HasMaxLength(50);
         entity.Property(e => e.PostalCode)
               .HasMaxLength(50)
               .IsUnicode(false);

         entity.HasOne(d => d.District).WithMany(p => p.AGenConfigDPoliceStations)
               .HasForeignKey(d => d.DistrictId)
               .HasConstraintName("FK_SoftConfig_D_PoliceStationInfo_SoftConfig_C_DistrictOrCityInfo");
      });

      modelBuilder.Entity<AGenConfigEBusinessType>(entity =>
      {
         entity.HasKey(e => e.BusinessTypeId).HasName("PK_Config_1_BusinessInfo");

         entity.ToTable("A_GenConfig_E_BusinessType", "dbo");

         entity.Property(e => e.BusinessTypeId)
               .ValueGeneratedNever()
               .HasColumnName("BusinessTypeID");
         entity.Property(e => e.BusinessTypeName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.BusinessTypeNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<AGenConfigEGender>(entity =>
      {
         entity.HasKey(e => e.GenderId).HasName("PK_HR_G_EmployeeGenderInfo");

         entity.ToTable("A_GenConfig_E_Gender", "dbo");

         entity.Property(e => e.GenderId)
               .ValueGeneratedNever()
               .HasColumnName("GenderID");
         entity.Property(e => e.GenderName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.GenderNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<AGenConfigFBloodGroup>(entity =>
      {
         entity.HasKey(e => e.BloodGroupId).HasName("PK_HR_H_BloodGroupInfo");

         entity.ToTable("A_GenConfig_F_BloodGroup", "dbo");

         entity.Property(e => e.BloodGroupId)
               .ValueGeneratedNever()
               .HasColumnName("BloodGroupID");
         entity.Property(e => e.BloodGroupName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BloodGroupNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<AGenConfigFLanguage>(entity =>
      {
         entity.HasKey(e => e.LanguageId).HasName("PK_Config_3_LanguageName");

         entity.ToTable("A_GenConfig_F_Language", "dbo");

         entity.Property(e => e.LanguageId)
               .ValueGeneratedNever()
               .HasColumnName("LanguageID");
         entity.Property(e => e.LanguageName)
               .HasMaxLength(50)
               .IsUnicode(false);
      });

      modelBuilder.Entity<AGenConfigGFont>(entity =>
      {
         entity.HasKey(e => e.FontId).HasName("PK_Config_4_FontInfo");

         entity.ToTable("A_GenConfig_G_Font", "dbo");

         entity.Property(e => e.FontId)
               .ValueGeneratedNever()
               .HasColumnName("FontID");
         entity.Property(e => e.FontName)
               .HasMaxLength(50)
               .IsUnicode(false);
      });

      modelBuilder.Entity<AGenConfigGReligion>(entity =>
      {
         entity.HasKey(e => e.ReligionId).HasName("PK_HR_I_ReligionInfo");

         entity.ToTable("A_GenConfig_G_Religion", "dbo");

         entity.Property(e => e.ReligionId)
               .ValueGeneratedNever()
               .HasColumnName("ReligionID");
         entity.Property(e => e.ReligionName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ReligionNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<AGenConfigHInvoiceMode>(entity =>
      {
         entity.HasKey(e => e.InvoiceModeId).HasName("PK_Config_5_InvoiceModeInfo");

         entity.ToTable("A_GenConfig_H_InvoiceMode", "dbo");

         entity.Property(e => e.InvoiceModeId)
               .ValueGeneratedNever()
               .HasColumnName("InvoiceModeID");
         entity.Property(e => e.InvoiceModeName)
               .HasMaxLength(50)
               .IsUnicode(false);
      });

      modelBuilder.Entity<AGenConfigIModule>(entity =>
      {
         entity.HasKey(e => e.ModuleId).HasName("PK_Config_2_BusinessWiseCompanyFormView");

         entity.ToTable("A_GenConfig_I_Module", "dbo");

         entity.Property(e => e.ModuleId)
               .ValueGeneratedNever()
               .HasColumnName("ModuleID");
         entity.Property(e => e.ModuleName)
               .HasMaxLength(150)
               .IsUnicode(false);
      });

      modelBuilder.Entity<AGenConfigJCompanyLinkModule>(entity =>
      {
         entity.HasKey(e => e.CompanyLinkModuleId).HasName("PK_SoftConfig_3_BusinessLinkModule");

         entity.ToTable("A_GenConfig_J_CompanyLinkModule", "dbo");

         entity.Property(e => e.CompanyLinkModuleId)
               .ValueGeneratedNever()
               .HasColumnName("CompanyLinkModuleID");
         entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
         entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

         entity.HasOne(d => d.Company).WithMany(p => p.AGenConfigJCompanyLinkModules)
               .HasForeignKey(d => d.CompanyId)
               .HasConstraintName("FK_SoftConfig_J_CompanyLinkModule_Comp_A_CompanyInfo");

         entity.HasOne(d => d.Module).WithMany(p => p.AGenConfigJCompanyLinkModules)
               .HasForeignKey(d => d.ModuleId)
               .HasConstraintName("FK_SoftConfig_J_CompanyLinkModule_SoftConfig_I_ModuleInfo");
      });

      modelBuilder.Entity<AGenConfigKBooth>(entity =>
      {
         entity.HasKey(e => e.BoothId).HasName("PK_Config_4_BoothInfo");

         entity.ToTable("A_GenConfig_K_Booth", "dbo");

         entity.Property(e => e.BoothId)
               .ValueGeneratedNever()
               .HasColumnName("BoothID");
         entity.Property(e => e.BoothCode)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BoothName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ComputerName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.FontId).HasColumnName("FontID");
         entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

         entity.HasOne(d => d.Font).WithMany(p => p.AGenConfigKBooths)
               .HasForeignKey(d => d.FontId)
               .HasConstraintName("FK_SoftConfig_K_BoothInfo_SoftConfig_G_FontInfo");

         entity.HasOne(d => d.InvoiceMode).WithMany(p => p.AGenConfigKBooths)
               .HasForeignKey(d => d.InvoiceModeId)
               .HasConstraintName("FK_SoftConfig_K_BoothInfo_SoftConfig_H_InvoiceModeInfo");

         entity.HasOne(d => d.Language).WithMany(p => p.AGenConfigKBooths)
               .HasForeignKey(d => d.LanguageId)
               .HasConstraintName("FK_SoftConfig_K_BoothInfo_SoftConfig_F_LanguageInfo");
      });

      modelBuilder.Entity<AGenConfigLBoothLinkBranch>(entity =>
      {
         entity
               .HasNoKey()
               .ToTable("A_GenConfig_L_BoothLinkBranch", "dbo");

         entity.Property(e => e.BoothId).HasColumnName("BoothID");
         entity.Property(e => e.BoothLinkBranchId).HasColumnName("BoothLinkBranchID");
         entity.Property(e => e.BranchId).HasColumnName("BranchID");

         entity.HasOne(d => d.Booth).WithMany()
               .HasForeignKey(d => d.BoothId)
               .HasConstraintName("FK_SoftConfig_L_BoothLinkBranch_SoftConfig_K_BoothInfo");

         entity.HasOne(d => d.Branch).WithMany()
               .HasForeignKey(d => d.BranchId)
               .HasConstraintName("FK_SoftConfig_L_BoothLinkBranch_Comp_B_BranchInfo");
      });

      modelBuilder.Entity<AGenConfigMGlobalOption>(entity =>
      {
         entity.HasKey(e => e.GlobalOptionId).HasName("PK_Config_4_GlobalOption");

         entity.ToTable("A_GenConfig_M_GlobalOption", "dbo");

         entity.Property(e => e.GlobalOptionId)
               .ValueGeneratedNever()
               .HasColumnName("GlobalOptionID");
         entity.Property(e => e.BoothId)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("BoothID");
         entity.Property(e => e.BranchId)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.GlobalOptionDefaultValue)
               .HasMaxLength(2550)
               .IsUnicode(false);
         entity.Property(e => e.GlobalOptionDescription)
               .HasMaxLength(2550)
               .IsUnicode(false);
         entity.Property(e => e.GlobalOptionValue)
               .HasMaxLength(2550)
               .IsUnicode(false);
         entity.Property(e => e.GlobalOptionValueDefinition)
               .HasMaxLength(2550)
               .IsUnicode(false);
      });

      modelBuilder.Entity<BSecAUserGroup>(entity =>
      {
         entity.HasKey(e => e.UserGroupId).HasName("PK_Sec_A_UserGroupInfo");

         entity.ToTable("B_Sec_A_UserGroup", "dbo");

         entity.Property(e => e.UserGroupId)
               .ValueGeneratedNever()
               .HasColumnName("UserGroupID");
         entity.Property(e => e.UserGroupDescription)
               .HasMaxLength(250)
               .IsUnicode(false);
         entity.Property(e => e.UserGroupDescriptionLocal).HasMaxLength(50);
         entity.Property(e => e.UserGroupName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.UserGroupNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<BSecBUser>(entity =>
      {
         entity.HasKey(e => e.UserId).HasName("PK_Sec_B_UserInfo");

         entity.ToTable("B_Sec_B_User", "dbo");

         entity.Property(e => e.UserId)
               .ValueGeneratedNever()
               .HasColumnName("UserID");
         entity.Property(e => e.EndDate).HasColumnType("date");
         entity.Property(e => e.LoginName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.LoginNameLocal).HasMaxLength(50);
         entity.Property(e => e.Password)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.StartDate).HasColumnType("date");
         entity.Property(e => e.UserGroupId).HasColumnName("UserGroupID");

         entity.HasOne(d => d.UserGroup).WithMany(p => p.BSecBUsers)
               .HasForeignKey(d => d.UserGroupId)
               .HasConstraintName("FK_Sec_B_User_Sec_A_UserGroup");
      });

      modelBuilder.Entity<BSecCLinkUserBranch>(entity =>
      {
         entity.HasKey(e => e.LinkUserBranchId).HasName("PK_Sec_C_LinkUserBranchInfo");

         entity.ToTable("B_Sec_C_LinkUserBranch", "dbo");

         entity.Property(e => e.LinkUserBranchId)
               .ValueGeneratedNever()
               .HasColumnName("LinkUserBranchID");
         entity.Property(e => e.BranchId).HasColumnName("BranchID");
         entity.Property(e => e.UserId).HasColumnName("UserID");

         entity.HasOne(d => d.Branch).WithMany(p => p.BSecCLinkUserBranches)
               .HasForeignKey(d => d.BranchId)
               .HasConstraintName("FK_Sec_C_LinkUserBranchInfo_Comp_B_BranchInfo");
      });

      modelBuilder.Entity<BSecDScreen>(entity =>
      {
         entity.HasKey(e => e.ScreenId).HasName("PK_Sec_D_ScreenInfo");

         entity.ToTable("B_Sec_D_Screen", "dbo");

         entity.Property(e => e.ScreenId)
               .ValueGeneratedNever()
               .HasColumnName("ScreenID");
         entity.Property(e => e.ActionName)
               .HasMaxLength(250)
               .IsUnicode(false);
         entity.Property(e => e.ControllerName)
               .HasMaxLength(250)
               .IsUnicode(false);
         entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
         entity.Property(e => e.ParentId).HasColumnName("ParentID");
         entity.Property(e => e.ScreenName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.ScreenNameInLocal).HasMaxLength(150);

         entity.HasOne(d => d.Module).WithMany(p => p.BSecDScreens)
               .HasForeignKey(d => d.ModuleId)
               .HasConstraintName("FK_Sec_D_ScreenInfo_SoftConfig_I_ModuleInfo");
      });

      modelBuilder.Entity<BSecELinkUserGroupScreen>(entity =>
      {
         entity.HasKey(e => e.LinkUserGroupScreenId).HasName("PK_Sec_E_LinkUserGroupScreenI");

         entity.ToTable("B_Sec_E_LinkUserGroupScreen", "dbo");

         entity.Property(e => e.LinkUserGroupScreenId)
               .ValueGeneratedNever()
               .HasColumnName("LinkUserGroupScreenID");
         entity.Property(e => e.ScreenId).HasColumnName("ScreenID");
         entity.Property(e => e.UserGroupId).HasColumnName("UserGroupID");

         entity.HasOne(d => d.Screen).WithMany(p => p.BSecELinkUserGroupScreens)
               .HasForeignKey(d => d.ScreenId)
               .HasConstraintName("FK_Sec_E_LinkUserGroupScreenInfo_Sec_D_ScreenInfo");

         entity.HasOne(d => d.UserGroup).WithMany(p => p.BSecELinkUserGroupScreens)
               .HasForeignKey(d => d.UserGroupId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Sec_E_LinkUserGroupScreenInfo_Sec_A_UserGroupInfo");
      });

      modelBuilder.Entity<BSecFScreenCommand>(entity =>
      {
         entity.HasKey(e => e.ScreenCommandId).HasName("PK_Sec_F_ScreenCommandInfo");

         entity.ToTable("B_Sec_F_ScreenCommand", "dbo");

         entity.Property(e => e.ScreenCommandId)
               .ValueGeneratedNever()
               .HasColumnName("ScreenCommandID");
         entity.Property(e => e.Description)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.ScreenCommandName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.ScreenCommandNameLocal).HasMaxLength(150);
         entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

         entity.HasOne(d => d.Screen).WithMany(p => p.BSecFScreenCommands)
               .HasForeignKey(d => d.ScreenId)
               .HasConstraintName("FK_Sec_F_ScreenCommandInfo_Sec_D_ScreenInfo");
      });

      modelBuilder.Entity<BSecGLinkUserGroupScreenCommand>(entity =>
      {
         entity.HasKey(e => e.LinkUserGroupScreenCommandId).HasName("PK_LinkUserGroupScreenCommandInfo");

         entity.ToTable("B_Sec_G_LinkUserGroupScreenCommand", "dbo");

         entity.Property(e => e.LinkUserGroupScreenCommandId)
               .ValueGeneratedNever()
               .HasColumnName("LinkUserGroupScreenCommandID");
         entity.Property(e => e.ScreenCommandId).HasColumnName("ScreenCommandID");
         entity.Property(e => e.UserGroupId).HasColumnName("UserGroupID");

         entity.HasOne(d => d.ScreenCommand).WithMany(p => p.BSecGLinkUserGroupScreenCommands)
               .HasForeignKey(d => d.ScreenCommandId)
               .HasConstraintName("FK_Sec_G_LinkUserGroupScreenCommandInfo_Sec_F_ScreenCommandInfo");

         entity.HasOne(d => d.UserGroup).WithMany(p => p.BSecGLinkUserGroupScreenCommands)
               .HasForeignKey(d => d.UserGroupId)
               .HasConstraintName("FK_Sec_G_LinkUserGroupScreenCommandInfo_Sec_A_UserGroupInfo");
      });

      modelBuilder.Entity<BSecHUserLoginTracking>(entity =>
      {
         entity.HasKey(e => e.UserLoginTrackingId).HasName("PK_Sec_H_UserLoginTracking");

         entity.ToTable("B_Sec_H_UserLoginTracking", "dbo");

         entity.Property(e => e.UserLoginTrackingId)
               .ValueGeneratedNever()
               .HasColumnName("UserLoginTrackingID");
         entity.Property(e => e.ActionType)
               .HasMaxLength(250)
               .IsUnicode(false);
         entity.Property(e => e.ComputerName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.LoginDate).HasColumnType("datetime");
         entity.Property(e => e.LoginTime).HasColumnType("datetime");
         entity.Property(e => e.UserId).HasColumnName("UserID");
      });

      modelBuilder.Entity<CCompACompany>(entity =>
      {
         entity.HasKey(e => e.CompanyId).HasName("PK_Comp_1_CompanyInfo");

         entity.ToTable("C_Comp_A_Company", "dbo");

         entity.Property(e => e.CompanyId)
               .ValueGeneratedNever()
               .HasColumnName("CompanyID");
         entity.Property(e => e.BusinessTypeId).HasColumnName("BusinessTypeID");
         entity.Property(e => e.CompanyCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CompanyAddress)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.CompanyAddressLocal).HasMaxLength(150);
         entity.Property(e => e.CompanyBackgroundImage).HasColumnType("image");
         entity.Property(e => e.CompanyBin)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("CompanyBIN");
         entity.Property(e => e.CompanyEin)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("CompanyEIN");
         entity.Property(e => e.CompanyEmailAddress)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CompanyFax)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CompanyLogo).HasColumnType("image");
         entity.Property(e => e.CompanyName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.CompanyNameLocal).HasMaxLength(150);
         entity.Property(e => e.CompanyPhone)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CompanyTin)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("CompanyTIN");
         entity.Property(e => e.CompanyVatRegistration)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CompanyWebAddress)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.CompanyWhatsApp)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.GroupOfCompanyName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.GroupOfCompanyNameLocal)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.PoliceStationId).HasColumnName("PoliceStationID");

         entity.HasOne(d => d.BusinessType).WithMany(p => p.CCompACompanies)
               .HasForeignKey(d => d.BusinessTypeId)
               .HasConstraintName("FK_Comp_A_CompanyInfo_SoftConfig_E_BusinessTypeInfo");

         entity.HasOne(d => d.PoliceStation).WithMany(p => p.CCompACompanies)
               .HasForeignKey(d => d.PoliceStationId)
               .HasConstraintName("FK_Comp_A_CompanyInfo_SoftConfig_D_PoliceStationInfo");
      });

      modelBuilder.Entity<CCompBBranch>(entity =>
      {
         entity.HasKey(e => e.BranchId).HasName("PK_Comp_2_BranchInfo");

         entity.ToTable("C_Comp_B_Branch", "dbo");

         entity.Property(e => e.BranchId)
               .ValueGeneratedNever()
               .HasColumnName("BranchID");
         entity.Property(e => e.BranchAddress)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.BranchAddressLocal).HasMaxLength(150);
         entity.Property(e => e.BranchCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BranchName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BranchNameLocal).HasMaxLength(150);
         entity.Property(e => e.BranchPhone)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BranchWhatsApp)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.PoliceStationId).HasColumnName("PoliceStationID");

         entity.HasOne(d => d.Comapany).WithMany(p => p.CCompBBranches)
               .HasForeignKey(d => d.ComapanyId)
               .HasConstraintName("FK_Comp_B_BranchInfo_Comp_A_CompanyInfo");
      });

      modelBuilder.Entity<CCompCTransportType>(entity =>
      {
         entity.HasKey(e => e.TransportTypeId).HasName("PK_Comp_C_TransportInfo");

         entity.ToTable("C_Comp_C_TransportType", "dbo");

         entity.Property(e => e.TransportTypeId)
               .ValueGeneratedNever()
               .HasColumnName("TransportTypeID");
         entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
         entity.Property(e => e.TransportType)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.TransportTypeLocal).HasMaxLength(150);

         entity.HasOne(d => d.Company).WithMany(p => p.CCompCTransportTypes)
               .HasForeignKey(d => d.CompanyId)
               .HasConstraintName("FK_Comp_C_TransportTypeInfo_Comp_A_CompanyInfo");
      });

      modelBuilder.Entity<CCompDTransport>(entity =>
      {
         entity.HasKey(e => e.TransportId).HasName("PK_Edu_D_TransportInfo");

         entity.ToTable("C_Comp_D_Transport", "dbo");

         entity.Property(e => e.TransportId)
               .ValueGeneratedNever()
               .HasColumnName("TransportID");
         entity.Property(e => e.Ait).HasColumnName("AIT");
         entity.Property(e => e.FitnessExpiry).HasColumnType("datetime");
         entity.Property(e => e.TaxTokenExpiry).HasColumnType("datetime");
         entity.Property(e => e.TransportEngineNo)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.TransportName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.TransportNameLocal).HasMaxLength(150);
         entity.Property(e => e.TransportRegNo)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.TransportTypeId).HasColumnName("TransportTypeID");

         entity.HasOne(d => d.TransportType).WithMany(p => p.CCompDTransports)
               .HasForeignKey(d => d.TransportTypeId)
               .HasConstraintName("FK_Comp_D_TransportInfo_Comp_C_TransportTypeInfo");
      });

      modelBuilder.Entity<DHrASalaryPayScal>(entity =>
      {
         entity.HasKey(e => e.SalaryPayScalId).HasName("PK_HR_SalaryPayScalInfo");

         entity.ToTable("D_HR_A_SalaryPayScal", "dbo");

         entity.Property(e => e.SalaryPayScalId)
               .ValueGeneratedNever()
               .HasColumnName("SalaryPayScalID");
         entity.Property(e => e.BasicSalary).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
         entity.Property(e => e.CompanyProvidentPercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.ConveyanceAllowance).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.ConveyancePercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.EmployeeProvidentFundPercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.FoodAllowance).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.FoodPercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.HouseRent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.HouseRentPercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.KallanTahabilPercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.MedicalAllowance).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.MedicalPercent).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.TotalSalary).HasColumnType("numeric(18, 2)");
      });

      modelBuilder.Entity<DHrBEmployeeCategory>(entity =>
      {
         entity.HasKey(e => e.EmployeeCategoryId).HasName("PK_HR_B_EmployeeCategoryInfo");

         entity.ToTable("D_HR_B_EmployeeCategory", "dbo");

         entity.Property(e => e.EmployeeCategoryId)
               .ValueGeneratedNever()
               .HasColumnName("EmployeeCategoryID");
         entity.Property(e => e.EmployeeCategory)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeCategoryLocal).HasMaxLength(50);
         entity.Property(e => e.EmployeeGrade)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeGradeLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<DHrCEmployeeDivision>(entity =>
      {
         entity.HasKey(e => e.EmployeeDivisionId).HasName("PK_HR_C_EmployeeDivisionInfo");

         entity.ToTable("D_HR_C_EmployeeDivision", "dbo");

         entity.Property(e => e.EmployeeDivisionId)
               .ValueGeneratedNever()
               .HasColumnName("EmployeeDivisionID");
         entity.Property(e => e.EmployeeDivision)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeDivisionLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<DHrDEmployeeDepartment>(entity =>
      {
         entity.HasKey(e => e.EmployeeDepartmentId).HasName("PK_HR_D_EmployeeDepartmentInfo");

         entity.ToTable("D_HR_D_EmployeeDepartment", "dbo");

         entity.Property(e => e.EmployeeDepartmentId)
               .ValueGeneratedNever()
               .HasColumnName("EmployeeDepartmentID");
         entity.Property(e => e.EmployeeDepartment)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeDepartmentLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<DHrEEmployeeDesignation>(entity =>
      {
         entity.HasKey(e => e.EmployeeDesignationId).HasName("PK_HR_E_EmployeeDesignationInfo");

         entity.ToTable("D_HR_E_EmployeeDesignation", "dbo");

         entity.Property(e => e.EmployeeDesignationId)
               .ValueGeneratedNever()
               .HasColumnName("EmployeeDesignationID");
         entity.Property(e => e.EmployeeDesignation)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeDesignationLoacl).HasMaxLength(50);
      });

      modelBuilder.Entity<DHrFEmployeeJobRefNo>(entity =>
      {
         entity.HasKey(e => e.EmployeeJobRefNoId).HasName("PK_HR_F_EmployeeJobRefNoInfo");

         entity.ToTable("D_HR_F_EmployeeJobRefNo", "dbo");

         entity.Property(e => e.EmployeeJobRefNoId)
               .ValueGeneratedNever()
               .HasColumnName("EmployeeJobRefNoID");
         entity.Property(e => e.EmployeeCategoryId).HasColumnName("EmployeeCategoryID");
         entity.Property(e => e.EmployeeDepartmentId).HasColumnName("EmployeeDepartmentID");
         entity.Property(e => e.EmployeeDesignationId).HasColumnName("EmployeeDesignationID");
         entity.Property(e => e.EmployeeDivisionId).HasColumnName("EmployeeDivisionID");
         entity.Property(e => e.EmployeeJobRefNo)
               .HasMaxLength(50)
               .IsUnicode(false);

         entity.HasOne(d => d.EmployeeCategory).WithMany(p => p.DHrFEmployeeJobRefNos)
               .HasForeignKey(d => d.EmployeeCategoryId)
               .HasConstraintName("FK_HR_F_EmployeeJobRefNoInfo_HR_B_EmployeeCategoryInfo");

         entity.HasOne(d => d.EmployeeDepartment).WithMany(p => p.DHrFEmployeeJobRefNos)
               .HasForeignKey(d => d.EmployeeDepartmentId)
               .HasConstraintName("FK_HR_F_EmployeeJobRefNoInfo_HR_D_EmployeeDepartmentInfo");

         entity.HasOne(d => d.EmployeeDesignation).WithMany(p => p.DHrFEmployeeJobRefNos)
               .HasForeignKey(d => d.EmployeeDesignationId)
               .HasConstraintName("FK_HR_F_EmployeeJobRefNoInfo_HR_E_EmployeeDesignationInfo");

         entity.HasOne(d => d.EmployeeDivision).WithMany(p => p.DHrFEmployeeJobRefNos)
               .HasForeignKey(d => d.EmployeeDivisionId)
               .HasConstraintName("FK_HR_F_EmployeeJobRefNoInfo_HR_C_EmployeeDivisionInfo");
      });

      modelBuilder.Entity<DHrJEmployee>(entity =>
      {
         entity.HasKey(e => e.EmployeeId).HasName("PK_HR_J_EmployeeInfo");

         entity.ToTable("D_HR_J_Employee", "dbo");

         entity.Property(e => e.EmployeeId)
               .ValueGeneratedNever()
               .HasColumnName("EmployeeID");
         entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");
         entity.Property(e => e.EmpPerAdd)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmpPerAddInLocal).HasMaxLength(150);
         entity.Property(e => e.EmpPerAddPsid).HasColumnName("EmpPerAddPSID");
         entity.Property(e => e.EmpPresAdd)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmpPresAddLocal).HasMaxLength(150);
         entity.Property(e => e.EmpPresAddPsid).HasColumnName("EmpPresAddPSID");
         entity.Property(e => e.EmployeeCardNo)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeEmail)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeFathersName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeFathersNameLocal).HasMaxLength(50);
         entity.Property(e => e.EmployeeJobRefNoId).HasColumnName("EmployeeJobRefNoID");
         entity.Property(e => e.EmployeeJoiningDate).HasColumnType("date");
         entity.Property(e => e.EmployeeName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.EmployeeNameLocal).HasMaxLength(50);
         entity.Property(e => e.EmployeeNationalId)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("EmployeeNationalID");
         entity.Property(e => e.EmployeePassportNumber)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.EmployeePhone)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.EmployeePhoto).HasColumnType("image");
         entity.Property(e => e.GenderId).HasColumnName("GenderID");
         entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

         entity.HasOne(d => d.BloodGroup).WithMany(p => p.DHrJEmployees)
               .HasForeignKey(d => d.BloodGroupId)
               .HasConstraintName("FK_D_HR_J_Employee_A_GenConfig_F_BloodGroup");

         entity.HasOne(d => d.EmpPresAddPs).WithMany(p => p.DHrJEmployees)
               .HasForeignKey(d => d.EmpPresAddPsid)
               .HasConstraintName("FK_D_HR_J_Employee_A_GenConfig_D_PoliceStation");

         entity.HasOne(d => d.EmployeeJobRefNo).WithMany(p => p.DHrJEmployees)
               .HasForeignKey(d => d.EmployeeJobRefNoId)
               .HasConstraintName("FK_HR_J_EmployeeInfo_HR_F_EmployeeJobRefNoInfo");

         entity.HasOne(d => d.Gender).WithMany(p => p.DHrJEmployees)
               .HasForeignKey(d => d.GenderId)
               .HasConstraintName("FK_D_HR_J_Employee_A_GenConfig_E_Gender");

         entity.HasOne(d => d.Religion).WithMany(p => p.DHrJEmployees)
               .HasForeignKey(d => d.ReligionId)
               .HasConstraintName("FK_D_HR_J_Employee_A_GenConfig_G_Religion");
      });

      modelBuilder.Entity<DHrKReferenceType>(entity =>
      {
         entity.HasKey(e => e.ReferenceTypeId).HasName("PK_HR_K_ReferenceTypeInfo");

         entity.ToTable("D_HR_K_ReferenceType", "dbo");

         entity.Property(e => e.ReferenceTypeId)
               .ValueGeneratedNever()
               .HasColumnName("ReferenceTypeID");
         entity.Property(e => e.ReferenceTypeName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ReferenceTypeNameLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<DHrLPresentAddress>(entity =>
      {
         entity.HasKey(e => e.PresentAddressId).HasName("PK_HR_K_PresentAddressInfo");

         entity.ToTable("D_HR_L_PresentAddress", "dbo");

         entity.Property(e => e.PresentAddressId)
               .ValueGeneratedNever()
               .HasColumnName("PresentAddressID");
         entity.Property(e => e.PoliceStationId).HasColumnName("PoliceStationID");
         entity.Property(e => e.PostOffice)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.PostOfficeLocal).HasMaxLength(50);
         entity.Property(e => e.PresentAddress)
               .HasMaxLength(250)
               .IsUnicode(false);
         entity.Property(e => e.PresentAddressLocal).HasMaxLength(50);
         entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
         entity.Property(e => e.ReferenceTypeId).HasColumnName("ReferenceTypeID");

         entity.HasOne(d => d.PoliceStation).WithMany(p => p.DHrLPresentAddresses)
               .HasForeignKey(d => d.PoliceStationId)
               .HasConstraintName("FK_HR_L_PresentAddressInfo_SoftConfig_D_PoliceStationInfo");

         entity.HasOne(d => d.ReferenceType).WithMany(p => p.DHrLPresentAddresses)
               .HasForeignKey(d => d.ReferenceTypeId)
               .HasConstraintName("FK_HR_L_PresentAddressInfo_HR_K_ReferenceTypeInfo");
      });

      modelBuilder.Entity<DHrMPermanentAddress>(entity =>
      {
         entity.HasKey(e => e.PermanentAddressId).HasName("PK_HR_M_PermanentAddressInfo");

         entity.ToTable("D_HR_M_PermanentAddress", "dbo");

         entity.Property(e => e.PermanentAddressId)
               .ValueGeneratedNever()
               .HasColumnName("PermanentAddressID");
         entity.Property(e => e.PermanentAddress)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.PermanentAddressLocal).HasMaxLength(50);
         entity.Property(e => e.PoliceStationId).HasColumnName("PoliceStationID");
         entity.Property(e => e.PostOffice)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.PostOfficeLocal).HasMaxLength(50);
         entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");
         entity.Property(e => e.ReferenceTypeId).HasColumnName("ReferenceTypeID");

         entity.HasOne(d => d.PoliceStation).WithMany(p => p.DHrMPermanentAddresses)
               .HasForeignKey(d => d.PoliceStationId)
               .HasConstraintName("FK_HR_M_PermanentAddressInfo_SoftConfig_D_PoliceStationInfo");

         entity.HasOne(d => d.ReferenceType).WithMany(p => p.DHrMPermanentAddresses)
               .HasForeignKey(d => d.ReferenceTypeId)
               .HasConstraintName("FK_HR_M_PermanentAddressInfo_HR_K_ReferenceTypeInfo");
      });

      modelBuilder.Entity<EAccAFinancialYear>(entity =>
      {
         entity.HasKey(e => e.FinancialYearId).HasName("PK_FinalcialYear");

         entity.ToTable("E_ACC_A_FinancialYear", "dbo");

         entity.Property(e => e.FinancialYearId)
               .ValueGeneratedNever()
               .HasColumnName("FinancialYearID");
         entity.Property(e => e.ClosedDate).HasColumnType("datetime");
         entity.Property(e => e.EndDate).HasColumnType("datetime");
         entity.Property(e => e.FinancialYear)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.StartDate).HasColumnType("datetime");
         entity.Property(e => e.Udate)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
      });

      modelBuilder.Entity<EAccBChartOfAccount>(entity =>
      {
         entity.HasKey(e => e.ChartOfAccountId).HasName("PK_Acc_A_ChartOfAccount");

         entity.ToTable("E_ACC_B_ChartOfAccount", "dbo");

         entity.Property(e => e.ChartOfAccountId)
               .ValueGeneratedNever()
               .HasColumnName("ChartOfAccountID");
         entity.Property(e => e.ChartOfAccountInitialBalance).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.ChartOfAccountNameIn)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.ChartOfAccountNameInLocal).HasMaxLength(150);
         entity.Property(e => e.ChartOfAccountNo)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ChartOfAccountParentId).HasColumnName("ChartOfAccountParentID");
         entity.Property(e => e.ChartOfAccountReserveAmount).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.CreateDate)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
         entity.Property(e => e.LastUpdateDate)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
         entity.Property(e => e.ReserveAmountForTheFinancialYearOf)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.UserId).HasColumnName("UserID");
      });

      modelBuilder.Entity<EAccCJournalMaster>(entity =>
      {
         entity.HasKey(e => e.JournalCode).HasName("PK_ACC_C_JournalMaster");

         entity.ToTable("E_ACC_C_JournalMaster", "dbo");

         entity.Property(e => e.JournalCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.AmountReference).IsUnicode(false);
         entity.Property(e => e.BoothCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BranchId).HasColumnName("BranchID");
         entity.Property(e => e.FinancialYearId)
               .HasColumnType("numeric(18, 0)")
               .HasColumnName("FinancialYearID");
         entity.Property(e => e.JournalDate).HasColumnType("datetime");
         entity.Property(e => e.JournalMasterId).HasColumnName("JournalMasterID");
         entity.Property(e => e.PostingBy).HasColumnType("numeric(18, 0)");
         entity.Property(e => e.PostingDate).HasColumnType("datetime");
         entity.Property(e => e.Udate)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
         entity.Property(e => e.Udt)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime")
               .HasColumnName("UDt");
         entity.Property(e => e.UserId)
               .HasColumnType("numeric(18, 2)")
               .HasColumnName("UserID");
         entity.Property(e => e.VoucherDirection)
               .HasMaxLength(250)
               .IsUnicode(false)
               .HasColumnName("Voucher_Direction");
      });

      modelBuilder.Entity<EAccDJournalDetail>(entity =>
      {
         entity.HasKey(e => e.JournalDetailsId).HasName("PK_JournalDetails");

         entity.ToTable("E_ACC_D_JournalDetails", "dbo");

         entity.Property(e => e.JournalDetailsId)
               .ValueGeneratedNever()
               .HasColumnName("JournalDetailsID");
         entity.Property(e => e.BoothCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BranchId).HasColumnName("BranchID");
         entity.Property(e => e.ChartofAccountId).HasColumnName("ChartofAccountID");
         entity.Property(e => e.Credit).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.Debit).HasColumnType("numeric(18, 2)");
         entity.Property(e => e.Description).IsUnicode(false);
         entity.Property(e => e.FinancialYearId).HasColumnName("FinancialYearID");
         entity.Property(e => e.JournalCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.Reference).IsUnicode(false);
         entity.Property(e => e.TrChartOfAccountId).HasColumnName("TrChartOfAccountID");
         entity.Property(e => e.TrDt).HasColumnType("datetime");
         entity.Property(e => e.Udate)
               .HasDefaultValueSql("(getdate())")
               .HasColumnType("datetime");
         entity.Property(e => e.UserId).HasColumnName("UserID");

         entity.HasOne(d => d.ChartofAccount).WithMany(p => p.EAccDJournalDetails)
               .HasForeignKey(d => d.ChartofAccountId)
               .HasConstraintName("FK_ACC_D_JournalDetails_ACC_B_ChartOfAccount");

         entity.HasOne(d => d.JournalCodeNavigation).WithMany(p => p.EAccDJournalDetails)
               .HasForeignKey(d => d.JournalCode)
               .HasConstraintName("FK_ACC_D_JournalDetails_ACC_C_JournalMaster");
      });

      modelBuilder.Entity<FEduAClass>(entity =>
      {
         entity.HasKey(e => e.ClassId).HasName("PK_Edu_A_ClassInfo");

         entity.ToTable("F_Edu_A_Class", "dbo");

         entity.Property(e => e.ClassId)
               .ValueGeneratedNever()
               .HasColumnName("ClassID");
         entity.Property(e => e.ClassName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.ClassNameLocal).HasMaxLength(150);
      });

      modelBuilder.Entity<FEduAStudent>(entity =>
      {
         entity.HasKey(e => e.StudentId).HasName("PK_Edu_A_StudentInfo");

         entity.ToTable("F_Edu_A_Student", "dbo");

         entity.Property(e => e.StudentId)
               .ValueGeneratedNever()
               .HasColumnName("StudentID");
         entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");
         entity.Property(e => e.GenderId).HasColumnName("GenderID");
         entity.Property(e => e.ReligionId).HasColumnName("ReligionID");
         entity.Property(e => e.StudentCode)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.StudentFathersName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.StudentFathersNameInLocal).HasMaxLength(150);
         entity.Property(e => e.StudentMothersName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.StudentMothersNameInLocal).HasMaxLength(150);
         entity.Property(e => e.StudentName)
               .IsRequired()
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.StudentNameInLocal).HasMaxLength(50);
         entity.Property(e => e.StudentPhoto).HasColumnType("image");

         entity.HasOne(d => d.BloodGroup).WithMany(p => p.FEduAStudents)
               .HasForeignKey(d => d.BloodGroupId)
               .HasConstraintName("FK_Edu_A_Student_SoftConfig_F_BloodGroup");

         entity.HasOne(d => d.Gender).WithMany(p => p.FEduAStudents)
               .HasForeignKey(d => d.GenderId)
               .HasConstraintName("FK_Edu_A_Student_SoftConfig_E_Gender");

         entity.HasOne(d => d.Religion).WithMany(p => p.FEduAStudents)
               .HasForeignKey(d => d.ReligionId)
               .HasConstraintName("FK_Edu_A_Student_SoftConfig_G_Religion");
      });

      modelBuilder.Entity<FEduAaStudentAdmission>(entity =>
      {
         entity.HasKey(e => e.StudAdmId).HasName("PK_Edu_AA_StudentAdmission");

         entity.ToTable("F_Edu_AA_StudentAdmission", "dbo");

         entity.Property(e => e.StudAdmId)
               .ValueGeneratedNever()
               .HasColumnName("StudAdmID");
         entity.Property(e => e.StudAdmClassId).HasColumnName("StudAdmClassID");
         entity.Property(e => e.StudAdmDate).HasColumnType("datetime");
         entity.Property(e => e.StudentId).HasColumnName("StudentID");
      });

      modelBuilder.Entity<FEduBBuilding>(entity =>
      {
         entity.HasKey(e => e.BuildingId).HasName("PK_Edu_A_BuildingInfo");

         entity.ToTable("F_Edu_B_Building", "dbo");

         entity.Property(e => e.BuildingId)
               .ValueGeneratedNever()
               .HasColumnName("BuildingID");
         entity.Property(e => e.BuildingName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.BuildingNameLocal).HasMaxLength(150);
         entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
         entity.Property(e => e.UsesType)
               .HasMaxLength(50)
               .IsUnicode(false);

         entity.HasOne(d => d.Company).WithMany(p => p.FEduBBuildings)
               .HasForeignKey(d => d.CompanyId)
               .HasConstraintName("FK_Edu_A_BuildingInfo_Comp_A_CompanyInfo");
      });

      modelBuilder.Entity<FEduBClassOrHallRoom>(entity =>
      {
         entity.HasKey(e => e.ClassRoomId).HasName("PK_Edu_B_ClassRoomInfo");

         entity.ToTable("F_Edu_B_ClassOrHallRoom", "dbo");

         entity.Property(e => e.ClassRoomId)
               .ValueGeneratedNever()
               .HasColumnName("ClassRoomID");
         entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
         entity.Property(e => e.ClassRoomName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassRoomNameLocal).HasMaxLength(150);

         entity.HasOne(d => d.Building).WithMany(p => p.FEduBClassOrHallRooms)
               .HasForeignKey(d => d.BuildingId)
               .HasConstraintName("FK_Edu_B_ClassOrHallRoomInfo_Edu_A_BuildingInfo");
      });

      modelBuilder.Entity<FEduCClassOrHall>(entity =>
      {
         entity.HasKey(e => e.HallSeatId).HasName("PK_Edu_C_ClassOrHallSeatInfo");

         entity.ToTable("F_Edu_C_ClassOrHall", "dbo");

         entity.Property(e => e.HallSeatId)
               .ValueGeneratedNever()
               .HasColumnName("HallSeatID");
         entity.Property(e => e.ClassRoomId).HasColumnName("ClassRoomID");

         entity.HasOne(d => d.ClassRoom).WithMany(p => p.FEduCClassOrHalls)
               .HasForeignKey(d => d.ClassRoomId)
               .HasConstraintName("FK_Edu_C_ClassOrHallSeatInfo_Edu_B_ClassOrHallRoomInfo");
      });

      modelBuilder.Entity<FEduDStudentAllocateHallSeat>(entity =>
      {
         entity.HasKey(e => e.AllocateStudentHallSeatId).HasName("PK_Edu_D_StudentAllocateHallSeatInfo");

         entity.ToTable("F_Edu_D_StudentAllocateHallSeat", "dbo");

         entity.Property(e => e.AllocateStudentHallSeatId)
               .ValueGeneratedNever()
               .HasColumnName("AllocateStudentHallSeatID");
         entity.Property(e => e.ClassRoomId).HasColumnName("ClassRoomID");
         entity.Property(e => e.HallSeatId).HasColumnName("HallSeatID");
         entity.Property(e => e.StudentId).HasColumnName("StudentID");

         entity.HasOne(d => d.ClassRoom).WithMany(p => p.FEduDStudentAllocateHallSeats)
               .HasForeignKey(d => d.ClassRoomId)
               .HasConstraintName("FK_Edu_D_StudentAllocateHallSeatInfo_Edu_B_ClassOrHallRoomInfo");

         entity.HasOne(d => d.HallSeat).WithMany(p => p.FEduDStudentAllocateHallSeats)
               .HasForeignKey(d => d.HallSeatId)
               .HasConstraintName("FK_Edu_D_StudentAllocateHallSeatInfo_Edu_D_StudentAllocateHallSeatInfo");
      });

      modelBuilder.Entity<FEduETransportArea>(entity =>
      {
         entity.HasKey(e => e.TransportAreaId).HasName("PK_Edu_E_TransportAreaInfo");

         entity.ToTable("F_Edu_E_TransportArea", "dbo");

         entity.Property(e => e.TransportAreaId)
               .ValueGeneratedNever()
               .HasColumnName("TransportAreaID");
         entity.Property(e => e.TransportAreaName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.TransportAreaNameLocal).HasMaxLength(150);
      });

      modelBuilder.Entity<FEduExamAGradePoint>(entity =>
      {
         entity.HasKey(e => e.GradePointId);

         entity.ToTable("F_Edu_Exam_A_GradePoint", "dbo");

         entity.Property(e => e.GradePointId).ValueGeneratedNever();
         entity.Property(e => e.Note)
             .HasMaxLength(500)
             .IsUnicode(false);
      });

      modelBuilder.Entity<FEduExamBExamShortCode>(entity =>
      {
         entity.HasKey(e => e.ExamShortCodeId).HasName("PK_F_Edu_Exam_B_ExamShortCode_ExamShortCodeId");

         entity.ToTable("F_Edu_Exam_B_ExamShortCode", "dbo");

         entity.Property(e => e.ExamShortCodeId).ValueGeneratedNever();
         entity.Property(e => e.ExamShortCode)
             .IsRequired()
             .HasMaxLength(10)
             .IsUnicode(false);
      });

      modelBuilder.Entity<FEduFTransportCharge>(entity =>
      {
         entity.HasKey(e => e.TransportChargeId).HasName("PK_Edu_F_TransportChargeInfo");

         entity.ToTable("F_Edu_F_TransportCharge", "dbo");

         entity.Property(e => e.TransportChargeId)
               .ValueGeneratedNever()
               .HasColumnName("TransportChargeID");
         entity.Property(e => e.TransportAreaId).HasColumnName("TransportAreaID");
         entity.Property(e => e.TransportId).HasColumnName("TransportID");

         entity.HasOne(d => d.TransportArea).WithMany(p => p.FEduFTransportCharges)
               .HasForeignKey(d => d.TransportAreaId)
               .HasConstraintName("FK_Edu_F_TransportChargeInfo_Edu_E_TransportAreaInfo");

         entity.HasOne(d => d.Transport).WithMany(p => p.FEduFTransportCharges)
               .HasForeignKey(d => d.TransportId)
               .HasConstraintName("FK_Edu_F_TransportChargeInfo_Comp_D_TransportInfo");
      });

      modelBuilder.Entity<FEduGLinkTransportArea>(entity =>
      {
         entity.HasKey(e => e.LinkAreaTransportId).HasName("PK_Edu_G_LinkTransportAreaInfo");

         entity.ToTable("F_Edu_G_LinkTransportArea", "dbo");

         entity.Property(e => e.LinkAreaTransportId)
               .ValueGeneratedNever()
               .HasColumnName("LinkAreaTransportID");
         entity.Property(e => e.TransportAreaId).HasColumnName("TransportAreaID");
         entity.Property(e => e.TransportId).HasColumnName("TransportID");

         entity.HasOne(d => d.TransportArea).WithMany(p => p.FEduGLinkTransportAreas)
               .HasForeignKey(d => d.TransportAreaId)
               .HasConstraintName("FK_Edu_G_LinkTransportAreaInfo_Edu_E_TransportAreaInfo");

         entity.HasOne(d => d.Transport).WithMany(p => p.FEduGLinkTransportAreas)
               .HasForeignKey(d => d.TransportId)
               .HasConstraintName("FK_Edu_G_LinkTransportAreaInfo_Comp_D_TransportInfo");
      });

      modelBuilder.Entity<FEduHStudentAllocateTransport>(entity =>
      {
         entity.HasKey(e => e.AllocateTransportId).HasName("PK_Edu_H_StudentAllocateTransport");

         entity.ToTable("F_Edu_H_StudentAllocateTransport", "dbo");

         entity.Property(e => e.AllocateTransportId)
               .ValueGeneratedNever()
               .HasColumnName("AllocateTransportID");
         entity.Property(e => e.StudentId).HasColumnName("StudentID");
         entity.Property(e => e.TransportAreaId).HasColumnName("TransportAreaID");
         entity.Property(e => e.TransportId).HasColumnName("TransportID");

         entity.HasOne(d => d.TransportArea).WithMany(p => p.FEduHStudentAllocateTransports)
               .HasForeignKey(d => d.TransportAreaId)
               .HasConstraintName("FK_Edu_H_StudentAllocateTransport_Edu_E_TransportAreaInfo");

         entity.HasOne(d => d.Transport).WithMany(p => p.FEduHStudentAllocateTransports)
               .HasForeignKey(d => d.TransportId)
               .HasConstraintName("FK_Edu_H_StudentAllocateTransport_Comp_D_TransportInfo");
      });

      modelBuilder.Entity<FEduJClassSection>(entity =>
      {
         entity.HasKey(e => e.ClassSectionId).HasName("PK_Edu_B_ClassSectionInfo");

         entity.ToTable("F_Edu_J_ClassSection", "dbo");

         entity.Property(e => e.ClassSectionId)
               .ValueGeneratedNever()
               .HasColumnName("ClassSectionID");
         entity.Property(e => e.ClassSectionName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassSectionNameLocal).HasMaxLength(150);
      });

      modelBuilder.Entity<FEduKClassGroup>(entity =>
      {
         entity.HasKey(e => e.ClassGroupId).HasName("PK_Edu_C_ClassGroupInfo");

         entity.ToTable("F_Edu_K_ClassGroup", "dbo");

         entity.Property(e => e.ClassGroupId)
               .ValueGeneratedNever()
               .HasColumnName("ClassGroupID");
         entity.Property(e => e.ClassGroupName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassGroupNameLocal).HasMaxLength(150);
      });

      modelBuilder.Entity<FEduLClassShift>(entity =>
      {
         entity.HasKey(e => e.ClassShiftId).HasName("PK_Edu_ClassShiftInfo");

         entity.ToTable("F_Edu_L_ClassShift", "dbo");

         entity.Property(e => e.ClassShiftId)
               .ValueGeneratedNever()
               .HasColumnName("ClassShiftID");
         entity.Property(e => e.ClassShiftName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassShiftNameLocal).HasMaxLength(150);
      });

      modelBuilder.Entity<FEduMClassSubject>(entity =>
      {
         entity.HasKey(e => e.ClassSubjectId).HasName("PK_Edu_D_ClassSubjectInfo");

         entity.ToTable("F_Edu_M_ClassSubject", "dbo");

         entity.Property(e => e.ClassSubjectId)
               .ValueGeneratedNever()
               .HasColumnName("ClassSubjectID");
         entity.Property(e => e.ClassSubjectCode)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassSubjectName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassSubjectNameLocal).HasMaxLength(150);
         entity.Property(e => e.ClassSubjectSl).HasColumnName("ClassSubjectSL");
      });

      modelBuilder.Entity<FEduNLinkClassGroup>(entity =>
      {
         entity.HasKey(e => e.LinkClassGroupId).HasName("PK_Edu_LinkClassGroupInfo");

         entity.ToTable("F_Edu_N_LinkClassGroup", "dbo");

         entity.Property(e => e.LinkClassGroupId)
               .ValueGeneratedNever()
               .HasColumnName("LinkClassGroupID");
         entity.Property(e => e.ClassGroupId).HasColumnName("ClassGroupID");
         entity.Property(e => e.ClassId).HasColumnName("ClassID");

         entity.HasOne(d => d.ClassGroup).WithMany(p => p.FEduNLinkClassGroups)
               .HasForeignKey(d => d.ClassGroupId)
               .HasConstraintName("FK_Edu_N_LinkClassGroupInfo_Edu_K_ClassGroupInfo");

         entity.HasOne(d => d.Class).WithMany(p => p.FEduNLinkClassGroups)
               .HasForeignKey(d => d.ClassId)
               .HasConstraintName("FK_Edu_N_LinkClassGroupInfo_Edu_I_ClassInfo");
      });

      modelBuilder.Entity<FEduOLinkClassSection>(entity =>
      {
         entity.HasKey(e => e.LinkClassSectionId).HasName("PK_Edu_F_LinkClassSectionInfo");

         entity.ToTable("F_Edu_O_LinkClassSection", "dbo");

         entity.Property(e => e.LinkClassSectionId)
               .ValueGeneratedNever()
               .HasColumnName("LinkClassSectionID");
         entity.Property(e => e.ClassId).HasColumnName("ClassID");
         entity.Property(e => e.ClassSectionId).HasColumnName("ClassSectionID");

         entity.HasOne(d => d.Class).WithMany(p => p.FEduOLinkClassSections)
               .HasForeignKey(d => d.ClassId)
               .HasConstraintName("FK_Edu_O_LinkClassSectionInfo_Edu_I_ClassInfo");

         entity.HasOne(d => d.ClassSection).WithMany(p => p.FEduOLinkClassSections)
               .HasForeignKey(d => d.ClassSectionId)
               .HasConstraintName("FK_Edu_O_LinkClassSectionInfo_Edu_J_ClassSectionInfo");
      });

      modelBuilder.Entity<FEduPLinkClassShift>(entity =>
      {
         entity.HasKey(e => e.LinkClassShiftId).HasName("PK_Edu_G_LinkClassShiftInfo");

         entity.ToTable("F_Edu_P_LinkClassShift", "dbo");

         entity.Property(e => e.LinkClassShiftId)
               .ValueGeneratedNever()
               .HasColumnName("LinkClassShiftID");
         entity.Property(e => e.ClassId).HasColumnName("ClassID");
         entity.Property(e => e.ClassShiftId).HasColumnName("ClassShiftID");

         entity.HasOne(d => d.Class).WithMany(p => p.FEduPLinkClassShifts)
               .HasForeignKey(d => d.ClassId)
               .HasConstraintName("FK_Edu_P_LinkClassShiftInfo_Edu_I_ClassInfo");

         entity.HasOne(d => d.ClassShift).WithMany(p => p.FEduPLinkClassShifts)
               .HasForeignKey(d => d.ClassShiftId)
               .HasConstraintName("FK_Edu_P_LinkClassShiftInfo_Edu_L_ClassShiftInfo");
      });

      modelBuilder.Entity<FEduQLinkClassSubject>(entity =>
      {
         entity.HasKey(e => e.LinkClassSubjectId).HasName("PK_Edu_H_LinkClassSubjectInfo");

         entity.ToTable("F_Edu_Q_LinkClassSubject", "dbo");

         entity.Property(e => e.LinkClassSubjectId)
               .ValueGeneratedNever()
               .HasColumnName("LinkClassSubjectID");
         entity.Property(e => e.ClassGroupId).HasColumnName("ClassGroupID");
         entity.Property(e => e.ClassId).HasColumnName("ClassID");
         entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

         entity.HasOne(d => d.ClassGroup).WithMany(p => p.FEduQLinkClassSubjects)
               .HasForeignKey(d => d.ClassGroupId)
               .HasConstraintName("FK_Edu_Q_LinkClassSubjectInfo_Edu_K_ClassGroupInfo");

         entity.HasOne(d => d.Class).WithMany(p => p.FEduQLinkClassSubjects)
               .HasForeignKey(d => d.ClassId)
               .HasConstraintName("FK_Edu_Q_LinkClassSubjectInfo_Edu_I_ClassInfo");

         entity.HasOne(d => d.ClassSubject).WithMany(p => p.FEduQLinkClassSubjects)
               .HasForeignKey(d => d.ClassSubjectId)
               .HasConstraintName("FK_Edu_Q_LinkClassSubjectInfo_Edu_M_ClassSubjectInfo");
      });

      modelBuilder.Entity<FEduRExam>(entity =>
      {
         entity.HasKey(e => e.ExamId).HasName("PK_Edu_R_ClassExamInfo");

         entity.ToTable("F_Edu_R_Exam", "dbo");

         entity.Property(e => e.ExamId)
               .ValueGeneratedNever()
               .HasColumnName("ExamID");
         entity.Property(e => e.ExamName)
               .HasMaxLength(150)
               .IsUnicode(false);
         entity.Property(e => e.ExamNameLocal).HasMaxLength(150);
         entity.Property(e => e.ExamSl).HasColumnName("ExamSL");
         entity.Property(e => e.ExamType)
               .HasMaxLength(50)
               .IsUnicode(false);
      });

      modelBuilder.Entity<FEduSAcademicYear>(entity =>
      {
         entity.HasKey(e => e.AcademicYearId).HasName("PK_Edu_S_AcademicYearSessionInfo");

         entity.ToTable("F_Edu_S_AcademicYear", "dbo");

         entity.Property(e => e.AcademicYearId)
               .ValueGeneratedNever()
               .HasColumnName("AcademicYearID");
         entity.Property(e => e.AcademicYear)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.AcademicYearInLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<FEduTAcademicSession>(entity =>
      {
         entity.HasKey(e => e.AcademicSessionId).HasName("PK_Edu_T_AcadeicSessionInfo");

         entity.ToTable("F_Edu_T_AcademicSession", "dbo");

         entity.Property(e => e.AcademicSessionId)
               .ValueGeneratedNever()
               .HasColumnName("AcademicSessionID");
         entity.Property(e => e.AcademicSession)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.AcademicSessionInLocal).HasMaxLength(50);
      });

      modelBuilder.Entity<FEduUClassPeriod>(entity =>
      {
         entity.HasKey(e => e.ClassPeriodId).HasName("PK_Edu_T_ClassPeriodInfo");

         entity.ToTable("F_Edu_U_ClassPeriod", "dbo");

         entity.Property(e => e.ClassPeriodId)
               .ValueGeneratedNever()
               .HasColumnName("ClassPeriodID");
         entity.Property(e => e.ClassPeriodName)
               .HasMaxLength(50)
               .IsUnicode(false);
         entity.Property(e => e.ClassPeriodNameInLocal).HasMaxLength(50);
         entity.Property(e => e.ClassPeriodSl).HasColumnName("ClassPeriodSL");
      });

      OnModelCreatingPartial(modelBuilder);
   }

   partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
