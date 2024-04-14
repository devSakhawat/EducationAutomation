import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ETransportArea } from 'src/app/shared/models/Education/ETransportArea';

@Component({
  selector: 'app-e-transport-area',
  templateUrl: './e-transport-area.component.html',
  styleUrls: ['./e-transport-area.component.css']
})

export class ETransportAreaComponent implements AfterViewInit, OnInit {
  transportAreas: ETransportArea[] = [];
  transportArea: ETransportArea = {} as ETransportArea;
  transportAreaForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<ETransportArea>;

  displayedColumns: string[] = [
    'transportAreaName',
    'areaDistance',
    'actions'
  ];

  private transportAreaSubscription: Subscription | undefined;

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
    this.transportAreaForm = this.fb.group({
      transportAreaId: [null],
      transportAreaName: [null],
      areaDistance: [0.00]
    });
  }

  resetForm() {
    this.transportAreaForm.reset();
    this.transportAreaForm.patchValue({
      transportAreaId: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<ETransportArea>();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.transportAreaSubscription) this.transportAreaSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.transportAreaSubscription = this.services.getTransportAreas().subscribe({
      next: (response: ETransportArea[]) => {
        this.transportAreas = response;
        this.dataSource = new MatTableDataSource<ETransportArea>(this.transportAreas);
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
    if (this.transportAreaForm.valid) {
      const formData: ETransportArea = this.transportAreaForm.value;
      if (formData.transportAreaId) {
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
    if (this.transportAreaForm.valid) {
      if (this.transportAreaForm.get('transportAreaId')?.value === null) this.transportAreaForm.patchValue({ transportAreaId: 0 });
      const newRecord: ETransportArea = this.transportAreaForm.value;

      this.services.insertTransportArea(newRecord).subscribe(() => {
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

  onEdit(transportAreaId: number) {
    if (transportAreaId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByTransportAreaId(transportAreaId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.transportArea = response;
          this.transportAreaForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.transportAreaForm.valid) {
      const updateRecord : ETransportArea = this.transportAreaForm.value;

      this.services.updateTransportArea(this.transportAreaForm.value.transportAreaId, updateRecord)
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

  onDetail(transportAreaId: number) {
    this.services.getByTransportAreaId(transportAreaId).subscribe({
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

  onDelete(row: ETransportArea) {
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

  private deleteRecord(row: ETransportArea) {
    this.services.deleteTransportArea(row.transportAreaId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}