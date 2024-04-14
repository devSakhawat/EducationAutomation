import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Observable, Subscription, catchError, tap } from "rxjs";
import { GeneralConfigurationService } from "../general-configuration.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatDialog } from "@angular/material/dialog";
import { GenConfigGReligion } from "src/app/shared/models/GeneralConfiguration/GenConfigGReligion";


@Component({
  selector: 'app-g-religion',
  templateUrl: './g-religion.component.html',
  styleUrls: ['./g-religion.component.css']
})

export class GReligionComponent implements AfterViewInit, OnInit {
  religions: GenConfigGReligion[] = [];
  religion : GenConfigGReligion = {} as GenConfigGReligion;
  religionForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<GenConfigGReligion>;
  displayedColumns: string[] = [
    'religionId',
    'religionName',
    'actions'
  ];

  private religionSubscription : Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', {static: true}) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', {static: true}) deleteDialog!: TemplateRef<any>;

  constructor(
      private fb: FormBuilder,
      private services: GeneralConfigurationService,
      private snackbar: MatSnackBar,
      private dialog: MatDialog 
    ){
      this.initForm();
    }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<GenConfigGReligion>();
    this.onLoad();
  }
  
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy()
  {
    if(this.religionSubscription) this.religionSubscription.unsubscribe();
  }

  initForm()
  {
    this.religionForm = this.fb.group({
      religionId: 0,
      religionName : ["", Validators.required]
    });
  }

  resetForm()
  {
    this.religionForm.reset();
    this.religionForm.patchValue({
      religionId: 0
    });
  }
  
  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onLoad()
  {
    this.religionSubscription = this.services.getReligions().subscribe({
      next: (response: GenConfigGReligion[]) => {
        this.religions = response;
        this.dataSource = new MatTableDataSource<GenConfigGReligion>(this.religions);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResult = false;
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResult = true;
        this.isRateLimiTeached = false;
      }
    });
  }

  ngSubmit()
  {
    if (this.religionForm.get('religionId')!.value) {
      this.updateRecord();
    } else {
      this.insertRecord();
    }
  }

  insertRecord()
  {
    if (this.religionForm.valid) {
      const religionIdValue = this.religionForm.get('religionId')?.value;
      if (religionIdValue === null) this.religionForm.patchValue({ religionId: 0 });
      const newRecord: GenConfigGReligion = this.religionForm.value;
      this.services.insertReligion(newRecord).subscribe(() => {
        this.onLoad(); // Refresh the user list after inserting a new user
        this.resetForm(); //
        this.snackbar.open('new country record created successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEdit(row : GenConfigGReligion)
  {
    if (row.religionId) {
      this.religionForm.patchValue({
        ...row
      });
    } else {
      this.resetForm();
    }
  }

  updateRecord()
  {
    if (this.religionForm.valid) {
      const updateRecord: GenConfigGReligion = this.religionForm.value;
      this.services.updateReligion(updateRecord).subscribe(() => {
        this.onLoad(); // Refresh the user list after updating a user
        this.resetForm();
        this.snackbar.open('Record updated successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onDetail(religionId : number)
  {
    this.services.getByReligionId(religionId)
      .pipe(
        tap((record) => {
          this.dialog.open(this.detailDialog, {
            width: '100%',
            data: record,
          });
          console.log(record);
        }),
        catchError((error) => {
          console.log(error);
          return [];
        })
      )
      .subscribe();
  }

  onDelete(row : GenConfigGReligion)
  {
    console.log(row);
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data : row
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteRecord(row);
      }
    });
  }

  private deleteRecord(row : GenConfigGReligion)
  {
    this.services.deleteReligion(row.religionId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000
      });
    });
  }
}