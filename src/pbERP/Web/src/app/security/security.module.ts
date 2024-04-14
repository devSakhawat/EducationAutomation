import { NgModule } from '@angular/core';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { SecurityComponent } from './security.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { SecurityRoutingModule } from './security-routing.module';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SecAUserGroupComponent } from './sec-a-user-group/sec-a-user-group.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { SecBUserComponent } from './sec-b-user/sec-b-user.component';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { SecDScreenComponent } from './sec-d-screen/sec-d-screen.component';
import { SecELinkUserGroupScreenComponent } from './sec-e-link-user-group-screen/sec-e-link-user-group-screen.component';



@NgModule({
  declarations: [
    SecurityComponent,
    SecAUserGroupComponent,
    SecBUserComponent,
    SecDScreenComponent,
    SecELinkUserGroupScreenComponent
  ],
  imports: [
    CommonModule,
    SecurityRoutingModule,
    NgIf, NgFor,
    FormsModule,

    MatSnackBarModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatSortModule,
    MatPaginatorModule,
    MatIconModule,
    MatTooltipModule,
    MatDialogModule,
    MatButtonModule,
    MatAutocompleteModule,
    MatInputModule,
    MatNativeDateModule,
    MatDatepickerModule,

    ReactiveFormsModule
  ]
})
export class SecurityModule { }
