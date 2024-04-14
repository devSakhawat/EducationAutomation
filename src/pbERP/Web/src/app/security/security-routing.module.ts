import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SecurityComponent } from './security.component';
import { SecAUserGroupComponent } from './sec-a-user-group/sec-a-user-group.component';
import { SecBUserComponent } from './sec-b-user/sec-b-user.component';
import { SecDScreenComponent } from './sec-d-screen/sec-d-screen.component';
import { SecELinkUserGroupScreenComponent } from './sec-e-link-user-group-screen/sec-e-link-user-group-screen.component';

const routes : Routes =[
  { path : '', component:  SecurityComponent },
  { path: 'user-group', component: SecAUserGroupComponent },
  { path: 'user', component: SecBUserComponent },
  { path: 'screen', component: SecDScreenComponent },
  { path: 'user-group-screen', component: SecELinkUserGroupScreenComponent },
  { path: '', redirectTo: 'security', pathMatch: 'full' }
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
export class SecurityRoutingModule { }
