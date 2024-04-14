import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

import { AStudentComponent } from "./a-student/a-student.component";
import { EducationComponent } from "./education.component";
import { BClassOrHallRoomComponent } from "./b-class-or-hall-room/b-class-or-hall-room.component";
import { CClassOrHallComponent } from "./c-class-or-hall/c-class-or-hall.component";
import { DStudentAllocateHallSeatComponent } from "./d-student-allocate-hall-seat/d-student-allocate-hall-seat.component";
import { ETransportAreaComponent } from "./e-transport-area/e-transport-area.component";
import { FTransportChargeComponent } from "./f-transport-charge/f-transport-charge.component";
import { JClassSectionComponent } from "./j-class-section/j-class-section.component";
import { KClassGroupComponent } from "./k-class-group/k-class-group.component";
import { LClassShiftComponent } from "./l-class-shift/l-class-shift.component";
import { MClassSubjectComponent } from "./m-class-subject/m-class-subject.component";
import { UClassPeriodComponent } from "./u-class-period/u-class-period.component";
import { RExamComponent } from "./r-exam/r-exam.component";
import { SAcademicYearComponent } from "./s-academic-year/s-academic-year.component";
import { TAcademicSessionComponent } from "./t-academic-session/t-academic-session.component";
import { AClassComponent } from "./a-class/a-class.component";
import { BBuildingComponent } from "./b-building/b-building.component";
import { GLinkTransportAreaComponent } from "./g-link-transport-area/g-link-transport-area.component";
import { NLinkClassGroupComponent } from "./n-link-class-group/n-link-class-group.component";
import { OLinkClassSectionComponent } from "./o-link-class-section/o-link-class-section.component";
import { PLinkClassShiftComponent } from "./p-link-class-shift/p-link-class-shift.component";
import { QLinkClassSubjectComponent } from "./q-link-class-subject/q-link-class-subject.component";
import { ExamBShortCodeComponent } from "./exam-b-short-code/exam-b-short-code.component";


const routes: Routes = [
  { path: '', component: EducationComponent},
  { path: 'class', component: AClassComponent},
  { path: 'student', component: AStudentComponent},
  { path: 'building', component: BBuildingComponent},
  { path: 'class-hall-seat', component: CClassOrHallComponent},
  { path: 'class-hall-room', component: BClassOrHallRoomComponent},
  { path: 'student-allowcate-hall-seat', component: DStudentAllocateHallSeatComponent},
  { path: 'transport-area', component: ETransportAreaComponent},
  { path: 'transport-charge', component: FTransportChargeComponent},
  { path: 'link-transport-area', component: GLinkTransportAreaComponent},
  { path: 'class-section', component: JClassSectionComponent},
  { path: 'class-group', component: KClassGroupComponent},
  { path: 'class-shift', component: LClassShiftComponent},
  { path: 'class-subject', component: MClassSubjectComponent},
  { path: 'link-class-group', component: NLinkClassGroupComponent},
  { path: 'link-class-section', component: OLinkClassSectionComponent},
  { path: 'link-class-shift', component: PLinkClassShiftComponent},
  { path: 'link-class-subject', component: QLinkClassSubjectComponent},
  { path: 'exam', component: RExamComponent},
  { path: 'academic-year', component: SAcademicYearComponent},
  { path: 'academic-session', component: TAcademicSessionComponent},
  { path: 'class-period', component: UClassPeriodComponent },
  { path: 'exam-short-code', component: ExamBShortCodeComponent },
  { path: '', redirectTo: 'education', pathMatch: 'full' }
]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class EducationRoutingModule { }
