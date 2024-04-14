using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.AGeneralConfig;
using pbERP.Infrastructure.Constracts.BSecurity;
using pbERP.Infrastructure.Constracts.CCompany;
using pbERP.Infrastructure.Constracts.DHR;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Constracts;

public interface IUnitOfWork
{
  Task<int> SaveChangesAsync();

  #region A GeneralConfiguration
  // A GeneralConfiguration
  IAGenConfigACountryRepository Country { get; }
  IAGenConfigBDivisionOrStateRepository Division { get; }
  IAGenConfigCDistrictOrCityRepository District { get; }
  IAGenConfigDPoliceStationRepository PS { get; }

  IAGenConfigIModuleRepository Module { get; }
  IAGenConfigFBloodGroupRepository BloodGroup { get; }
  IAGenConfigEGenderRepository Gender { get; }
  IAGenConfigGReligionRepository Religion { get; }
  #endregion A GeneralConfiguration

  #region C Company
  ICCompACompanyRepository ACompany { get; }
  ICCompDTransportRepository DTransport { get; }
  #endregion C Company

  #region F Education
  // F Education
  IFEduAClassRepository AClass { get; }
  IFEduAStudentRepository EduAStudent { get; }
  IFEduBBuildingRepository EduBBuilding { get; }
  IFEduBClassOrHallRoomRepository ClassOrHallRoom { get; }
  IFEduCClassOrHallRepository ClassOrHall { get; }
  IFEduDStudentAllocateHallSeatRepository StudentSeat { get; }
  IFEduETransportAreaRepository TransportArea { get; }
  IFEduFTransportChargeRepository TransportCharge { get; }
  IFEduGLinkTransportAreaRepository LinkTransportArea { get; }
  IFEduHStudentAllocateTransportRepository AllocateTransport { get; }
  IFEduJClassSectionRepository ClassSection { get; }
  IFEduKClassGroupRepository ClassGroup { get; }
  IFEduNLinkClassGroupRepository LinkClassGroup { get; }
  IFEduOLinkClassSectionRepository LinkClassSection { get; }
  IFEduPLinkClassShiftRepository LinkClassShift { get; }
  IFEduQLinkClassSubjectRepository LinkClassSubject { get; }
  IFEduLClassShiftRepository ClassShift { get; }
  IFEduMClassSubjectRepository ClassSubject { get; }
  IFEduRExamRepository Exam { get; }
  IFEduSAcademicYearRepository AcademicYear { get; }
  IFEduTAcademicSessionRepository AcademicSession { get; }
  IFEduUClassPeriodRepository ClassPeriod { get; }
  IFEduExamBExamShortCodeRepository ExamShortCode { get; }
  IFEduExamAGradePointRepository ExamAGradePoint { get; }
  #endregion F Education

  #region HR
  // DHR ReferenceType
  IDHrKReferenceTypeRepository DHrKReferenceType { get; }
  IDHrLPresentAddressRepository DHrLPresentAddress { get; }
  IDHrMPermanentAddressRepository DHrMPermanentAddress { get; }
  #endregion HR

  #region menu and Security
  IMainMenuRepository MainMenu { get; }
  IBSecAUserGroupRepository UserGroup { get; }
  IBSecBUserRepository User { get; }
  IBSecELinkUserGroupScreenRepository LinkUserGroupScreen { get; }
  IBSecDScreenRepository Screen { get; }
  #endregion menu and Security



}
