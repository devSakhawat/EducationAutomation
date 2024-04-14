import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs/internal/Subscription';
import { DTransport } from 'src/app/shared/models/Company/DTransport';
import { ETransportArea } from 'src/app/shared/models/Education/ETransportArea';
import { GLinkTransportArea } from 'src/app/shared/models/Education/GLinkTransportArea';
import { EducationService } from '../education.service';
import { CompanyService } from 'src/app/company/company.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RepositionScrollStrategy } from '@angular/cdk/overlay';

@Component({
  selector: 'app-g-link-transport-area',
  templateUrl: './g-link-transport-area.component.html',
  styleUrls: ['./g-link-transport-area.component.css']
})

export class GLinkTransportAreaComponent implements OnInit {
  linkTransportAreas: GLinkTransportArea[] = [];
  linkTransportAreaForm!: FormGroup;
  transports: DTransport[] = [];
  transportAreas: ETransportArea[] = [];
  isLoadingResults = true;
  isRateLimitReached = false;
  areasArray!: FormArray;
  private linkTransportAreaSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', { static: true }) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', { static: true }) deleteDialog!: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private services: EducationService,
    private companyServices: CompanyService,
    private dialog: MatDialog,
    private snackbar: MatSnackBar
  ) {
    this.initForm();
  }

  // ngOnInit() {
  //   this.onLoad();
  //   this.companyServices.getTransports().subscribe(
  //     (response: DTransport[]) => {
  //       this.transports = response;
  //     }
  //   );
  //   this.services.getTransportAreas().subscribe(
  //     (response: ETransportArea[]) => {
  //       this.transportAreas = response;
  //     }
  //   );    
  // }

  ngOnInit() {
    this.onLoad();
    // Fetch transports first
    this.companyServices.getTransports().subscribe(
      (response: DTransport[]) => {
        this.transports = response;  
        // After fetching transports, fetch transport areas
        this.services.getTransportAreas().subscribe(
          (response: ETransportArea[]) => {
            this.transportAreas = response; 
          }
        );
      }
    );
  
    // You can place other code that doesn't depend on the data here
  }

  ngOnDestroy(){
    if(this.linkTransportAreaSubscription) this.linkTransportAreaSubscription.unsubscribe();
  }


  onLoad() {
    this.linkTransportAreaSubscription = this.services.getLinkTransportAreas().subscribe({
      next: (response: GLinkTransportArea[]) => {
        this.linkTransportAreas = response;
        this.isLoadingResults = false;
        this.initForm();
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  convertToFormControl(absCtrl: AbstractControl | null): FormControl {
    return absCtrl as FormControl;
  }

  initForm() {
    this.linkTransportAreaForm = this.fb.group({
      linkAreaTransportId: [null],
      transportId: [null, Validators.required],
      areas: this.fb.array([]) // Initialize areas as an empty FormArray
    });

    this.areasArray = this.linkTransportAreaForm.get('areas') as FormArray;

    // Add a value change listener to the transport dropdown
    this.linkTransportAreaForm.get('transportId')?.valueChanges.subscribe((transportId) => {
      this.updateCheckboxOptions(Number(transportId));
    });
  }

  // updateCheckboxOptions(transportId: number | null): void {
  //   debugger;
  //   const selectedAreas = this.linkTransportAreas
  //     .filter(transport => transport.transportId === Number(transportId))
  //     .map(area => area.transportAreaId);
  
  //   const areasWithCheckboxes = this.transportAreas.map(area => {
  //     return {
  //       ...area,
  //       isChecked: selectedAreas.includes(area.transportAreaId)
  //     };
  //   });
  
  //   // Clear and reinitialize the areasArray
  //   this.areasArray.clear();
  
  //   this.areasArray = this.linkTransportAreaForm.get('areas') as FormArray;
  //   for (const area of areasWithCheckboxes) {
  //     const areaGroup = this.fb.group({
  //       isChecked: new FormControl(area.isChecked),
  //       transportAreaName: new FormControl(area.transportAreaName),
  //       transportAreaId: new FormControl(area.transportAreaId)
  //     });
  //     this.areasArray.push(areaGroup);
  //   }
  // }

  
  // resetForm() {    
  //   this.linkTransportAreaForm.patchValue({
  //     linkAreaTransportId: null
  //   });
  //   this.areasArray.clear();
  // }

  resetForm() {
    // Clear the checked checkboxes
    this.areasArray.controls.forEach((control) => {
      control.get('isChecked')?.setValue(false);
    });
  }

  toggleCheckbox(index: number) {
    const isCheckedControl = this.areasArray.controls[index].get('isChecked');
    if (isCheckedControl) {
      isCheckedControl.setValue(!isCheckedControl.value);
    }
  }
  
  


  updateCheckboxOptions(transportId: number | null): void {
    debugger;
    const selectedAreas = this.linkTransportAreas
    .filter(transport => transport.transportId === Number(transportId))
    .map(area => area.transportAreaId);

    const areasWithCheckboxes = this.transportAreas.map(area => {
      return {
        ...area,
        // isChecked: transportId ? selectedAreas.includes(area.transportAreaId) : false
        isChecked: transportId ? this.isAreaChecked(area, transportId) : false
      };
    });
  
    // Clear and reinitialize the areasArray
    this.areasArray.clear();
  
    for (const area of areasWithCheckboxes) {
      const areaGroup = this.fb.group({
        isChecked: new FormControl(area.isChecked),
        transportAreaName: new FormControl(area.transportAreaName),
        transportAreaId: new FormControl(area.transportAreaId)
      });
      this.areasArray.push(areaGroup);
    }
  }
  
  private isAreaChecked(area: ETransportArea, transportId: number): boolean {
    // Check if the area is associated with the selected transport
    return this.linkTransportAreas.some(
      transport => transport.transportId === transportId && transport.transportAreaId === area.transportAreaId
    );
  }

  onSubmit() {
    debugger;
    if (this.linkTransportAreaForm.valid) {
      const formData = this.linkTransportAreaForm.value;
      const selectedAreas = formData.areas.filter((area: any) => area.isChecked);

      const newRecordArray: GLinkTransportArea[] = selectedAreas.map((area: any) => ({
        linkAreaTransportId: 0,
        transportId: formData.transportId,
        transportName: formData.transportName,
        transportAreaId: area.transportAreaId,
        transportAreaName: area.transportAreaName
      }));

      this.services.insertLinkTransportArea(newRecordArray).subscribe(
        () => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record(s) inserted successfully!', 'Dismiss', {
          duration: 1500,
        });
      },
        (error) => {
          console.error('Error inserting data:', error);
        }
      );
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }
}