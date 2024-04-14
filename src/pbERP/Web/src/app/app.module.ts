
import { CommonModule, NgIf } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountsModule } from './accounts/accounts.module';
import { LayoutModule } from './layout/layout.module';
import { HomeModule } from './home/home.module';
// import { EducationModule } from './education/education.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SecurityModule } from './security/security.module';
import { GeneralConfigurationModule } from './general-configuration/general-configuration.module';
import { MaterialModule } from './material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { EducationModule } from './education/education.module';


@NgModule({
  declarations: [
    AppComponent,
   ],
  imports: [    
    NgIf,
    MaterialModule,
    CommonModule,
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    HttpClientModule,

    AccountsModule,
    LayoutModule,
    HomeModule,
    EducationModule,
    SecurityModule,
    GeneralConfigurationModule
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
