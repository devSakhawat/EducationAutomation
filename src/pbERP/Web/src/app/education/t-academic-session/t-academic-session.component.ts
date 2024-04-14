import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { TAcademicSession } from 'src/app/shared/models/Education/TAcademicSession';

@Component({
  selector: 'app-t-academic-session',
  templateUrl: './t-academic-session.component.html',
  styleUrls: ['./t-academic-session.component.css']
})


export class TAcademicSessionComponent implements AfterViewInit, OnInit {
  academicSessions: TAcademicSession[] = [];
  academicSession: TAcademicSession = {} as TAcademicSession;
  academicSessionForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<TAcademicSession>;

  displayedColumns: string[] = [
    // 'academicSessionId',
    'academicSession',
    'actions'
  ];

  private academicSessionSubscription: Subscription | undefined;

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
    this.academicSessionForm = this.fb.group({
      academicSessionId: [null],
      academicSession: ['', Validators.required]
    });
  }

  resetForm() {
    this.academicSessionForm.reset();
    this.academicSessionForm.patchValue({
      academicSessionId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<TAcademicSession>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.academicSessionSubscription) this.academicSessionSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.academicSessionSubscription = this.services.getAcademicSessions().subscribe({
      next: (response: TAcademicSession[]) => {
        this.academicSessions = response;
        this.dataSource = new MatTableDataSource<TAcademicSession>(this.academicSessions);
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
    if (this.academicSessionForm.valid) {
      const formData: TAcademicSession = this.academicSessionForm.value;
      if (formData.academicSessionId) {
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    if (this.academicSessionForm.valid) {
      if (this.academicSessionForm.get('academicSessionId')?.value === null) this.academicSessionForm.patchValue({ academicSessionId: 0 });
      const newRecord: TAcademicSession = this.academicSessionForm.value;

      this.services.insertAcademicSession(newRecord).subscribe(() => {
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

  onEdit(academicSessionId: number) {
    if (academicSessionId) {
      this.services.getByAcademicSessionId(academicSessionId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.academicSession = response;
          this.academicSessionForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    if (this.academicSessionForm.valid) {
      const updateRecord : TAcademicSession = this.academicSessionForm.value;

      this.services.updateAcademicSession(this.academicSessionForm.value.academicSessionId, updateRecord)
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

  onDetail(academicSessionId: number) {
    this.services.getByAcademicSessionId(academicSessionId).subscribe({
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

  onDelete(row: TAcademicSession) {
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

  private deleteRecord(row: TAcademicSession) {
    this.services.deleteAcademicSession(row.academicSessionId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}