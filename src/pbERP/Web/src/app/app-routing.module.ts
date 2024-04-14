import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home/home.component';
import { AccountComponent } from './accounts/account.component';
import { InventoryComponent } from './inventory/inventory.component';
import { ProductionComponent } from './production/production.component';
import { RealEstateComponent } from './real-estate/real-estate.component';
import { ConstructionComponent } from './construction/construction.component';
import { TailoringComponent } from './tailoring/tailoring.component';
import { HouseRentComponent } from './house-rent/house-rent.component';
import { ParcelManagementComponent } from './parcel-management/parcel-management.component';
import { DyeingComponent } from './dyeing/dyeing.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { PimComponent } from './pim/pim.component';
import { GeneralComponent } from './general/general.component';

const routes: Routes = [
{ path : '', component : HomeComponent },
{ path : 'accounts', component : AccountComponent },
{ path : 'inventory', component : InventoryComponent },
{ path : 'production', component : ProductionComponent },
{ path : 'real-estate', component : RealEstateComponent },
{ path : 'construction', component : ConstructionComponent },
{ path : 'PIM', component : PimComponent },
{ path : 'hr&payroll', loadChildren:() => import('./hr-payroll/hr-payroll.module').then(m => m.HrPayrollModule) },
{ path : 'education', loadChildren: () => import('./education/education.module').then(m => m.EducationModule) },
{ path : 'security', loadChildren: () => import('./security/security.module').then(m => m.SecurityModule)},
{ path : 'general', loadChildren: () => import('./general-configuration/general-configuration.module').then(m => m.GeneralConfigurationModule)},

// { path: 'lazy-feature', loadChildren: () => import('./my-feature-module/my-feature-module.module').then(m => m.MyFeatureModuleModule) },
{ path : 'tailoring', component : TailoringComponent },
{ path : 'house-rent', component : HouseRentComponent },
{ path : 'parcel-management', component : ParcelManagementComponent },
{ path : 'dyeing', component : DyeingComponent },

// error
{ path : 'not-found', component : NotFoundComponent},
{ path : 'server-error', component : ServerErrorComponent},
{ path : 'test-error', component : TestErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
