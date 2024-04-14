import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EduAStudent } from 'src/app/shared/models/Education/eduAStudent';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { BClassOrHallRoom } from 'src/app/shared/models/Education/BClassOrHallRoom';
import { CClassOrHall } from 'src/app/shared/models/Education/CClassOrHall';
import { DStudentAllocateHallSeat } from 'src/app/shared/models/Education/DStudentAllocateHallSeat';

@Component({
  selector: 'app-d-student-allocate-hall-seat',
  templateUrl: './d-student-allocate-hall-seat.component.html',
  styleUrls: ['./d-student-allocate-hall-seat.component.css']
})

export class DStudentAllocateHallSeatComponent implements AfterViewInit, OnInit {
  studentAllocateHallSeats: DStudentAllocateHallSeat[] = [];
  studentAllocateHallSeat: DStudentAllocateHallSeat = {} as DStudentAllocateHallSeat;
  studentAllocateHallSeatForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<DStudentAllocateHallSeat>;

  students!: EduAStudent[];
  classHallrooms!: BClassOrHallRoom[];
  classHalls!: CClassOrHall[];

  displayedColumns: string[] = [
    'allocateStudentHallSeatId',
    // 'studentId',
    // 'classRoomId',
    // 'hallSeatId',
    'studentName',
    'classRoomName',
    'hallSeatNumber',
    'actions'
  ];

  private studentAllocateHallSeatSubscription: Subscription | undefined;

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
    this.studentAllocateHallSeatForm = this.fb.group({
      allocateStudentHallSeatId: [null],
      studentId: [0],
      classRoomId: [0],
      hallSeatId: [0],
    });
  }

  resetForm() {
    this.studentAllocateHallSeatForm.reset();
    this.studentAllocateHallSeatForm.patchValue({
      allocateStudentHallSeatId: [null],
      studentId: [0],
      classRoomId: [0],
      hallSeatId: [0]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<DStudentAllocateHallSeat>();

    // Get Stduents from Database
    this.services.getStudents().subscribe((response) => {
      this.students = [];
      for (const data of response) {
        if (data.studentName !== null && !this.students.some((x) => x.studentId === data.studentId))
        {
          this.students.push(data);
        }
      }
    });

    // Get ClassHallRooms from Database
    this.services.getClassOrHallRooms().subscribe((response) => {
      this.classHallrooms = [];
      for (const data of response) {
        if (data.classRoomName !== null && !this.classHallrooms.some((x) => x.classRoomId === data.classRoomId))
        {
          this.classHallrooms.push(data);
        }
      }
    });

    // Get ClassHalls from Database
    this.services.getClassOrHalls().subscribe((response) => {
      this.classHalls = [];
      for (const data of response) {
        if (data.hallSeatNumber !== null && !this.classHalls.some((x) => x.hallSeatId === data.hallSeatId))
        {
          this.classHalls.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.studentAllocateHallSeatSubscription) this.studentAllocateHallSeatSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.studentAllocateHallSeatSubscription = this.services.getStudentAllocateHallSeats().subscribe({
      next: (response: DStudentAllocateHallSeat[]) => {
        this.studentAllocateHallSeats = response;
        this.dataSource = new MatTableDataSource<DStudentAllocateHallSeat>(this.studentAllocateHallSeats);
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
    if (this.studentAllocateHallSeatForm.valid) {
      const formData: DStudentAllocateHallSeat = this.studentAllocateHallSeatForm.value;
      if (formData.allocateStudentHallSeatId) {
        // Update existing record
        //this.updateRecord();
      } else {
        // Insert new record
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.studentAllocateHallSeatForm.valid) {
      if (this.studentAllocateHallSeatForm.get('allocateStudentHallSeatId')?.value === null) this.studentAllocateHallSeatForm.patchValue({ allocateStudentHallSeatId: 0 });
      const newRecord: DStudentAllocateHallSeat = this.studentAllocateHallSeatForm.value;

      this.services.insertStudentAllocateHallSeat(newRecord).subscribe(() => {
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

  onEdit(allocateStudentHallSeatId: number) {
    if (allocateStudentHallSeatId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByStudentAllocateHallSeatId(allocateStudentHallSeatId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.studentAllocateHallSeat = response;
          this.studentAllocateHallSeatForm.patchValue(response);
        }
      });
    }
  }

  // updateRecord() {
  //   debugger;
  //   if (this.studentAllocateHallSeatForm.valid) {
  //     const updateRecord : DStudentAllocateHallSeat = this.studentAllocateHallSeatForm.value;

  //     //updateRecord.allocateStudentHallSeatId = updateRecord.classRoomName !== this.studentAllocateHallSeatForm.classRoomName ? 1 : updateRecord.classRoomId;

  //     this.services.updateClassOrHall(this.classOrHallForm.value.hallSeatId, updateRecord)
  //       .subscribe(() => {
  //         this.resetForm();
  //         this.onLoad();
  //         this.snackbar.open('Record updated successfully!', 'Dismiss',
  //           {
  //             duration: 1500,
  //           }
  //         );
  //       });
  //   } else {
  //     this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
  //       duration: 2000,
  //     });
  //   }
  // }

  // onDetail(hallSeatId: number) {
  //   this.services.getByClassOrHallId(hallSeatId).subscribe({
  //     next: (record) => {
  //       console.log('API Response:', record);

  //       if (record !== null) {
  //         this.dialog.open(this.detailDialog, {
  //           width: '500px',
  //           data: record,
  //         });
  //       } else {
  //         // Show an error message to the user.
  //         console.log('No class or hallroom record found.');
  //       }
  //     },
  //     error: (error) => {
  //       console.error('API Error:', error);
  //       // Handle API errors here, e.g., show an error message.
  //     },
  //   });
  // }

  // onDelete(row: DStudentAllocateHallSeat) {
  //   console.log(row);
  //   const dialogRef = this.dialog.open(this.deleteDialog, {
  //     width: '500px',
  //     data: row,
  //   });
  //   dialogRef.afterClosed().subscribe((result) => {
  //     if (result) {
  //       this.deleteRecord(row);
  //     }
  //   });
  // }

  // private deleteRecord(row: DStudentAllocateHallSeat) {
  //   this.services.deleteClassOrHall(row.hallSeatId).subscribe(() => {
  //     this.onLoad();
  //     this.snackbar.open('Record deleted successfully!', 'Dismiss', {
  //       duration: 3000,
  //     });
  //   });
  // }
}
