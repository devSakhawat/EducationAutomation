import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GeneralConfigurationComponent } from './general-configuration.component';
import { ACountryComponent } from './a-country/a-country.component';
import { ModuleComponent } from './module/module.component';
import { BDivisionOrStateComponent } from './b-division-or-state/b-division-or-state.component';
import { CDistrictOrCityComponent } from './c-district-or-city/c-district-or-city.component';
import { DPoliceStation } from './d-police-station/d-police-station.component';
import { EGenderComponent } from './e-gender/e-gender.component';
import { FBloodGroupComponent } from './f-blood-group/f-blood-Group.component';
import { GReligionComponent } from './g-religion/g-religion.component';

const routes: Routes = [
  { path : '', component: GeneralConfigurationComponent },
  { path : 'country', component: ACountryComponent },
  { path : 'division-state', component: BDivisionOrStateComponent},
  { path : 'district-city', component: CDistrictOrCityComponent},
  { path : 'police-station', component: DPoliceStation},
  { path : 'gender', component: EGenderComponent},
  { path : 'blood-group', component: FBloodGroupComponent},
  { path : 'religion', component: GReligionComponent},
  { path : 'module', component: ModuleComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GeneralConfigurationRoutingModule { }
