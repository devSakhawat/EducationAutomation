import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Observable, Subscription, catchError, tap } from "rxjs";
import { GeneralConfigurationService } from "../general-configuration.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatDialog } from "@angular/material/dialog";
import { GenConfigEGender } from "src/app/shared/models/GeneralConfiguration/genConfigEGender";
import { GenConfigFBloodGroup } from "src/app/shared/models/GeneralConfiguration/genConfigFBloodGroup";


@Component({
  selector: 'app-f-blood-Group',
  templateUrl: './f-blood-Group.component.html',
  styleUrls: ['./f-blood-Group.component.css']
})

export class FBloodGroupComponent implements AfterViewInit, OnInit {
  bloodGroups: GenConfigFBloodGroup[] = [];
  bloodGroup : GenConfigFBloodGroup = {} as GenConfigFBloodGroup;
  bloodGroupForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<GenConfigFBloodGroup>;
  displayedColumns: string[] = [
    'bloodGroupId',
    'bloodGroupName',
    'actions'
  ];

  private bloodGroupSubscription : Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', {static: true}) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', {static: true}) deleteDialog!: TemplateRef<any>;

  constructor(
      private fb: FormBuilder,
      private services: GeneralConfigurationService,
      private snackbar: MatSnackBar,
      private dialog: MatDialog 
    ){
      this.initForm();
    }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<GenConfigFBloodGroup>();
    this.onLoad();
  }
  
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy()
  {
    if(this.bloodGroupSubscription) this.bloodGroupSubscription.unsubscribe();
  }

  initForm()
  {
    this.bloodGroupForm = this.fb.group({
      bloodGroupId: 0,
      bloodGroupName : ["", Validators.required]
    });
  }

  resetForm()
  {
    this.bloodGroupForm.reset();
    this.bloodGroupForm.patchValue({
      bloodGroupId: 0
    });
  }
  
  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onLoad()
  {
    this.bloodGroupSubscription = this.services.getBloodGroups().subscribe({
      next: (response: GenConfigFBloodGroup[]) => {
        this.bloodGroups = response;
        this.dataSource = new MatTableDataSource<GenConfigFBloodGroup>(this.bloodGroups);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResult = false;
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResult = true;
        this.isRateLimiTeached = false;
      }
    });
  }

  ngSubmit()
  {
    if (this.bloodGroupForm.get('bloodGroupId')!.value) {
      this.updateRecord();
    } else {
      this.insertRecord();
    }
  }

  insertRecord()
  {
    if (this.bloodGroupForm.valid) {
      const bloodGroupIdValue = this.bloodGroupForm.get('bloodGroupId')?.value;
      if (bloodGroupIdValue === null) this.bloodGroupForm.patchValue({ bloodGroupId: 0 });
      const newRecord: GenConfigFBloodGroup = this.bloodGroupForm.value;
      this.services.insertBloodGroup(newRecord).subscribe(() => {
        this.onLoad(); // Refresh the user list after inserting a new user
        this.resetForm(); //
        this.snackbar.open('new country record created successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEdit(row : GenConfigFBloodGroup)
  {
    if (row.bloodGroupId) {
      this.bloodGroupForm.patchValue({
        ...row
      });
    } else {
      this.resetForm();
    }
  }

  updateRecord()
  {
    if (this.bloodGroupForm.valid) {
      const updateRecord: GenConfigFBloodGroup = this.bloodGroupForm.value;
      this.services.updateBloodGroup(updateRecord).subscribe(() => {
        this.onLoad(); // Refresh the user list after updating a user
        this.resetForm();
        this.snackbar.open('Record updated successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onDetail(bloodGroupId : number)
  {
    this.services.getByBloodGroupId(bloodGroupId)
      .pipe(
        tap((record) => {
          this.dialog.open(this.detailDialog, {
            width: '100%',
            data: record,
          });
          console.log(record);
        }),
        catchError((error) => {
          console.log(error);
          return [];
        })
      )
      .subscribe();
  }

  onDelete(row : GenConfigFBloodGroup)
  {
    console.log(row);
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data : row
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteRecord(row);
      }
    });
  }

  private deleteRecord(row : GenConfigFBloodGroup)
  {
    this.services.deleteBloodGroup(row.bloodGroupId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000
      });
    });
  }
}