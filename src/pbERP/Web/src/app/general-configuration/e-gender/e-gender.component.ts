import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Observable, Subscription, catchError, tap } from "rxjs";
import { GenConfigACountry } from "src/app/shared/models/GeneralConfiguration/genConfigACountry";
import { GeneralConfigurationService } from "../general-configuration.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatDialog } from "@angular/material/dialog";
import { GenConfigEGender } from "src/app/shared/models/GeneralConfiguration/genConfigEGender";


@Component({
  selector: 'app-e-gender',
  templateUrl: './e-gender.component.html',
  styleUrls: ['./e-gender.component.css']
})

export class EGenderComponent implements AfterViewInit, OnInit {
  eGenders: GenConfigEGender[] = [];
  eGender: GenConfigEGender = {} as GenConfigEGender;
  eGenderForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<GenConfigEGender>;
  displayedColumns: string[] = [
    'genderId',
    'genderName',
    'actions'
  ];

  private genderSubscription : Subscription | undefined;

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
    this.dataSource = new MatTableDataSource<GenConfigEGender>();
    this.onLoad();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy()
  {
    if(this.genderSubscription) this.genderSubscription.unsubscribe();
  }

  initForm()
  {
    this.eGenderForm = this.fb.group({
      genderId: [0],
      genderName : ["", Validators.required]
    });
  }

  resetForm()
  {
    this.eGenderForm.reset();
    this.eGenderForm.patchValue({
      genderId : [0]
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
    this.genderSubscription = this.services.getGenders().subscribe({
      next: (response: GenConfigEGender[]) => {
        this.eGenders = response;
        this.dataSource = new MatTableDataSource<GenConfigEGender>(this.eGenders);
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
    if (this.eGenderForm.get('genderId')!.value) {
      this.updateRecord();
    } else {
      this.insertRecord();
    }
  }

  insertRecord()
  {
    if (this.eGenderForm.valid) {
      const countryIdValue = this.eGenderForm.get('genderId')?.value;
      if (countryIdValue === null) this.eGenderForm.patchValue({ genderId: 0 });
      const newRecord: GenConfigEGender = this.eGenderForm.value;
      console.log(newRecord);
      this.services.insertGender(newRecord).subscribe(() => {
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

  onEdit(row : GenConfigEGender)
  {
    if (row.genderId) {
      this.eGenderForm.patchValue({
        ...row
      });
    } else {
      this.resetForm();
    }
  }

  updateRecord()
  {
    if (this.eGenderForm.valid) {
      const updateRecord: GenConfigEGender = this.eGenderForm.value;
      this.services.updateGender(updateRecord).subscribe(() => {
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

  onDetail(genderId : number)
  {
    this.services.getByGenderId(genderId)
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

  onDelete(row : GenConfigEGender)
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

  private deleteRecord(row : GenConfigEGender)
  {
    this.services.deleteGender(row.genderId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000
      });
    });
  }
}