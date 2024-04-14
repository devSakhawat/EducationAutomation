import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { JClassSection } from 'src/app/shared/models/Education/JClassSection';

@Component({
  selector: 'app-j-class-section',
  templateUrl: './j-class-section.component.html',
  styleUrls: ['./j-class-section.component.css']
})

export class JClassSectionComponent implements AfterViewInit, OnInit {
  classSections: JClassSection[] = [];
  classSection: JClassSection = {} as JClassSection;
  classSectionForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<JClassSection>;

  displayedColumns: string[] = [
    // 'classSectionId',
    'classSectionName',
    'actions'
  ];

  private classSectionSubscription: Subscription | undefined;

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
    this.classSectionForm = this.fb.group({
      classSectionId: [null],
      classSectionName: ['', Validators.required]
    });
  }

  resetForm() {
    this.classSectionForm.reset();
    this.classSectionForm.patchValue({
      classSectionId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<JClassSection>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classSectionSubscription) this.classSectionSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classSectionSubscription = this.services.getClassSections().subscribe({
      next: (response: JClassSection[]) => {
        this.classSections = response;
        this.dataSource = new MatTableDataSource<JClassSection>(this.classSections);
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
    if (this.classSectionForm.valid) {
      const formData: JClassSection = this.classSectionForm.value;
      if (formData.classSectionId) {
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
    if (this.classSectionForm.valid) {
      if (this.classSectionForm.get('classSectionId')?.value === null) this.classSectionForm.patchValue({ classSectionId: 0 });
      const newRecord: JClassSection = this.classSectionForm.value;

      this.services.insertClassSection(newRecord).subscribe(() => {
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

  onEdit(classSectionId: number) {
    if (classSectionId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByClassSectionId(classSectionId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classSection = response;
          this.classSectionForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classSectionForm.valid) {
      const updateRecord : JClassSection = this.classSectionForm.value;

      this.services.updateClassSection(this.classSectionForm.value.classSectionId, updateRecord)
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

  onDetail(classSectionId: number) {
    this.services.getByClassSectionId(classSectionId).subscribe({
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

  onDelete(row: JClassSection) {
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

  private deleteRecord(row: JClassSection) {
    this.services.deleteClassSection(row.classSectionId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}