import { NgModule } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { AccountsModule } from '../accounts/accounts.module';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
import {MatListModule} from '@angular/material/list';
import {MatExpansionModule} from '@angular/material/expansion';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { TestErrorComponent } from './test-error/test-error.component';





@NgModule({
  declarations: [

  
    NotFoundComponent,
         ServerErrorComponent,
         TestErrorComponent
  ],
  imports: [
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    AccountsModule,
    CommonModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatListModule,
    BrowserAnimationsModule,MatExpansionModule,
    NgIf
  ],
  exports:[

  ]
})
export class CoreModule { }
