import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { RExam } from 'src/app/shared/models/Education/RExam';

@Component({
  selector: 'app-r-exam',
  templateUrl: './r-exam.component.html',
  styleUrls: ['./r-exam.component.css']
})

export class RExamComponent implements AfterViewInit, OnInit {
  exams: RExam[] = [];
  exam: RExam = {} as RExam;
  examForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<RExam>;

  displayedColumns: string[] = [
    'examSl',
    'examName',
    'examType',
    'actions'
  ];

  private examSubscription: Subscription | undefined;

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
    this.examForm = this.fb.group({
      examId: [null],
      examSl: [0, Validators.required],
      examName: ['', Validators.required],
      examType: ['', Validators.required]
    });
  }

  resetForm() {
    this.examForm.reset();
    this.examForm.patchValue({
      examId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<RExam>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.examSubscription) this.examSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.examSubscription = this.services.getExams().subscribe({
      next: (response: RExam[]) => {
        this.exams = response;
        this.dataSource = new MatTableDataSource<RExam>(this.exams);
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
    if (this.examForm.valid) {
      const formData: RExam = this.examForm.value;
      if (formData.examId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.examForm.valid) {
      if (this.examForm.get('examId')?.value === null) this.examForm.patchValue({ examId: 0 });
      const newRecord: RExam = this.examForm.value;

      this.services.insertExam(newRecord).subscribe(() => {
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

  onEdit(examId: number) {
    if (examId) {
      this.services.getByExamId(examId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.exam = response;
          this.examForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.examForm.valid) {
      const updateRecord : RExam = this.examForm.value;

      this.services.updateExam(this.examForm.value.examId, updateRecord)
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

  onDetail(examId: number) {
    this.services.getByExamId(examId).subscribe({
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

  onDelete(row: RExam) {
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

  private deleteRecord(row: RExam) {
    this.services.deleteExam(row.examId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}