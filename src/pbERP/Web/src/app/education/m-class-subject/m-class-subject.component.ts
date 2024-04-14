import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { MClassSubject } from 'src/app/shared/models/Education/MClassSubject';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-m-class-subject',
  templateUrl: './m-class-subject.component.html',
  styleUrls: ['./m-class-subject.component.css']
})

export class MClassSubjectComponent implements AfterViewInit, OnInit {
  classSubjects: MClassSubject[] = [];
  classSubject: MClassSubject = {} as MClassSubject;
  classSubjectForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<MClassSubject>;

  displayedColumns: string[] = [
    // 'classSubjectId',
    'classSubjectSl',
    'classSubjectCode',
    'classSubjectName',
    'actions'
  ];

  private classSubjectSubscription: Subscription | undefined;

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
    this.classSubjectForm = this.fb.group({
      classSubjectId: [null],
      classSubjectSl: [0],
      classSubjectCode: ['', Validators.required],
      classSubjectName: ['', Validators.required]
    });
  }

  resetForm() {
    this.classSubjectForm.reset();
    this.classSubjectForm.patchValue({
      classSubjectId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<MClassSubject>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classSubjectSubscription) this.classSubjectSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classSubjectSubscription = this.services.getClassSubjects().subscribe({
      next: (response: MClassSubject[]) => {
        this.classSubjects = response;
        this.dataSource = new MatTableDataSource<MClassSubject>(this.classSubjects);
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
    if (this.classSubjectForm.valid) {
      const formData: MClassSubject = this.classSubjectForm.value;
      if (formData.classSubjectId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.classSubjectForm.valid) {
      if (this.classSubjectForm.get('classSubjectId')?.value === null) this.classSubjectForm.patchValue({ classSubjectId: 0 });
      const newRecord: MClassSubject = this.classSubjectForm.value;

      this.services.insertClassSubject(newRecord).subscribe(() => {
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

  onEdit(classSubjectId: number) {
    if (classSubjectId) {
      this.services.getByClassSubjectId(classSubjectId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classSubject = response;
          this.classSubjectForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classSubjectForm.valid) {
      const updateRecord : MClassSubject = this.classSubjectForm.value;

      this.services.updateClassSubject(this.classSubjectForm.value.classSubjectId, updateRecord)
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

  onDetail(classSubjectId: number) {
    this.services.getByClassSubjectId(classSubjectId).subscribe({
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

  onDelete(row: MClassSubject) {
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

  private deleteRecord(row: MClassSubject) {
    this.services.deleteClassSubject(row.classSubjectId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}