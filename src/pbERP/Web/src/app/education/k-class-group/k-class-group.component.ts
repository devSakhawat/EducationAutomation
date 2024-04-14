import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { KClassGroup } from 'src/app/shared/models/Education/KClassGroup';

@Component({
  selector: 'app-k-class-group',
  templateUrl: './k-class-group.component.html',
  styleUrls: ['./k-class-group.component.css']
})

export class KClassGroupComponent implements AfterViewInit, OnInit {
  classGroups: KClassGroup[] = [];
  classGroup: KClassGroup = {} as KClassGroup;
  classGroupForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<KClassGroup>;

  displayedColumns: string[] = [
    // 'classGroupId',
    'classGroupName',
    'actions'
  ];

  private classGroupSubscription: Subscription | undefined;

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
    this.classGroupForm = this.fb.group({
      classGroupId: [null],
      classGroupName: ['', Validators.required]
    });
  }

  resetForm() {
    this.classGroupForm.reset();
    this.classGroupForm.patchValue({
      classGroupId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<KClassGroup>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classGroupSubscription) this.classGroupSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classGroupSubscription = this.services.getClassGroups().subscribe({
      next: (response: KClassGroup[]) => {
        this.classGroups = response;
        this.dataSource = new MatTableDataSource<KClassGroup>(this.classGroups);
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
    if (this.classGroupForm.valid) {
      const formData: KClassGroup = this.classGroupForm.value;
      if (formData.classGroupId) {
        // Update existing record
        this.updateRecord();
      } else {
        // Insert new record
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.classGroupForm.valid) {
      if (this.classGroupForm.get('classGroupId')?.value === null) this.classGroupForm.patchValue({ classGroupId: 0 });
      const newRecord: KClassGroup = this.classGroupForm.value;

      this.services.insertClassGroup(newRecord).subscribe(() => {
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

  onEdit(classGroupId: number) {
    if (classGroupId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByClassGroupId(classGroupId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classGroup = response;
          this.classGroupForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classGroupForm.valid) {
      const updateRecord : KClassGroup = this.classGroupForm.value;

      this.services.updateClassGroup(this.classGroupForm.value.classGroupId, updateRecord)
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

  onDetail(classGroupId: number) {
    this.services.getByClassGroupId(classGroupId).subscribe({
      next: (record) => {
        console.log('API Response:', record);

        if (record !== null) {
          this.dialog.open(this.detailDialog, {
            width: '500px',
            data: record,
          });
        } else {
          // Show an error message to the user.
          console.log('No class or hallroom record found.');
        }
      },
      error: (error) => {
        console.error('API Error:', error);
        // Handle API errors here, e.g., show an error message.
      },
    });
  }

  onDelete(row: KClassGroup) {
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

  private deleteRecord(row: KClassGroup) {
    this.services.deleteClassGroup(row.classGroupId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}