import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { SAcademicYear } from 'src/app/shared/models/Education/SAcademicYear';

@Component({
  selector: 'app-s-academic-year',
  templateUrl: './s-academic-year.component.html',
  styleUrls: ['./s-academic-year.component.css']
})

export class SAcademicYearComponent implements AfterViewInit, OnInit {
  academicYears: SAcademicYear[] = [];
  academicYear: SAcademicYear = {} as SAcademicYear;
  academicYearForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<SAcademicYear>;

  displayedColumns: string[] = [
    // 'academicYearId',
    'academicYear',
    'actions'
  ];

  private academicYearSubscription: Subscription | undefined;

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
    this.academicYearForm = this.fb.group({
      academicYearId: [null],
      academicYear: ['', Validators.required]
    });
  }

  resetForm() {
    this.academicYearForm.reset();
    this.academicYearForm.patchValue({
      academicYearId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<SAcademicYear>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.academicYearSubscription) this.academicYearSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.academicYearSubscription = this.services.getAcademicYears().subscribe({
      next: (response: SAcademicYear[]) => {
        this.academicYears = response;
        this.dataSource = new MatTableDataSource<SAcademicYear>(this.academicYears);
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
    if (this.academicYearForm.valid) {
      const formData: SAcademicYear = this.academicYearForm.value;
      if (formData.academicYearId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.academicYearForm.valid) {
      if (this.academicYearForm.get('academicYearId')?.value === null) this.academicYearForm.patchValue({ academicYearId: 0 });
      const newRecord: SAcademicYear = this.academicYearForm.value;

      this.services.insertAcademicYear(newRecord).subscribe(() => {
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

  onEdit(academicYearId: number) {
    if (academicYearId) {
      this.services.getByAcademicYearId(academicYearId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.academicYear = response;
          this.academicYearForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.academicYearForm.valid) {
      const updateRecord : SAcademicYear = this.academicYearForm.value;

      this.services.updateAcademicYear(this.academicYearForm.value.academicYearId, updateRecord)
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

  onDetail(academicYearId: number) {
    this.services.getByAcademicYearId(academicYearId).subscribe({
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

  onDelete(row: SAcademicYear) {
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

  private deleteRecord(row: SAcademicYear) {
    this.services.deleteAcademicYear(row.academicYearId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}