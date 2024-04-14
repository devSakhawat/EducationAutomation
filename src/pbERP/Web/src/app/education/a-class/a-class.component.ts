import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { AClass } from 'src/app/shared/models/Education/AClass';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { AccountComponent } from 'src/app/accounts/account.component';

@Component({
  selector: 'app-a-class',
  templateUrl: './a-class.component.html',
  styleUrls: ['./a-class.component.css']
})

export class AClassComponent implements OnInit{
  classes : AClass[] = [];
  class : AClass = {} as AClass;
  classForm! : FormGroup;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<AClass>;

  displayedColumns: string[] = [
    'className',
    'actions'
  ]

  private classSubscription : Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', { static: true }) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', { static: true }) deleteDialog!: TemplateRef<any>;

  /**
   *
   */
  constructor(
      private fb : FormBuilder,
      private services : EducationService,
      private snackbar : MatSnackBar,
      private dialog : MatDialog
    ) {
    this.initForm();
  }
  initForm() : void {
    this.classForm = this.fb.group({
      classId : [null],
      className : ['', Validators.required]
    });
  }

  resetForm() {
    this.classForm.reset();
    this.classForm.patchValue({
      classId : [null]
    });
  }


  ngOnInit() : void{
    this.onLoad();
    this.dataSource = new MatTableDataSource<AClass>();
  }

  ngAfterViewInit() : void{
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(){
    if(this.classSubscription) this.classSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event){
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if(this.dataSource.paginator) this.dataSource.paginator.firstPage();
  }

  onLoad() {
    this.classSubscription = this.services.getClasses().subscribe({
      next: (response : AClass[]) => {
        this.classes = response;
        this.dataSource = new MatTableDataSource<AClass>(this.classes);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoadingResults = false;
        this.initForm();
      },
      error : (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  onSubmit(){
    if(this.classForm.valid){
      const formData : AClass = this.classForm.value;

      if(formData.classId){
        this.updateRecord();
      } else {
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    if(this.classForm.valid){
      if(this.classForm.get('classId')?.value === null) this.classForm.patchValue({classId : 0});
      const newRecord : AClass = this.classForm.value;

      this.services.insertClass(newRecord).subscribe(
        () => {
          this.resetForm();
          this.onLoad();
          this.snackbar.open('New record inserted successfully!', 'Dismiss', {duration : 1500});
        }
      );
    }else{
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {duration: 2000});
    }
  }

  onEdit(classId : number){
    if(classId){
      this.services.getByClassId(classId).subscribe(
        (response) => {
          if(response){
            this.class = response;
            this.classForm.patchValue(this.class);
          }
        }  
      );
    }
  }

  updateRecord() {
    if(this.classForm.valid){
      const updateRecord : AClass = this.classForm.value;

      this.services.updateClass(this.classForm.value.classId, updateRecord).subscribe(
        (response) => {
          this.resetForm();
          this.onLoad();
          this.snackbar.open('Record updated successfully!', 'Dismiss', {duration : 1500});
        }
      );
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {duration : 2000});
    }
  }

  onDetail(classId : number){
    debugger;
    this.services.getByClassId(classId).subscribe({
      next: (response) => {
        console.log("API Response : ", response);
        if(response !== null){
          this.dialog.open(this.detailDialog, { width: '500px', data : response});
        } else {
          console.log("No class record found!");
        }
      },
      error : (error) => {
        console.error("API Error", error);
        // Handle API errors here, e.g., show an error message.
      }
    });
  }

  onDelete(row : AClass) {
    const dialogRef = this.dialog.open(this.deleteDialog, { width : '500px', data: row });
    debugger;
    dialogRef.afterClosed().subscribe(
      (response) => {
        if(response){
          this.deleteRecord(row);
        }
      }  
    );
  }

  private deleteRecord(row : AClass){
    debugger;
    this.services.deleteClass(row.classId).subscribe(
      () => {
        this.onLoad();
        this.snackbar.open('Record deleted successfully!', 'Dismiss', {duration: 2000});
      }  
    );
  }
}