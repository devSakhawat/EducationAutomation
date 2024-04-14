
using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Constracts.AGeneralConfig;
using pbERP.Infrastructure.Constracts.BSecurity;
using pbERP.Infrastructure.Constracts.CCompany;
using pbERP.Infrastructure.Constracts.DHR;
using pbERP.Infrastructure.Constracts.FEducation;
using pbERP.Infrastructure.Repositories.AGeneralConfig;
using pbERP.Infrastructure.Repositories.BSecurity;
using pbERP.Infrastructure.Repositories.CCompany;
using pbERP.Infrastructure.Repositories.Education;
using pbERP.Infrastructure.Repositories.FEducation;

namespace pbERP.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly pbERPContext context;

  public UnitOfWork(pbERPContext context)
  {
    this.context = context;
    MainMenu = new MainMenuRepository(context);

    #region A General Configuration
    // A GeneralConfiguration
    Country = new AGenConfigACountryRepository(context);
    Division = new AGenConfigBDivisionOrStateRepository(context);
    District = new AGenConfigCDistrictOrCityRepository(context);
    PS = new AGenConfigDPoliceStationRepository(context);
    Gender = new AGenConfigEGenderRepository(context);
    BloodGroup = new AGenConfigFBloodGroupRepository(context);
    Religion = new AGenConfigGReligionRepository(context);
    Module = new AGenConfigIModuleRepository(context);
    #endregion A General Configuration

    #region C Company
    ACompany = new CCompACompanyRepository(context);
    DTransport = new CCompDTransportRepository(context);
    #endregion C Company

    #region F Education
    // F Education
    AClass = new FEduAClassRepository(context);
    EduAStudent = new FEduAStudentRepository(context);
    EduBBuilding = new FEduBBuildingRepository(context);
    ClassOrHallRoom = new FEduBClassOrHallRoomRepository(context);
    ClassOrHall = new FEduCClassOrHallRepository(context);
    StudentSeat = new FEduDStudentAllocateHallSeatRepository(context);
    TransportArea = new FEduETransportAreaRepository(context);
    TransportCharge = new FEduFTransportChargeRepository(context);
    LinkTransportArea = new FEduGLinkTransportAreaRepository(context);
    AllocateTransport = new FEduHStudentAllocateTransportRepository(context);
    ClassSection = new FEduJClassSectionRepository(context);
    ClassGroup = new FEduKClassGroupRepository(context);
    ClassShift = new FEduLClassShiftRepository(context);
    LinkClassGroup = new FEduNLinkClassGroupRepository(context);
    LinkClassSection = new FEduOLinkClassSectionRepository(context);
    LinkClassShift = new FEduPLinkClassShiftRepository(context);
    LinkClassSubject = new FEduQLinkClassSubjectRepository(context);
    ClassSubject = new FEduMClassSubjectRepository(context);
    Exam = new FEduRExamRepository(context);
    AcademicYear = new FEduSAcademicYearRepository(context);
    AcademicSession = new FEduTAcademicSessionRepository(context);
    ClassPeriod = new FEduUClassPeriodRepository(context);
    ExamShortCode = new FEduExamBExamShortCodeRepository(context);
    ExamAGradePoint = new FEduExamAGradePointRepository(context);
    #endregion F Education

    #region D HR
    //D Human Resource
    DHrKReferenceType = new DHrKReferenceTypeRepository(context);
    DHrLPresentAddress = new DHrLPresentAddressRepository(context);
    DHrMPermanentAddress = new DHrMPermanentAddressRepository(context);

    UserGroup = new BSecAUserGroupRepository(context);
    User = new BSecBUserRepository(context);
    LinkUserGroupScreen = new BSecELinkUserGroupScreenRepository(context);
    Screen = new BSecDScreenRepository(context);
    #endregion

  }

  public async Task<int> SaveChangesAsync()
  {
    return await context.SaveChangesAsync();
  }

  public IMainMenuRepository MainMenu { get; private set; }

  #region General Configuration
  // A GeneralConfiguration
  public IAGenConfigACountryRepository Country { get; private set; }
  public IAGenConfigBDivisionOrStateRepository Division { get; private set; }
  public IAGenConfigCDistrictOrCityRepository District { get; private set; }
  public IAGenConfigDPoliceStationRepository PS { get; private set; }

  public IAGenConfigEGenderRepository Gender { get; private set; }
  public IAGenConfigFBloodGroupRepository BloodGroup { get; private set; }
  public IAGenConfigGReligionRepository Religion { get; private set; }
  public IAGenConfigIModuleRepository Module { get; private set; }
  #endregion General Configuration

  #region C Company
  public ICCompACompanyRepository ACompany { get; set; }
  public ICCompDTransportRepository DTransport { get; set; }
  #endregion C Company

  #region F Education
  // F Education
  public IFEduAClassRepository AClass { get; private set; }
  public IFEduAStudentRepository EduAStudent { get; private set; }
  public IFEduBBuildingRepository EduBBuilding { get; private set; }
  public IFEduBClassOrHallRoomRepository ClassOrHallRoom { get; private set; }
  public IFEduCClassOrHallRepository ClassOrHall { get; private set; }
  public IFEduDStudentAllocateHallSeatRepository StudentSeat { get; private set; }
  public IFEduETransportAreaRepository TransportArea { get; private set; }
  public IFEduFTransportChargeRepository TransportCharge { get; private set; }
  public IFEduGLinkTransportAreaRepository LinkTransportArea { get; private set; }
  public IFEduHStudentAllocateTransportRepository AllocateTransport { get; private set; }
  public IFEduJClassSectionRepository ClassSection { get; private set; }
  public IFEduKClassGroupRepository ClassGroup { get; private set; }
  public IFEduLClassShiftRepository ClassShift { get; private set; }
  public IFEduNLinkClassGroupRepository LinkClassGroup { get; private set; }
  public IFEduOLinkClassSectionRepository LinkClassSection { get; private set; }
  public IFEduPLinkClassShiftRepository LinkClassShift { get; private set; }
  public IFEduQLinkClassSubjectRepository LinkClassSubject { get; private set; }
  public IFEduMClassSubjectRepository ClassSubject { get; private set; }
  public IFEduRExamRepository Exam { get; private set; }
  public IFEduSAcademicYearRepository AcademicYear { get; private set; }
  public IFEduTAcademicSessionRepository AcademicSession { get; private set; }
  public IFEduUClassPeriodRepository ClassPeriod { get; private set; }
  public IFEduExamBExamShortCodeRepository ExamShortCode { get; private set; }
  public IFEduExamAGradePointRepository ExamAGradePoint { get; private set; }
  #endregion F Education

  #region D HR// D HumanResource
  public IDHrKReferenceTypeRepository DHrKReferenceType { get; private set; }
  public IDHrLPresentAddressRepository DHrLPresentAddress { get; private set; }
  public IDHrMPermanentAddressRepository DHrMPermanentAddress { get; private set; }
  #endregion D HR// D HumanResource

  #region Security
  public IBSecAUserGroupRepository UserGroup { get; private set; }
  public IBSecBUserRepository User { get; private set; }
  public IBSecELinkUserGroupScreenRepository LinkUserGroupScreen { get; private set; }
  public IBSecDScreenRepository Screen { get; private set; }
  #endregion Security


}
