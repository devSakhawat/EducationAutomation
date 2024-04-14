import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EduAStudent } from '../shared/models/Education/eduAStudent';
import { environment } from 'src/environments/environment';
import { merge, Observable, of as observableOf } from 'rxjs';
import { EduParams } from '../shared/models/eduParams';
import { BClassOrHallRoom } from '../shared/models/Education/BClassOrHallRoom';
import { BBuilding } from '../shared/models/Education/BBuilding';
import { CClassOrHall } from '../shared/models/Education/CClassOrHall';
import { DStudentAllocateHallSeat } from '../shared/models/Education/DStudentAllocateHallSeat';
import { ETransportArea } from '../shared/models/Education/ETransportArea';
import { JClassSection } from '../shared/models/Education/JClassSection';
import { KClassGroup } from '../shared/models/Education/KClassGroup';
import { LClassShift } from '../shared/models/Education/LClassShift';
import { MClassSubject } from '../shared/models/Education/MClassSubject';
import { UClassPeriod } from '../shared/models/Education/UClassPeriod';
import { RExam } from '../shared/models/Education/RExam';
import { SAcademicYear } from '../shared/models/Education/SAcademicYear';
import { TAcademicSession } from '../shared/models/Education/TAcademicSession';
import { AClass } from '../shared/models/Education/AClass';
import { FTransportCharge } from '../shared/models/Education/FTransportCharge';
import { GLinkTransportArea } from '../shared/models/Education/GLinkTransportArea';
import { NLinkClassGroup } from '../shared/models/Education/NLinkClassGroup';
import { OLinkClassSection } from '../shared/models/Education/OLinkClassSection';
import { PLinkClassShift } from '../shared/models/Education/PLinkClassShift';
import { QLinkClassSubject } from '../shared/models/Education/QLinkClassSubject';
import { ExamBShortCode } from '../shared/models/Education/ExamBShortCode';

@Injectable({
  providedIn: 'root'
})
export class EducationService {
  baseUrl = environment.apiUrl ;
  newUrl = environment.apiUrl + "education/";

  constructor(private http: HttpClient) { }
  //#region A Student Services
  insertStudent(student: FormData): Observable<EduAStudent> {
    return this.http.post<EduAStudent>(this.baseUrl + "education/student", student);
  }

  getStudents(): Observable<EduAStudent[]> {
    return this.http.get<EduAStudent[]>(this.baseUrl + 'education/students');
  }

  // Get By StudentId
  getByStudentId(studentId : number)
  {
    return this.http.get<EduAStudent>(this.baseUrl + `education/student/key/${studentId}`);
  }

  updateStudent(studentId: number, student: FormData): Observable<EduAStudent> {
    return this.http.put<EduAStudent>(`${this.baseUrl}education/student/${studentId}`, student);
  }

  // getStudentByStudentId(studentId: number){
  //   return this.http.get<EduAStudent>(this.baseUrl + `education/student/key/${studentId}`);
  // }
  // updateEduAStudent(student: EduAStudent): Observable<EduAStudent> {
  //   return this.http.put<EduAStudent>(`${this.baseUrl}education/student?key=${student.studentId}`, student);
  // }

  deleteStudent(studentId: number): Observable<EduAStudent> {
    return this.http.delete<EduAStudent>(`${this.baseUrl}education/student/${studentId}`);
  }
  //#endregion Student Services

  //#region AClass services
  // Insert AClass
  insertClass(aClass : AClass): Observable<AClass>{
    return this.http.post<AClass>(this.newUrl + "class", aClass);
  }
  // Get AClasses
  getClasses() : Observable<AClass[]>{
    return this.http.get<AClass[]>(this.newUrl + "classes");
  }

  // Get by AClassId
  getByClassId(classId : number){
    return this.http.get<AClass>(this.newUrl + `class/key/${classId}`);
  }

  // Update AClass
  updateClass(classId : number, aClass : AClass) : Observable<AClass>{
    return this.http.put<AClass>(this.newUrl + `class/${classId}`, aClass);
  }

  // Delete AClass
  deleteClass(classId : number) : Observable<AClass>{
    return this.http.delete<AClass>(this.newUrl + `class/${classId}`);
  }
  //#endregion AClass services

  //#region B Building Services
  insertBuilding(building: BBuilding): Observable<BBuilding> {
    return this.http.post<BBuilding>(this.baseUrl + "education/building", building);
  }

  getBuildings(): Observable<BBuilding[]> {
    return this.http.get<BBuilding[]>(this.baseUrl + 'education/buildings');
  }

  // Get By StudentId
  getByBuildingId(buildingId : number)
  {
    return this.http.get<BBuilding>(this.baseUrl + `education/building/key/${buildingId}`);
  }

  updateBuilding(buildingId: number, building: BBuilding): Observable<BBuilding> {
    
    return this.http.put<BBuilding>(`${this.baseUrl}education/building/${buildingId}`, building);
  }


  deleteBuilding(buildingId: number): Observable<BBuilding> {
    return this.http.delete<BBuilding>(`${this.baseUrl}education/building/${buildingId}`);
  }
  //#endregion Building Services

  //#region B Class or Hall Room Services
  insertClassOrHallRoom(classOrHallRoom: BClassOrHallRoom): Observable<BClassOrHallRoom> {
    
    return this.http.post<BClassOrHallRoom>(this.baseUrl + "education/class-hallroom", classOrHallRoom);
  }

  getClassOrHallRooms(): Observable<BClassOrHallRoom[]> {
    return this.http.get<BClassOrHallRoom[]>(this.baseUrl + 'education/class-hallrooms');
  }

  // Get By StudentId
  getByClassOrHallRoomId(classOrHallRoomId : number)
  {
    return this.http.get<BClassOrHallRoom>(this.baseUrl + `education/class-hallroom/key/${classOrHallRoomId}`);
  }

  updateClassOrHallRoom(classOrHallRoomId: number, classOrHallRoom: BClassOrHallRoom): Observable<BClassOrHallRoom> {
    
    return this.http.put<BClassOrHallRoom>(`${this.baseUrl}education/class-hallroom/${classOrHallRoomId}`, classOrHallRoom);
  }


  deleteClassOrHallRoom(classOrHallRoomId: number): Observable<BClassOrHallRoom> {
    return this.http.delete<BClassOrHallRoom>(`${this.baseUrl}education/class-hallroom/${classOrHallRoomId}`);
  }
  //#endregion Class or Hall Room Services

  //#region C Class Or Hall Services
  insertClassOrHall(classOrHall: CClassOrHall): Observable<CClassOrHall> {
    
    return this.http.post<CClassOrHall>(this.baseUrl + "education/class-hall", classOrHall);
  }

  getClassOrHalls(): Observable<CClassOrHall[]> {
    return this.http.get<CClassOrHall[]>(this.baseUrl + 'education/class-halls');
  }

  // Get By StudentId
  getByClassOrHallId(hallSeatId : number)
  {
    return this.http.get<CClassOrHall>(this.baseUrl + `education/class-hall/key/${hallSeatId}`);
  }

  updateClassOrHall(hallSeatId: number, classOrHall: CClassOrHall): Observable<CClassOrHall> {
    
    return this.http.put<CClassOrHall>(`${this.baseUrl}education/class-hall/${hallSeatId}`, classOrHall);
  }


  deleteClassOrHall(hallSeatId: number): Observable<CClassOrHall> {
    return this.http.delete<CClassOrHall>(`${this.baseUrl}education/class-hall/${hallSeatId}`);
  }
  //#endregion Class or Hall Room Services

  //#region D Student allocate hall seat Services
  // insert StudentAllocateHallSeat
  insertStudentAllocateHallSeat(studentAllocateHallSeat: DStudentAllocateHallSeat): Observable<DStudentAllocateHallSeat> {
    
    return this.http.post<DStudentAllocateHallSeat>(this.baseUrl + "education/hall-seat", studentAllocateHallSeat);
  }

  // Get By allocateStudentHallSeatId
  getStudentAllocateHallSeats(): Observable<DStudentAllocateHallSeat[]> {
    return this.http.get<DStudentAllocateHallSeat[]>(this.baseUrl + 'education/hall-seats');
  }

  // Get By StudentId
  getByStudentAllocateHallSeatId(allocateStudentHallSeatId : number)
  {
    return this.http.get<DStudentAllocateHallSeat>(this.baseUrl + `education/hall-seat/key/${allocateStudentHallSeatId}`);
  }

    // Update StudentAllocateHallSeat
  updateStudentAllocateHallSeat(allocateStudentHallSeatId: number, studentAllocateHallSeat: DStudentAllocateHallSeat): Observable<DStudentAllocateHallSeat> {
    
    return this.http.put<DStudentAllocateHallSeat>(`${this.baseUrl}education/hall-seat/${allocateStudentHallSeatId}`, studentAllocateHallSeat);
  }

  // Delete StudentAllocateHallSeat
  deleteStudentAllocateHallSeat(allocateStudentHallSeatId: number): Observable<DStudentAllocateHallSeat> {
    return this.http.delete<DStudentAllocateHallSeat>(`${this.baseUrl}education/hall-seat/${allocateStudentHallSeatId}`);
  }
  //#endregion D Student allocate hall seat Services

  //#region E Transport Area Services
  // insert ETransportArea
  insertTransportArea(transportArea: ETransportArea): Observable<ETransportArea> {
    
    return this.http.post<ETransportArea>(this.baseUrl + "education/transport-area", transportArea);
  }

  // Get By transportAreaId
  getTransportAreas(): Observable<ETransportArea[]> {
    return this.http.get<ETransportArea[]>(this.baseUrl + 'education/transport-areas');
  }

  // Get By StudentId
  getByTransportAreaId(transportAreaId : number)
  {
    return this.http.get<ETransportArea>(this.baseUrl + `education/transport-area/key/${transportAreaId}`);
  }

    // Update ETransportArea
  updateTransportArea(transportAreaId: number, transportArea: ETransportArea): Observable<ETransportArea> {
    
    return this.http.put<ETransportArea>(`${this.baseUrl}education/transport-area/${transportAreaId}`, transportArea);
  }

  // Delete ETransportArea
  deleteTransportArea(transportAreaId: number): Observable<ETransportArea> {
    return this.http.delete<ETransportArea>(`${this.baseUrl}education/transport-area/${transportAreaId}`);
  }
  //#endregion E Transport Area Services

  //#region FTransportCharge services
  // insert FTransportCharge
  insertTransportCharge(transportCharge: FTransportCharge): Observable<FTransportCharge> {
    
    return this.http.post<FTransportCharge>(this.baseUrl + "education/transport-charge", transportCharge);
  }
  // Get By FTransportChargees
  getTransportCharges(): Observable<FTransportCharge[]> {
    return this.http.get<FTransportCharge[]>(this.baseUrl + 'education/transport-charges');
  }
  // Get By FTransportChargeeId
  getByTransportChargeId(transportChargeId : number)
  {
    return this.http.get<FTransportCharge>(this.baseUrl + `education/transport-charge/key/${transportChargeId}`);
  }
  // Update FTransportCharge
  updateTransportCharge(transportChargeId: number, transportCharge: FTransportCharge): Observable<FTransportCharge> 
  {
    debugger;  
    return this.http.put<FTransportCharge>(`${this.baseUrl}education/transport-charge/${transportChargeId}`, transportCharge);
  }
  // Delete FTransportCharge
  deleteTransportCharge(transportChargeId: number): Observable<FTransportCharge> {
    return this.http.delete<FTransportCharge>(`${this.baseUrl}education/transport-charge/${transportChargeId}`);
  }
  //#endregion  FTransportCharge services

  //#region J Class Section
  // insert JClassSection
  insertClassSection(classSection: JClassSection): Observable<JClassSection> {
    
    return this.http.post<JClassSection>(this.baseUrl + "education/class-section", classSection);
  }

  // Get By classSectionId
  getClassSections(): Observable<JClassSection[]> {
    return this.http.get<JClassSection[]>(this.baseUrl + 'education/class-sections');
  }

  // Get By StudentId
  getByClassSectionId(classSectionId : number)
  {
    return this.http.get<JClassSection>(this.baseUrl + `education/class-section/key/${classSectionId}`);
  }

    // Update JClassSection
  updateClassSection(classSectionId: number, classSection: JClassSection): Observable<JClassSection> {
    
    return this.http.put<JClassSection>(`${this.baseUrl}education/class-section/${classSectionId}`, classSection);
  }

  // Delete JClassSection
  deleteClassSection(classSectionId: number): Observable<JClassSection> {
    return this.http.delete<JClassSection>(`${this.baseUrl}education/class-section/${classSectionId}`);
  }
  //#endregion E Transport Area Services

  //#region K Class Group services
  // insert KClassGroup
  insertClassGroup(classGroup: KClassGroup): Observable<KClassGroup> {
    
    return this.http.post<KClassGroup>(this.baseUrl + "education/class-group", classGroup);
  }

  // Get By classGroupId
  getClassGroups(): Observable<KClassGroup[]> {
    return this.http.get<KClassGroup[]>(this.baseUrl + 'education/class-groups');
  }

  // Get By StudentId
  getByClassGroupId(classGroupId : number)
  {
    return this.http.get<KClassGroup>(this.baseUrl + `education/class-group/key/${classGroupId}`);
  }

    // Update KClassGroup
  updateClassGroup(classGroupId: number, classGroup: KClassGroup): Observable<KClassGroup> {
    
    return this.http.put<KClassGroup>(`${this.baseUrl}education/class-group/${classGroupId}`, classGroup);
  }

  // Delete KClassGroup
  deleteClassGroup(classGroupId: number): Observable<KClassGroup> {
    return this.http.delete<KClassGroup>(`${this.baseUrl}education/class-group/${classGroupId}`);
  }
  //#endregion K Class Group services

  //#region L Class Shift services
  // insert LClassShift
  insertClassShift(classSubject: LClassShift): Observable<LClassShift> {
    
    return this.http.post<LClassShift>(this.baseUrl + "education/class-shift", classSubject);
  }

  // Get By classShiftId
  getClassShifts(): Observable<LClassShift[]> {
    return this.http.get<LClassShift[]>(this.baseUrl + 'education/class-shifts');
  }

  // Get By StudentId
  getByClassShiftId(classShiftId : number)
  {
    return this.http.get<LClassShift>(this.baseUrl + `education/class-shift/key/${classShiftId}`);
  }

    // Update LClassShift
  updateClassShift(classShiftId: number, classShift: LClassShift): Observable<LClassShift> {
    
    return this.http.put<LClassShift>(`${this.baseUrl}education/class-shift/${classShiftId}`, classShift);
  }

  // Delete LClassShift
  deleteClassShift(classShiftId: number): Observable<LClassShift> {
    return this.http.delete<LClassShift>(`${this.baseUrl}education/class-shift/${classShiftId}`);
  }
  //#endregion K Class Group services

  //#region M ClassSubject services
  // insert MClassSubject
  insertClassSubject(classSubject: MClassSubject): Observable<MClassSubject> {
    
    return this.http.post<MClassSubject>(this.baseUrl + "education/class-subject", classSubject);
  }

  // Get By classSubjectId
  getClassSubjects(): Observable<MClassSubject[]> {
    return this.http.get<MClassSubject[]>(this.baseUrl + 'education/class-subjects');
  }

  // Get By StudentId
  getByClassSubjectId(classSubjectId : number)
  {
    return this.http.get<MClassSubject>(this.baseUrl + `education/class-subject/key/${classSubjectId}`);
  }

    // Update MClassSubject
  updateClassSubject(classSubjectId: number, classSubject: MClassSubject): Observable<MClassSubject> {
    
    return this.http.put<MClassSubject>(`${this.baseUrl}education/class-subject/${classSubjectId}`, classSubject);
  }

  // Delete MClassSubject
  deleteClassSubject(classSubjectId: number): Observable<MClassSubject> {
    return this.http.delete<MClassSubject>(`${this.baseUrl}education/class-subject/${classSubjectId}`);
  }
  //#endregion  M ClassSubject services

  //#region UClassPeriod services
  // insert UClassPeriod
  insertClassPeriod(classPeriod: UClassPeriod): Observable<UClassPeriod> {
    
    return this.http.post<UClassPeriod>(this.baseUrl + "education/class-period", classPeriod);
  }

  // Get By classPeriodId
  getClassPeriods(): Observable<UClassPeriod[]> {
    return this.http.get<UClassPeriod[]>(this.baseUrl + 'education/class-periods');
  }

  // Get By StudentId
  getByClassPeriodId(classPeriodId : number)
  {
    return this.http.get<UClassPeriod>(this.baseUrl + `education/class-period/key/${classPeriodId}`);
  }

    // Update UClassPeriod
  updateClassPeriod(classPeriodId: number, classPeriod: UClassPeriod): Observable<UClassPeriod> {
    
    return this.http.put<UClassPeriod>(`${this.baseUrl}education/class-period/${classPeriodId}`, classPeriod);
  }

  // Delete UClassPeriod
  deleteClassPeriod(classPeriodId: number): Observable<UClassPeriod> {
    return this.http.delete<UClassPeriod>(`${this.baseUrl}education/class-period/${classPeriodId}`);
  }
  //#endregion  M UClassPeriod services

  //#region RExam services
  // insert RExam
  insertExam(exam: RExam): Observable<RExam> {
    
    return this.http.post<RExam>(this.baseUrl + "education/exam", exam);
  }

  // Get By examId
  getExams(): Observable<RExam[]> {
    return this.http.get<RExam[]>(this.baseUrl + 'education/exams');
  }

  // Get By StudentId
  getByExamId(examId : number)
  {
    return this.http.get<RExam>(this.baseUrl + `education/exam/key/${examId}`);
  }

    // Update RExam
  updateExam(examId: number, exam: RExam): Observable<RExam> {
    
    return this.http.put<RExam>(`${this.baseUrl}education/exam/${examId}`, exam);
  }

  // Delete RExam
  deleteExam(examId: number): Observable<RExam> {
    return this.http.delete<RExam>(`${this.baseUrl}education/exam/${examId}`);
  }
  //#endregion  RExam services

  //#region SAcademicYear services
  // insert SAcademicYear
  insertAcademicYear(academicYear: SAcademicYear): Observable<SAcademicYear> {
    
    return this.http.post<SAcademicYear>(this.baseUrl + "education/academic-year", academicYear);
  }

  // Get By academicYearId
  getAcademicYears(): Observable<SAcademicYear[]> {
    return this.http.get<SAcademicYear[]>(this.baseUrl + 'education/academic-years');
  }

  // Get By StudentId
  getByAcademicYearId(academicYearId : number)
  {
    return this.http.get<SAcademicYear>(this.baseUrl + `education/academic-year/key/${academicYearId}`);
  }

    // Update SAcademicYear
  updateAcademicYear(academicYearId: number, academicYear: SAcademicYear): Observable<SAcademicYear> {
    
    return this.http.put<SAcademicYear>(`${this.baseUrl}education/academic-year/${academicYearId}`, academicYear);
  }

  // Delete SAcademicYear
  deleteAcademicYear(academicYearId: number): Observable<SAcademicYear> {
    return this.http.delete<SAcademicYear>(`${this.baseUrl}education/academic-year/${academicYearId}`);
  }
  //#endregion  SAcademicYear services


  //#region TAcademicSession services
  // insert TAcademicSession
  insertAcademicSession(academicSession: TAcademicSession): Observable<TAcademicSession> {
    
    return this.http.post<TAcademicSession>(this.baseUrl + "education/academic-session", academicSession);
  }
  // Get By TAcademicSessiones
  getAcademicSessions(): Observable<TAcademicSession[]> {
    return this.http.get<TAcademicSession[]>(this.baseUrl + 'education/academic-sessions');
  }
  // Get By TAcademicSessioneId
  getByAcademicSessionId(academicSessionId : number)
  {
    return this.http.get<TAcademicSession>(this.baseUrl + `education/academic-session/key/${academicSessionId}`);
  }
  // Update TAcademicSession
  updateAcademicSession(academicSessionId: number, academicSession: TAcademicSession): Observable<TAcademicSession> {
    
    return this.http.put<TAcademicSession>(`${this.baseUrl}education/academic-session/${academicSessionId}`, academicSession);
  }
  // Delete TAcademicSession
  deleteAcademicSession(academicSessionId: number): Observable<TAcademicSession> {
    return this.http.delete<TAcademicSession>(`${this.baseUrl}education/academic-session/${academicSessionId}`);
  }
  //#endregion  TAcademicSession services



  //#region GLinkTransportArea services
  // insert GLinkTransportArea
  insertLinkTransportArea(linkTransportArea: GLinkTransportArea[]): Observable<GLinkTransportArea[]> 
  {
    debugger;  
    return this.http.post<GLinkTransportArea[]>(this.baseUrl + "education/link-transport-area", linkTransportArea);
  }
  // Get By GLinkTransportAreas
  getLinkTransportAreas(): Observable<GLinkTransportArea[]> {
    return this.http.get<GLinkTransportArea[]>(this.baseUrl + 'education/link-transport-areas');
  }
  //#endregion  GLinkTransportArea services


  //#region NLinkClassGroup services
  // insert NLinkClassGroup
  insertLinkClassGroup(linkClassGroup: NLinkClassGroup[]): Observable<NLinkClassGroup[]> 
  {
    debugger;  
    return this.http.post<NLinkClassGroup[]>(this.baseUrl + "education/link-class-group", linkClassGroup);
  }
  // Get By NLinkClassGroups
  getLinkClassGroups(): Observable<NLinkClassGroup[]> 
  {
    return this.http.get<NLinkClassGroup[]>(this.baseUrl + 'education/link-class-groups');
  }
  //#endregion  NLinkClassGroup services
  


  //#region OLinkClassSection services
  // insert OLinkClassSection
  insertLinkClassSection(linkClassSection: OLinkClassSection[]): Observable<OLinkClassSection[]> 
  {
    debugger;  
    return this.http.post<OLinkClassSection[]>(this.baseUrl + "education/link-class-section", linkClassSection);
  }
  // Get By OLinkClassSection
  getLinkClassSections(): Observable<OLinkClassSection[]>
  {
    return this.http.get<OLinkClassSection[]>(this.baseUrl + 'education/link-class-sections');
  }
  //#endregion  OLinkClassSection services

  
  //#region PLinkClassShift
  insertLinkClassShift(linkClassShift : PLinkClassShift[]) : Observable<PLinkClassShift[]>
  {
    debugger;
    return this.http.post<PLinkClassShift[]>(this.baseUrl + 'education/link-class-shift', linkClassShift);
  }

  getLinkClassShifts() : Observable<PLinkClassShift[]>
  {
    return this.http.get<PLinkClassShift[]>(this.baseUrl + 'education/link-class-shifts');
  }
  //#endregion PLinkClassShift

  
  //#region QLinkClassSubject
  insertLinkClassSubject(linkClassSubject: QLinkClassSubject[]) : Observable<QLinkClassSubject[]>
  {
    return this.http.post<QLinkClassSubject[]>(this.baseUrl + 'education/link-class-subject', linkClassSubject);
  }

  getLinkClassSubjects() : Observable<QLinkClassSubject[]>
  {
    return this.http.get<QLinkClassSubject[]>(this.baseUrl + 'education/link-class-subjects');
  }
  //#endregion QLinkClassSubject

  
  //#region ExamBShortCode services
  // insert ExamBShortCode
  insertExamShortCode(examShortCode: ExamBShortCode): Observable<ExamBShortCode> {
    
    return this.http.post<ExamBShortCode>(this.baseUrl + "education/exam-short-code", examShortCode);
  }
  // Get By ExamBExamShortCodees
  getExamShortCodes(): Observable<ExamBShortCode[]> {
    return this.http.get<ExamBShortCode[]>(this.baseUrl + 'education/exam-short-codes');
  }
  // Get By ExamBExamShortCodeeId
  getByExamShortCodeId(examShortCodeId : number)
  {
    return this.http.get<ExamBShortCode>(this.baseUrl + `education/exam-short-code/key/${examShortCodeId}`);
  }
  // Update ExamBShortCode
  updateExamShortCode(examShortCodeId: number, examShortCode: ExamBShortCode): Observable<ExamBShortCode> {
    debugger;
    return this.http.put<ExamBShortCode>(`${this.baseUrl}education/exam-short-code/${examShortCodeId}`, examShortCode);
  }
  // Delete ExamBShortCode
  deleteExamShortCode(examShortCodeId: number): Observable<ExamBShortCode> {
    return this.http.delete<ExamBShortCode>(`${this.baseUrl}education/exam-short-code/${examShortCodeId}`);
  }
  //#endregion  ExamBShortCode services


  

}
