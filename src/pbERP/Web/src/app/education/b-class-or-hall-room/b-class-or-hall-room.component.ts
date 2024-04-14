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
import { BBuilding } from 'src/app/shared/models/Education/BBuilding';

@Component({
  selector: 'app-b-class-or-hall-room',
  templateUrl: './b-class-or-hall-room.component.html',
  styleUrls: ['./b-class-or-hall-room.component.css']
})

export class BClassOrHallRoomComponent implements AfterViewInit, OnInit {
  classOrHallRooms: BClassOrHallRoom[] = [];
  classOrHallRoom: BClassOrHallRoom = {} as BClassOrHallRoom;
  classOrHallRoomForm!: FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<BClassOrHallRoom>;

  buildings!: BBuilding[];

  displayedColumns: string[] = [
    'classRoomName',
    'accomodationCapacity',
    'buildingName',
    'actions'
  ];

  private classOrHallRommSubscription: Subscription | undefined;

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
    this.classOrHallRoomForm = this.fb.group({
      classRoomId: [null],
      buildingId: [0],
      classRoomName: ['', Validators.required],
      accomodationCapacity: [0]
    });
  }

  resetForm() {
    this.classOrHallRoomForm.reset();
    this.classOrHallRoomForm.patchValue({
      classRoomId: [null],
      buildingId: [0]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<BClassOrHallRoom>();

    // Get Building from Database
    this.services.getBuildings().subscribe((response) => {
      this.buildings = [];
      for (const data of response) {
        if ( data.buildingName !== null && !this.buildings.some((x) => x.buildingId === data.buildingId))
        {
          this.buildings.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.classOrHallRommSubscription) this.classOrHallRommSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classOrHallRommSubscription = this.services.getClassOrHallRooms().subscribe({
      next: (response: BClassOrHallRoom[]) => {
        this.classOrHallRooms = response;
        this.dataSource = new MatTableDataSource<BClassOrHallRoom>(this.classOrHallRooms);
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
    console.log(this.classOrHallRoomForm.value);
    console.log(this.classOrHallRoomForm.get("classRoomId")!.value);
    debugger;

      // if(formData.classRoomId == 0)
      // {
      //   this.insertRecord();
      // }
      // else if(formData.classRoomId > 1){
      //   this.updateRecord();
      // }
      // else{
      //   console.log("Invalid form");
      // }

    if (this.classOrHallRoomForm.valid) {
      const formData: BClassOrHallRoom = this.classOrHallRoomForm.value;
      if (formData.classRoomId ) {
        // Update existing student
        this.updateRecord();
      } else {
        // Insert new student
        this.insertRecord();
      }
    }
    else{
      console.log("Invalid form");
    }
  }

  insertRecord() {
    debugger;
    if (this.classOrHallRoomForm.valid) {
      if (this.classOrHallRoomForm.get('classRoomId')?.value === null) this.classOrHallRoomForm.patchValue({ classRoomId: 0 });
      const newClassOrHallRoom: BClassOrHallRoom = this.classOrHallRoomForm.value;
      // const formData = new FormData();
      // const formValue = this.classOrHallRoomForm.value;
      // console.log(this.classOrHallRoomForm.value);
      // for (const key in formValue) {
      //   if (formValue.hasOwnProperty(key)) {
      //     let value = formValue[key];

      //     // Set classRoomId to 0
      //     if (key === 'classRoomId' && value !== 0) {
      //       value = 0;
      //     }
      //     formData.append(key, value);
      //   }
      // }
      // formData.forEach((value, key) => {
      //   console.log(key, value);
      // });

      this.services.insertClassOrHallRoom(newClassOrHallRoom).subscribe(() => {
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

  onEdit(classRoomId: number) {
    if (classRoomId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByClassOrHallRoomId(classRoomId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.classOrHallRoom = response;
          this.classOrHallRoomForm.patchValue(response);
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.classOrHallRoomForm.valid) {
      const updateRecord : BClassOrHallRoom = this.classOrHallRoomForm.value;
      //updateRecord.buildingId = (updateRecord.buildingId === this.classOrHallRoom.buildingId && updateRecord.buildingName !== this.classOrHallRoom.buildingName) ? 1 : updateRecord.buildingId;
      //console.log(updateRecord);

      this.services.updateClassOrHallRoom(this.classOrHallRoomForm.value.classRoomId, updateRecord)
        .subscribe(() => {
          this.resetForm();
          this.onLoad();
          this.snackbar.open( ' updated successfully!', 'Dismiss',
            {
              duration: 1500
            }
          );
        });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onDetail(classRoomId: number) {
    this.services.getByClassOrHallRoomId(classRoomId).subscribe({
      next: (record) => {
        console.log('API Response:', record);

        if (record !== null) {
          this.dialog.open(this.detailDialog, {
            width: '100%',
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

  onDelete(row: BClassOrHallRoom) {
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

  private deleteRecord(row: BClassOrHallRoom) {
    this.services.deleteClassOrHallRoom(row.classRoomId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }
}
