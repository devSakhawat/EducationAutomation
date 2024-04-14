import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HrPayrollComponent } from './hr-payroll.component';
import { HrHbloodGroupComponent } from './hr-hblood-group/hr-hblood-group.component';

const routes : Routes =[
  { path : '', component:  HrPayrollComponent },
  { path: 'blood-group', component: HrHbloodGroupComponent }
  
]



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class HrPayrollRoutingModule { }
