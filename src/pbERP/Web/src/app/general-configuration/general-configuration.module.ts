import { NgModule } from '@angular/core';
import { CommonModule, NgFor, NgIf } from '@angular/common';

import { GeneralConfigurationRoutingModule } from './general-configuration-routing.module';
import { ModuleComponent } from './module/module.component';
import { ACountryComponent } from './a-country/a-country.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { GeneralConfigurationComponent } from './general-configuration.component';
import { BDivisionOrStateComponent } from './b-division-or-state/b-division-or-state.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CDistrictOrCityComponent } from './c-district-or-city/c-district-or-city.component';
import { DPoliceStation } from './d-police-station/d-police-station.component';
import { EGenderComponent } from './e-gender/e-gender.component';
import { FBloodGroupComponent } from './f-blood-group/f-blood-Group.component';
import { GReligionComponent } from './g-religion/g-religion.component';

@NgModule({
  declarations: [
    ModuleComponent,
    ACountryComponent,
    GeneralConfigurationComponent,
    BDivisionOrStateComponent,
    CDistrictOrCityComponent,
    DPoliceStation,
    EGenderComponent,
    FBloodGroupComponent,
    GReligionComponent
  ],
  imports: [
    GeneralConfigurationRoutingModule,
    NgIf, NgFor,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    MatProgressSpinnerModule,

    MatFormFieldModule,
    MatSnackBarModule,
    MatDialogModule,
    MatInputModule,
    MatIconModule,

    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatButtonModule,
    MatAutocompleteModule,
  ]
})
export class GeneralConfigurationModule { }
