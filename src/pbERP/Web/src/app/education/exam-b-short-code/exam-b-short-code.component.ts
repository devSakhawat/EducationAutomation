import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ExamBShortCode } from '../../shared/models/Education/ExamBShortCode';

@Component({
  selector: 'app-exam-b-short-code',
  templateUrl: './exam-b-short-code.component.html',
  styleUrls: ['./exam-b-short-code.component.css']
})

export class ExamBShortCodeComponent implements AfterViewInit, OnInit {
  examShortCodes: ExamBShortCode[] = [];
  examShortCode: ExamBShortCode = {} as ExamBShortCode;
  examShortCodeForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<ExamBShortCode>;

  displayedColumns: string[] = [
    // 'examShortCodeId',
    'examShortCode',
    'position',
    'actions'
  ];

  private examShortCodeSubscription: Subscription | undefined;

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
    this.examShortCodeForm = this.fb.group({
      examShortCodeId: [null],
      examShortCode: ['', Validators.required],
      position: ['', Validators.required]
    });
  }

  resetForm() {
    this.examShortCodeForm.reset();
    this.examShortCodeForm.patchValue({
      examShortCodeId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<ExamBShortCode>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.examShortCodeSubscription) this.examShortCodeSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.examShortCodeSubscription = this.services.getExamShortCodes().subscribe({
      next: (response: ExamBShortCode[]) => {
        this.examShortCodes = response;
        this.dataSource = new MatTableDataSource<ExamBShortCode>(this.examShortCodes);
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
    if (this.examShortCodeForm.valid) {
      const formData: ExamBShortCode = this.examShortCodeForm.value;
      if (formData.examShortCodeId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.examShortCodeForm.valid) {
      if (this.examShortCodeForm.get('examShortCodeId')?.value === null) this.examShortCodeForm.patchValue({ examShortCodeId: 0 });
      const newRecord: ExamBShortCode = this.examShortCodeForm.value;

      this.services.insertExamShortCode(newRecord).subscribe(() => {
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

  onEdit(examShortCodeId: number) {
    if (examShortCodeId) {
      this.services.getByExamShortCodeId(examShortCodeId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.examShortCode = response;
          this.examShortCodeForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.examShortCodeForm.valid) {
      const updateRecord : ExamBShortCode = this.examShortCodeForm.value;

      this.services.updateExamShortCode(this.examShortCodeForm.value.examShortCodeId, updateRecord)
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

  onDetail(examShortCodeId: number) {
    this.services.getByExamShortCodeId(examShortCodeId).subscribe({
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

  onDelete(row: ExamBShortCode) {
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

  private deleteRecord(row: ExamBShortCode) {
    this.services.deleteExamShortCode(row.examShortCodeId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
  
}
