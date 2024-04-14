import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ACompany } from 'src/app/shared/models/Company/ACompany';
import { BBuilding } from 'src/app/shared/models/Education/BBuilding';
import { EducationService } from '../education.service';
import { CompanyService } from 'src/app/company/company.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-b-building',
  templateUrl: './b-building.component.html',
  styleUrls: ['./b-building.component.css']
})
export class BBuildingComponent implements AfterViewInit, OnInit {
  buildings: BBuilding[] = [];
  building: BBuilding = {} as BBuilding;
  buildingForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<BBuilding>;

  // Companies
  companies: ACompany[] = [];

  displayedColumns: string[] = [
    'buildingName',
    'usesType',
    'companyName',
    'actions'
  ]

  private buildingSubscription: Subscription | undefined;

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
    this.buildingForm = this.fb.group({
      buildingId: [null],
      buildingName: ['', Validators.required],
      usesType: [''],
      companyId: [null],
      companyName: ['']
    });
  }

  resetForm() {
    this.buildingForm.reset();
    this.buildingForm.patchValue({
      buildingId: [null]
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.buildingSubscription) this.buildingSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<BBuilding>();

    // Get Companies
    this.companyServices.getCompanies().subscribe(
      (response) => {
        this.companies = [];
        for (const data of response) {
          if (data.companyCode !== null && !this.companies.some((x) => x.companyId === data.companyId)) {
            this.companies.push(data);
          }
        }
        console.log(this.companies);
      }
    );
  }

  applyFilterDataTable(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if(this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }


  onLoad() {
    this.buildingSubscription = this.services.getBuildings().subscribe({
      next: (response: BBuilding[]) => {
        this.buildings = response;
        this.dataSource = new MatTableDataSource<BBuilding>(this.buildings);
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
    if(this.buildingForm.valid){
      const formData: BBuilding = this.buildingForm.value;
      if(formData.buildingId){
        this.updateRecord();
      }else{
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    if(this.buildingForm.valid){
      if(this.buildingForm.get('buildingId')?.value === null) this.buildingForm.patchValue({buildingId : 0});
      const newRecord : BBuilding = this.buildingForm.value;

      this.services.insertBuilding(newRecord).subscribe(
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

  onEdit(buildingId : number){
    if(buildingId){
      this.services.getByBuildingId(buildingId).subscribe(
        (response) =>{
          if(response){
            this.building = response;
            this.buildingForm.patchValue(this.building);
          }
        }  
      );
    }
  }

  updateRecord() {
    if(this.buildingForm.valid){
      const updateRecord: BBuilding = this.buildingForm.value;

      this.services.updateBuilding(this.buildingForm.value.buildingId, updateRecord).subscribe(
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

  onDetail(buildingId: number){
    if(buildingId){
      this.services.getByBuildingId(buildingId).subscribe(
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

  onDelete(row : BBuilding){
    const dialogRef  = this.dialog.open(this.deleteDialog, {width: '500px', data: row});
    dialogRef.afterClosed().subscribe(
      (response) => {
        if(response) this.deleteRecord(row);
      }  
    );
  }

  private deleteRecord(row : BBuilding) {
    if(row){
      this.services.deleteBuilding(row.buildingId).subscribe(
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
