import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HrPayrollComponent } from './hr-payroll.component';
import { HrJReligionComponent } from './hr-jreligion/hr-jreligion.component';
import { HrHbloodGroupComponent } from './hr-hblood-group/hr-hblood-group.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { HrPayrollRoutingModule } from './hr-payroll-routing.module';




@NgModule({
  declarations: [
    HrPayrollComponent,
    HrJReligionComponent,
    HrHbloodGroupComponent,


  ],
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    HrPayrollRoutingModule
  ]
})
export class HrPayrollModule { }
