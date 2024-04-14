import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { DTransport } from 'src/app/shared/models/Company/DTransport';
import { FTransportCharge } from 'src/app/shared/models/Education/FTransportCharge';
import { EducationService } from '../education.service';
import { CompanyService } from 'src/app/company/company.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ETransportArea } from 'src/app/shared/models/Education/ETransportArea';

@Component({
  selector: 'app-f-transport-charge',
  templateUrl: './f-transport-charge.component.html',
  styleUrls: ['./f-transport-charge.component.css']
})


export class FTransportChargeComponent implements AfterViewInit, OnInit {
  transportCharges: FTransportCharge[] = [];
  transportCharge: FTransportCharge = {} as FTransportCharge;
  transportChargeForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<FTransportCharge>;

  // Trasports
  transports: DTransport[] = [];
  // TrasnportArea
  transportAreas: ETransportArea[] = [];

  displayedColumns: string[] = [
    'transportName',
    'transportAreaName',
    'transportCharge',
    'actions'
  ]

  private transportChargeSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', { static: true }) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', { static: true }) deleteDialog!: TemplateRef<any>;


  constructor(
    private fb: FormBuilder,
    private services: EducationService,
    private companyServices: CompanyService,
    private snackbar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.initForm();
  }

  initForm() {
    this.transportChargeForm = this.fb.group({
      transportChargeId: [null],
      transportAreaId: [null],
      transportId: [null],
      transportCharge: ['', Validators.required],
      transportName: [''],
      transportAreaName: ['']
    });
  }

  resetForm() {
    this.transportChargeForm.reset();
    this.transportChargeForm.patchValue({
      transportChargeId: [null]
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.transportChargeSubscription) this.transportChargeSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<FTransportCharge>();

    // Get Trasnports
    this.companyServices.getTransports().subscribe(
      (response) => {
        this.transports = [];
        for (const data of response) {
          if (data.transportName !== null && !this.transports.some((x) => x.transportId === data.transportId)) {
            this.transports.push(data);
          }
        }
        console.log(this.transports);
      }
    );

    // Get TrasnportAreas
    this.services.getTransportAreas().subscribe(
      (response) => {
        this.transportAreas = [];
        for (const data of response) {
          if (data.transportAreaName !== null && !this.transportAreas.some((x) => x.transportAreaId === data.transportAreaId)) {
            this.transportAreas.push(data);
          }
        }
        console.log(this.transportAreas);
      }
    );
  }

  applyFilterDataTable(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if(this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }


  onLoad() {
    this.transportChargeSubscription = this.services.getTransportCharges().subscribe({
      next: (response: FTransportCharge[]) => {
        this.transportCharges = response;
        this.dataSource = new MatTableDataSource<FTransportCharge>(this.transportCharges);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
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

  onSubmit(){
    if(this.transportChargeForm.valid){
      const formData: FTransportCharge = this.transportChargeForm.value;
      if(formData.transportChargeId){
        this.updateRecord();
      }else{
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    if(this.transportChargeForm.valid){
      if(this.transportChargeForm.get('transportChargeId')?.value === null) this.transportChargeForm.patchValue({transportChargeId : 0});
      const newRecord : FTransportCharge = this.transportChargeForm.value;

      this.services.insertTransportCharge(newRecord).subscribe(
        () => {
          this.resetForm();
          this.onLoad();
          this.snackbar.open('New record inserted successfully!', 'Dismiss', {duration: 1500});
        }        
      );
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {duration: 2000});
    }
  }

  onEdit(transportChargeId : number){
    if(transportChargeId){
      this.services.getByTransportChargeId(transportChargeId).subscribe(
        (response) =>{
          if(response){
            this.transportCharge = response;
            this.transportChargeForm.patchValue(this.transportCharge);
          }
        }  
      );
    }
  }

  updateRecord() {
    debugger;
    if(this.transportChargeForm.valid){
      const updateRecord: FTransportCharge = this.transportChargeForm.value;

      this.services.updateTransportCharge(this.transportChargeForm.value.transportChargeId, updateRecord).subscribe(
        () => {
          this.resetForm();
          this.onLoad();
          this.snackbar.open('Record updated successfully!', 'Dismiss', {duration : 1500});
        }  
      );
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {duration: 2000});
    }
  }

  onDetail(transportChargeId: number){
    if(transportChargeId){
      this.services.getByTransportChargeId(transportChargeId).subscribe(
        (response) => {
          if(response){
            this.dialog.open(this.detailDialog, {width: '500px', data: response});
          } else {
            console.log('No building data found!');
          }
        },
        (error) => {
          console.error(error);
          // Handle API error here.
        }
      );
    }
  }

  onDelete(row : FTransportCharge){
    const dialogRef  = this.dialog.open(this.deleteDialog, {width: '500px', data: row});
    dialogRef.afterClosed().subscribe(
      (response) => {
        if(response) this.deleteRecord(row);
      }  
    );
  }

  private deleteRecord(row : FTransportCharge) {
    if(row){
      this.services.deleteTransportCharge(row.transportChargeId).subscribe(
        () => {
          this.onLoad();
          this.snackbar.open('Record deleted successfully!', 'Dismiss', {duration: 2000});
        },
        (error) => {
          this.snackbar.open('Invalid request', 'Dismiss', {duration : 1500});
        }        
      );
    }
  }
}