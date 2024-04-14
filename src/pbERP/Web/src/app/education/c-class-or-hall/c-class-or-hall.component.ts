import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { BClassOrHallRoom } from 'src/app/shared/models/Education/BClassOrHallRoom';
import { CClassOrHall } from 'src/app/shared/models/Education/CClassOrHall';

@Component({
  selector: 'app-c-class-or-hall',
  templateUrl: './c-class-or-hall.component.html',
  styleUrls: ['./c-class-or-hall.component.css']
})

export class CClassOrHallComponent implements AfterViewInit, OnInit {
  classOrHalls: CClassOrHall[] = [];
  classOrHall: CClassOrHall = {} as CClassOrHall;
  classOrHallForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<CClassOrHall>;

  classHallrooms!: BClassOrHallRoom[];

  displayedColumns: string[] = [
    'hallSeatNumber',
    'hallSeatCharge',
    'classRoomName',
    'actions'
  ];

  private classOrHallSubscription: Subscription | undefined;

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
    this.classOrHallForm = this.fb.group({
      hallSeatId: [null],
      hallSeatNumber: [0],
      hallSeatCharge: [0.00],
      classRoomId: [0]
    });
  }

  resetForm() {
    this.classOrHallForm.reset();
    this.classOrHallForm.patchValue({
      hallSeatId: [null],
      classRoomId: [0]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<CClassOrHall>();

    // Get ClassHallRoom from Database
    this.services.getClassOrHallRooms().subscribe((response) => {
      this.classHallrooms = [];
      for (const data of response) {
        if (data.classRoomName !== null && !this.classHallrooms.some((x) => x.classRoomId === data.classRoomId))
        {
          this.classHallrooms.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classOrHallSubscription) this.classOrHallSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classOrHallSubscription = this.services.getClassOrHalls().subscribe({
      next: (response: CClassOrHall[]) => {
        this.classOrHalls = response;
        this.dataSource = new MatTableDataSource<CClassOrHall>(this.classOrHalls);
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
    if (this.classOrHallForm.valid) {
      const formData: CClassOrHall = this.classOrHallForm.value;
      if (formData.hallSeatId) {
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
    if (this.classOrHallForm.valid) {
      if (this.classOrHallForm.get('hallSeatId')?.value === null) this.classOrHallForm.patchValue({ hallSeatId: 0 });
      const newRecord: CClassOrHall = this.classOrHallForm.value;

      this.services.insertClassOrHall(newRecord).subscribe(() => {
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

  onEdit(hallSeatId: number) {
    if (hallSeatId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByClassOrHallId(hallSeatId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classOrHall = response;
          this.classOrHallForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classOrHallForm.valid) {
      const updateRecord : CClassOrHall = this.classOrHallForm.value;

      updateRecord.classRoomId = updateRecord.classRoomName !== this.classOrHall.classRoomName ? 1 : updateRecord.classRoomId;

      this.services.updateClassOrHall(this.classOrHallForm.value.hallSeatId, updateRecord)
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

  onDetail(hallSeatId: number) {
    this.services.getByClassOrHallId(hallSeatId).subscribe({
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

  onDelete(row: CClassOrHall) {
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

  private deleteRecord(row: CClassOrHall) {
    this.services.deleteClassOrHall(row.hallSeatId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}
