namespace pbERP.Utilities.Constant
{
  public static class RouteConstant
  {
    public const string BaseRoute = "pberp";


    #region Menu
    // Company base Module (MainMenu)
    public const string MainMenu = "layout/modules";
    // Module base Screen (NestedMenu)
    public const string SubMenu = "layout/screens";
    #endregion Menu

    #region ErrorUrl
    public const string ErrorsByCode = "/errors/{code}";
    #endregion

    #region AGeneralConfig Module

    #region AGenConfigACountry
    public const string CreateAGenConfigACountry = "gen-config/country";
    public const string ReadAGenConfigACountries = "gen-config/countries";
    public const string ReadAGenConfigACountryByKey = "gen-config/country/key/{key}";
    public const string UpdateAGenConfigACountry = "gen-config/country/{key}";
    public const string DeleteAGenConfigACountry = "gen-config/country/{key}";
    #endregion AGenConfigACountry

    #region AGenConfigBDivisionOrState
    public const string CreateAGenConfigBDivisionOrState = "gen-config/division-state";
    public const string ReadAGenConfigBDivisionOrStates = "gen-config/division-states";
    public const string ReadAGenConfigBDivisionOrStateByKey = "gen-config/division-state/key/{key}";
    public const string UpdateAGenConfigBDivisionOrState = "gen-config/division-state/{key}";
    public const string DeleteAGenConfigBDivisionOrState = "gen-config/division-state/{key}";
    #endregion AGenConfigACountry

    #region AGenConfigCDistrictOrCity
    public const string CreateAGenConfigCDistrictOrCity = "gen-config/district-city";
    public const string ReadAGenConfigCDistrictOrCities = "gen-config/districts-cities";
    public const string ReadAGenConfigCDistrictOrCity = "gen-config/district-city/key/{key}";
    public const string UpdateAGenConfigCDistrictOrCity = "gen-config/district-city/{key}";
    public const string DeleteAGenConfigCDistrictOrCity = "gen-config/district-city/{key}";
    #endregion AGenConfigCDistrictOrCity

    #region AGenConfigDPoliceStation
    public const string CreateAGenConfigDPoliceStation = "gen-config/police-station";
    public const string ReadAGenConfigDPoliceStations = "gen-config/police-stations";
    public const string ReadAGenConfigDPoliceStation = "gen-config/police-station/key/{key}";
    public const string UpdateAGenConfigDPoliceStation = "gen-config/police-station/{Key}";
    public const string DeleteAGenConfigDPoliceStation = "gen-config/police-station/{key}";
    #endregion AGenConfigDPoliceStation

    #region AGenConfigEGender
    public const string CreateAGenConfigEGender = "gen-config/gender";
    public const string ReadAGenConfigEGenders = "gen-config/genders";
    public const string ReadAGenConfigEGenderByKey = "gen-config/gender/key/{key}";
    public const string UpdateAGenConfigEGender = "gen-config/gender/{key}";
    public const string DeleteAGenConfigEGender = "gen-config/gender/{key}";
    #endregion AGenConfigEGender

    #region FBloodGroup
    public const string CreateAGenConfigFBloodGroup = "gen-config/blood-group";
    public const string ReadAGenConfigFBloodGroups = "gen-config/blood-groups";
    public const string ReadAGenConfigFBloodGroupByKey = "gen-config/blood-group/key/{key}";
    public const string UpdateAGenConfigFBloodGroup = "gen-config/blood-group/{key}";
    public const string DeleteAGenConfigFBloodGroup = "gen-config/blood-group/{key}";
    #endregion FBloodGroup

    #region AGenConfigGReligion
    public const string CreateAGenConfigGReligion = "gen-config/religion";
    public const string ReadAGenConfigGReligions = "gen-config/religions";
    public const string ReadAGenConfigGReligionByKey = "gen-config/religion/key/{key}";
    public const string UpdateAGenConfigGReligion = "gen-config/religion/{key}";
    public const string DeleteAGenConfigGReligion = "gen-config/religion/{key}";
    #endregion AGenConfigGReligion

    #endregion AGeneralConfig Module

    #region HumanResource

    #region DHrKReferenceType
    public const string CreateDHrKReferenceType = "hr/reference-type";
    public const string ReadDHrKReferenceTypes = "hr/reference-types";
    public const string ReadDHrKReferenceTypeByKey = "hr/reference-type/key/{key}";
    public const string UpdateDHrKReferenceType = "hr/reference-type/{key}";
    public const string DeleteDHrKReferenceType = "hr/reference-type/{key}";
    #endregion DHrKReferenceType

    #endregion HumanResource

    #region Company Module
    #region CCompACompany
    public const string CreateCCompACompany = "company/company";
    public const string ReadCCompACompanys = "company/companies";
    public const string ReadCCompACompanyByKey = "company/company/key/{key}";
    public const string UpdateCCompACompany = "company/company/{key}";
    public const string DeleteCCompACompany = "company/company/{key}";
    #endregion CCompACompany

    #region CCompDTransport
    public const string CreateCCompDTransport = "company/transport";
    public const string ReadCCompDTransports = "company/transports";
    public const string ReadCCompDTransportByKey = "company/transport/key/{key}";
    public const string UpdateCCompDTransport = "company/transport/{key}";
    public const string DeleteCCompDTransport = "company/transport/{key}";
    #endregion CCompDTransport
    #endregion Company Module

    #region Education Module

    #region EduAStudent
    public const string CreateEduAStudent = "education/student";
    public const string ReadEduAStudents = "education/students";
    public const string ReadEduAStudentByKey = "education/student/key/{key}";
    public const string UpdateEduAStudent = "education/student/{key}";
    public const string DeleteEduAStudent = "education/student/{key}";
    #endregion EduAStudent

    #region FEduAClass
    public const string CreateFEduAClass = "education/class";
    public const string ReadFEduAClasss = "education/classes";
    public const string ReadFEduAClassByKey = "education/class/key/{key}";
    public const string UpdateFEduAClass = "education/class/{key}";
    public const string DeleteFEduAClass = "education/class/{key}";
    #endregion FEduAClass

    #region EduBBuilding
    public const string CreateEduBBuilding = "education/building";
    public const string ReadEduBBuildings = "education/buildings";
    public const string ReadEduBBuildingByKey = "education/building/key/{key}";
    public const string UpdateEduBBuilding = "education/building/{key}";
    public const string DeleteEduBBuilding = "education/building/{key}";
    #endregion EduBBuilding

    #region EduBClassOrHallRoom
    public const string CreateEduBClassOrHallRoom = "education/class-hallroom";
    public const string ReadEduBClassOrHallRooms = "education/class-hallrooms";
    public const string ReadEduBClassOrHallRoomByKey = "education/class-hallroom/key/{key}";
    public const string UpdateEduBClassOrHallRoom = "education/class-hallroom/{key}";
    public const string DeleteEduBClassOrHallRoom = "education/class-hallroom/{key}";
    #endregion EduBClassOrHallRoom

    #region FEduCClassOrHall
    public const string CreateEduCClassOrHall = "education/class-hall";
    public const string ReadEduCClassOrHalls = "education/class-halls";
    public const string ReadEduCClassOrHallByKey = "education/class-hall/key/{key}";
    public const string UpdateEduCClassOrHall = "education/class-hall/{key}";
    public const string DeleteEduCClassOrHall = "education/class-hall/{key}";
    #endregion FEduCClassOrHall

    #region FEduDStudentAllocateHallSeat
    public const string CreateFEduDStudentAllocateHallSeat = "education/hall-seat";
    public const string ReadFEduDStudentAllocateHallSeats = "education/hall-seats";
    public const string ReadFEduDStudentAllocateHallSeatByKey = "education/hall-seat/key/{key}";
    public const string UpdateFEduDStudentAllocateHallSeat = "education/hall-seat/{key}";
    public const string DeleteFEduDStudentAllocateHallSeat = "education/hall-seat/{key}";
    #endregion FEduDStudentAllocateHallSeat

    #region FEduETransportArea
    public const string CreateFEduETransportArea = "education/transport-area";
    public const string ReadFEduETransportAreas = "education/transport-areas";
    public const string ReadFEduETransportAreaByKey = "education/transport-area/key/{key}";
    public const string UpdateFEduETransportArea = "education/transport-area/{key}";
    public const string DeleteFEduETransportArea = "education/transport-area/{key}";
    #endregion FEduETransportArea

    #region FEduFTransportCharge
    public const string CreateFEduFTransportCharge = "education/transport-charge";
    public const string ReadFEduFTransportCharges = "education/transport-charges";
    public const string ReadFEduFTransportChargeByKey = "education/transport-charge/key/{key}";
    public const string UpdateFEduFTransportCharge = "education/transport-charge/{key}";
    public const string DeleteFEduFTransportCharge = "education/transport-charge/{key}";
    #endregion FEduFTransportCharge

    #region FEduGLinkTransportArea
    public const string CreateFEduGLinkTransportArea = "education/link-transport-area";
    public const string ReadFEduGLinkTransportAreas = "education/link-transport-areas";
    public const string ReadFEduGLinkTransportAreaByKey = "education/link-transport-area/key/{key}";
    public const string UpdateFEduGLinkTransportArea = "education/link-transport-area/{key}";
    public const string DeleteFEduGLinkTransportArea = "education/link-transport-area/{key}";
    #endregion FEduGLinkTransportArea

    #region FEduGLinkTransportArea
    public const string CreateFEduHStudentAllocateTransport = "education/allocate-transport";
    public const string ReadFEduHStudentAllocateTransports = "education/allocate-transports";
    public const string ReadFEduHStudentAllocateTransportByKey = "education/allocate-transport/key/{key}";
    public const string UpdateFEduHStudentAllocateTransport = "education/allocate-transport/{key}";
    public const string DeleteFEduHStudentAllocateTransport = "education/allocate-transport/{key}";
    #endregion FEduGLinkTransportArea

    #region FEduJClassSection
    public const string CreateFEduJClassSection = "education/class-section";
    public const string ReadFEduJClassSections = "education/class-sections";
    public const string ReadFEduJClassSectionByKey = "education/class-section/key/{key}";
    public const string UpdateFEduJClassSection = "education/class-section/{key}";
    public const string DeleteFEduJClassSection = "education/class-section/{key}";
    #endregion FEduJClassSection

    #region FEduKClassGroup
    public const string CreateFEduKClassGroup = "education/class-group";
    public const string ReadFEduKClassGroups = "education/class-groups";
    public const string ReadFEduKClassGroupByKey = "education/class-group/key/{key}";
    public const string UpdateFEduKClassGroup = "education/class-group/{key}";
    public const string DeleteFEduKClassGroup = "education/class-group/{key}";
    #endregion FEduKClassGroup

    #region FEduLClassShift
    public const string CreateFEduLClassShift = "education/class-shift";
    public const string ReadFEduLClassShifts = "education/class-shifts";
    public const string ReadFEduLClassShiftByKey = "education/class-shift/key/{key}";
    public const string UpdateFEduLClassShift = "education/class-shift/{key}";
    public const string DeleteFEduLClassShift = "education/class-shift/{key}";
    #endregion FEduLClassShift

    #region FEduMClassSubject
    public const string CreateFEduMClassSubject = "education/class-subject";
    public const string ReadFEduMClassSubjects = "education/class-subjects";
    public const string ReadFEduMClassSubjectByKey = "education/class-subject/key/{key}";
    public const string UpdateFEduMClassSubject = "education/class-subject/{key}";
    public const string DeleteFEduMClassSubject = "education/class-subject/{key}";
    #endregion FEduMClassSubject

    #region FEduNLinkClassGroup
    public const string CreateFEduNLinkClassGroup = "education/link-class-group";
    public const string ReadFEduNLinkClassGroups = "education/link-class-groups";

    #endregion FEduNLinkClassGroup

    #region FEduOLinkClassSection
    public const string CreateFEduOLinkClassSection = "education/link-class-section";
    public const string ReadFEduOLinkClassSectiones = "education/link-class-sections";
    #endregion FEduOLinkClassSection

    #region FEduOLinkClassSection
    public const string CreateFEduPLinkClassShift = "education/link-class-shift";
    public const string ReadFEduPLinkClassShifts = "education/link-class-shifts";
    #endregion FEduOLinkClassSection

    #region FEduQLinkClassSubject
    public const string CreateFEduQLinkClassSubject = "education/link-class-subject";
    public const string ReadFEduQLinkClassSubjects = "education/link-class-subjects";
    #endregion FEduQLinkClassSubject

    #region FEduRExam
    public const string CreateFEduRExam = "education/exam";
    public const string ReadFEduRExams = "education/exams";
    public const string ReadFEduRExamByKey = "education/exam/key/{key}";
    public const string UpdateFEduRExam = "education/exam/{key}";
    public const string DeleteFEduRExam = "education/exam/{key}";
    #endregion FEduRExam

    #region FEduSAcademicYear
    public const string CreateFEduSAcademicYear = "education/academic-year";
    public const string ReadFEduSAcademicYears = "education/academic-years";
    public const string ReadFEduSAcademicYearByKey = "education/academic-year/key/{key}";
    public const string UpdateFEduSAcademicYear = "education/academic-year/{key}";
    public const string DeleteFEduSAcademicYear = "education/academic-year/{key}";
    #endregion FEduSAcademicYear

    #region FEduTAcademicSession
    public const string CreateFEduTAcademicSession = "education/academic-session";
    public const string ReadFEduTAcademicSessions = "education/academic-sessions";
    public const string ReadFEduTAcademicSessionByKey = "education/academic-session/key/{key}";
    public const string UpdateFEduTAcademicSession = "education/academic-session/{key}";
    public const string DeleteFEduTAcademicSession = "education/academic-session/{key}";
    #endregion FEduTAcademicSession

    #region FEduUClassPeriod
    public const string CreateFEduUClassPeriod = "education/class-period";
    public const string ReadFEduUClassPeriods = "education/class-periods";
    public const string ReadFEduUClassPeriodByKey = "education/class-period/key/{key}";
    public const string UpdateFEduUClassPeriod = "education/class-period/{key}";
    public const string DeleteFEduUClassPeriod = "education/class-period/{key}";
    #endregion FEduUClassPeriod

    #region FEduExamBExamShortCode
    public const string CreateFEduExamBExamShortCode = "education/exam-short-code";
    public const string ReadFEduExamBExamShortCodes = "education/exam-short-codes";
    public const string ReadFEduExamBExamShortCodeByKey = "education/exam-short-code/key/{key}";
    public const string UpdateFEduExamBExamShortCode = "education/exam-short-code/{key}";
    public const string DeleteFEduExamBExamShortCode = "education/exam-short-code/{key}";
    #endregion FEduExamBExamShortCode

    #region FEduExamAGradePoint
    public const string CreateFEduExamAGradePoint = "grade-point";
    public const string ReadFEduExamAGradePoints = "grade-points";
    public const string ReadFEduExamAGradePointByKey = "grade-point/key/{key}";
    public const string UpdateFEduExamAGradePoint = "grade-point/{key}";
    public const string DeleteFEduExamAGradePoint = "grade-point/{key}";
    #endregion FEduExamAGradePoint

    //#region EduABuilding
    //public const string CreateEduBBuilding = "eduB-building";
    //public const string ReadEduBBuilding = "eduB-buildings";
    //public const string ReadEduBBuildingWithoutParams = "eduB-buildings-without-params";
    //public const string ReadEduBBuildingEdu = "eduB-buildings-edu";
    //public const string ReadEduBBuildingByKey = "eduB-building/key/{key}";
    //public const string UpdateEduBBuilding = "eduB-building/{key}";
    //public const string DeleteEduBBuilding = "eduB-building/{key}";

    //public const string ReadComACompanies = "comA-Companies";
    //#endregion EduABuilding

    #endregion

    #region AccAFinancial
    public const string CreateAccAFinancialYear = "financial-year";
    public const string ReadAccAFinancialYears = "financial-years";
    public const string ReadAccAFinancialYearByKey = "financial-year/key/{key}";
    public const string UpdateAccAFinancialYear = "financial-year/{key}";
    public const string DeleteAccAFinancialYear = "financial-year/{key}";
    #endregion AccAFinancial

    #region AccBChartOfAccount
    public const string CreateAccBChartOfAccount = "chart-of-account";
    public const string ReadAccBChartOfAccounts = "chart-of-accounts";
    public const string ReadAccBChartOfAccountByKey = "chart-of-accounts/key/{key}";
    public const string UpdateAccBChartOfAccount = "chart-of-account/{key}";
    public const string DeleteAccBChartOfAccount = "chart-of-account/{key}";
    #endregion AccBChartOfAccount

    #region AccCJournalMaster
    public const string CreateAccCJournalMaster = "journal-master";
    public const string ReadAccCJournalMasters = "journal-masters";
    public const string ReadAccCJournalMasterByKey = "journal-master/key/{key}";
    public const string UpdateAccCJournalMaster = "journal-master/{key}";
    public const string DeleteAccCJournalMaster = "journal-master/{key}";
    #endregion AccCJournalMaster

    #region AccDJournalDetail
    public const string CreateAccDJournalDetail = "journal-detail";
    public const string ReadAccDJournalDetails = "journal-details";
    public const string ReadAccDJournalDetailByKey = "journal-detail/key/{key}";
    public const string UpdateAccDJournalDetail = "journal-detail/{key}";
    public const string DeleteAccDJournalDetail = "journal-detail/{key}";
    #endregion AccDJournalDetail

    #region CompACompanyInfo
    public const string CreateCompACompanyInfo = "company";
    public const string ReadCompACompanyInfos = "companies";
    public const string ReadCompACompanyInfoByKey = "company/key/{key}";
    public const string UpdateCompACompanyInfo = "company/{key}";
    public const string DeleteCompACompanyInfo = "company/{key}";
    #endregion CompACompanyInfo

    #region HR Module



    #endregion HR Module

    #region SoftConfigJCompany

    public const string ReadCompanyBaseModule = "company-modules";
    #endregion SoftConfigJCompany

    #region GeneralConfiguration

    #region AGenConfigIModule
    public const string CreateAGenConfigIModule = "gen/module";
    public const string ReadAGenConfigIModules = "gen/modules";
    public const string ReadAGenConfigIModuleByKey = "gen/module/key/{key}";
    public const string UpdateAGenConfigIModule = "gen/module/{key}";
    public const string DeleteAGenConfigIModule = "gen/module/{key}";
    #endregion AGenConfigIModule

    #endregion GeneralConfiguration 

    #region Security Module

    #region SecAUserGroup
    public const string CreateSecAUserGroup = "sec/user-group";
    public const string ReadSecAUserGroups = "sec/user-groups";
    public const string ReadSecAUserGroupByKey = "sec/user-group/key/{key}";
    public const string UpdateSecAUserGroup = "sec/user-group/{key}";
    public const string DeleteSecAUserGroup = "sec/user-group/{key}";
    #endregion SecAUserGroup

    #region SecBUser
    public const string CreateSecBUser = "sec/user";
    public const string ReadSecBUsers = "sec/users";
    public const string ReadSecBUserByKey = "sec/user/key/{key}";
    public const string UpdateSecBUser = "sec/user/{key}";
    public const string DeleteSecBUser = "sec/user/{key}";
    #endregion SecBUser

    #region BSecELinkUserGroupScreen
    public const string CreateSecELinkUserGroupScreen = "sec/link-user-group-screen";
    public const string BatchInsertSecELinkUserGroupScreen = "sec/batch-link-user-group-screen";
    public const string ReadSecELinkUserGroupScreens = "sec/link-user-group-screens";
    public const string ReadSecELinkUserGroupScreenByKey = "sec/link-user-group-screen/key/{key}";
    public const string UpdateSecELinkUserGroupScreen = "sec/link-user-group-screen/{key}";
    public const string DeleteSecELinkUserGroupScreen = "sec/link-user-group-screen/{key}";
    #endregion BSecELinkUserGroupScreen

    #region BSecDScreen
    public const string CreateBSecDScreen = "sec/screen";
    public const string ReadBSecDScreens = "sec/screens";
    public const string ReadBSecDScreenByKey = "sec/screen/key/{key}";
    public const string UpdateBSecDScreen = "sec/screen/{key}";
    public const string DeleteBSecDScreen = "sec/screen/{key}";
    #endregion BSecDScreen

    #endregion HR Module



  }
}
