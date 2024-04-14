import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { LClassShift } from 'src/app/shared/models/Education/LClassShift';

@Component({
  selector: 'app-l-class-shift',
  templateUrl: './l-class-shift.component.html',
  styleUrls: ['./l-class-shift.component.css']
})

export class LClassShiftComponent implements AfterViewInit, OnInit {
  classShifts: LClassShift[] = [];
  classShift: LClassShift = {} as LClassShift;
  classShiftForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<LClassShift>;

  displayedColumns: string[] = [
    // 'classShiftId',
    'classShiftName',
    'actions'
  ];

  private classShiftSubscription: Subscription | undefined;

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
    this.classShiftForm = this.fb.group({
      classShiftId: [null],
      classShiftName: ['', Validators.required]
    });
  }

  resetForm() {
    this.classShiftForm.reset();
    this.classShiftForm.patchValue({
      classShiftId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<LClassShift>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classShiftSubscription) this.classShiftSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classShiftSubscription = this.services.getClassShifts().subscribe({
      next: (response: LClassShift[]) => {
        this.classShifts = response;
        this.dataSource = new MatTableDataSource<LClassShift>(this.classShifts);
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
    if (this.classShiftForm.valid) {
      const formData: LClassShift = this.classShiftForm.value;
      if (formData.classShiftId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.classShiftForm.valid) {
      if (this.classShiftForm.get('classShiftId')?.value === null) this.classShiftForm.patchValue({ classShiftId: 0 });
      const newRecord: LClassShift = this.classShiftForm.value;

      this.services.insertClassShift(newRecord).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record inserted successfully!', 'Dismiss', {
          duration: 1500,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEdit(classShiftId: number) {
    if (classShiftId) {
      this.services.getByClassShiftId(classShiftId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classShift = response;
          this.classShiftForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classShiftForm.valid) {
      const updateRecord : LClassShift = this.classShiftForm.value;

      this.services.updateClassShift(this.classShiftForm.value.classShiftId, updateRecord)
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

  onDetail(classShiftId: number) {
    this.services.getByClassShiftId(classShiftId).subscribe({
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

  onDelete(row: LClassShift) {
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

  private deleteRecord(row: LClassShift) {
    this.services.deleteClassShift(row.classShiftId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}