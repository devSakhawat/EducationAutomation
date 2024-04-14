import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { UClassPeriod } from 'src/app/shared/models/Education/UClassPeriod';

@Component({
  selector: 'app-u-class-period',
  templateUrl: './u-class-period.component.html',
  styleUrls: ['./u-class-period.component.css']
})

export class UClassPeriodComponent implements AfterViewInit, OnInit {
  classPeriods: UClassPeriod[] = [];
  classPeriod: UClassPeriod = {} as UClassPeriod;
  classPeriodForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<UClassPeriod>;

  displayedColumns: string[] = [
    // 'classPeriodId',
    'classPeriodSl',
    'classPeriodName',
    'actions'
  ];

  private classPeriodSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', { static: true }) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', { static: true }) deleteDialog!: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private services: EducationService,
    private snackbar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.initForm();
  }

  initForm(): void {
    this.classPeriodForm = this.fb.group({
      classPeriodId: [null],
      classPeriodSl: [0],
      classPeriodName: ['', Validators.required]
    });
  }

  resetForm() {
    this.classPeriodForm.reset();
    this.classPeriodForm.patchValue({
      classPeriodId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<UClassPeriod>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classPeriodSubscription) this.classPeriodSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classPeriodSubscription = this.services.getClassPeriods().subscribe({
      next: (response: UClassPeriod[]) => {
        this.classPeriods = response;
        this.dataSource = new MatTableDataSource<UClassPeriod>(this.classPeriods);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResults = false;
        this.initForm();
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      },
    });
  }

  onSubmit() {
    debugger;
    if (this.classPeriodForm.valid) {
      const formData: UClassPeriod = this.classPeriodForm.value;
      if (formData.classPeriodId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.classPeriodForm.valid) {
      if (this.classPeriodForm.get('classPeriodId')?.value === null) this.classPeriodForm.patchValue({ classPeriodId: 0 });
      const newRecord: UClassPeriod = this.classPeriodForm.value;

      this.services.insertClassPeriod(newRecord).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record inserted successfully!', 'Dismiss', { duration: 1500 });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEdit(classPeriodId: number) {
    if (classPeriodId) {
      this.services.getByClassPeriodId(classPeriodId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classPeriod = response;
          this.classPeriodForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classPeriodForm.valid) {
      const updateRecord : UClassPeriod = this.classPeriodForm.value;

      this.services.updateClassPeriod(this.classPeriodForm.value.classPeriodId, updateRecord)
        .subscribe(() => {
          this.resetForm();
          this.onLoad();
          this.snackbar.open('Record updated successfully!', 'Dismiss',
            {
              duration: 1500,
            }
          );
        });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onDetail(classPeriodId: number) {
    this.services.getByClassPeriodId(classPeriodId).subscribe({
      next: (record) => {
        console.log('API Response:', record);

        if (record !== null) {
          this.dialog.open(this.detailDialog, {
            width: '500px',
            data: record,
          });
        } else {
          console.log('No class or hallroom record found.');
        }
      },
      error: (error) => {
        console.error('API Error:', error);
      },
    });
  }

  onDelete(row: UClassPeriod) {
    console.log(row);
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data: row,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteRecord(row);
      }
    });
  }

  private deleteRecord(row: UClassPeriod) {
    this.services.deleteClassPeriod(row.classPeriodId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
  
}