import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatBadgeModule } from "@angular/material/badge";
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatGridListModule } from "@angular/material/grid-list"



let meterialModules = [
  MatButtonModule, MatInputModule, MatToolbarModule, 
  MatIconModule, MatMenuModule, MatExpansionModule, 
  MatDividerModule, MatTooltipModule, MatSidenavModule, 
  MatListModule, MatRippleModule, MatListModule,
  MatBadgeModule, MatAutocompleteModule, MatDialogModule,
  MatSlideToggleModule, MatSnackBarModule, MatCardModule,
  MatFormFieldModule, MatProgressSpinnerModule, MatTableModule,
  MatSortModule, MatPaginatorModule, MatGridListModule
  
  
   
]
@NgModule({
  declarations: [],
  imports: [ meterialModules ],
  exports: [meterialModules]
})
export class MaterialModule { }
