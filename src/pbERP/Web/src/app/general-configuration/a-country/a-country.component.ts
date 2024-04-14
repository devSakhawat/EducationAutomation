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


@Component({
  selector: 'app-a-country',
  templateUrl: './a-country.component.html',
  styleUrls: ['./a-country.component.css']
})

export class ACountryComponent implements AfterViewInit, OnInit {
  genConfigACountries: GenConfigACountry[] = [];
  genConfigACountry: GenConfigACountry = {} as GenConfigACountry;
  genConfigACountryForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<GenConfigACountry>;
  displayedColumns: string[] = [
    'countryId',
    'countryName',
    'actions'
  ];

  private countrySubscription : Subscription | undefined;

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
    this.dataSource = new MatTableDataSource<GenConfigACountry>();
    this.onLoad();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy()
  {
    if(this.countrySubscription) this.countrySubscription.unsubscribe();
  }

  initForm()
  {
    this.genConfigACountryForm = this.fb.group({
      countryId: [0],
      countryName :  ['', Validators.required]
    });
  }

  resetForm()
  {
    this.genConfigACountryForm.reset();
    this.genConfigACountryForm.patchValue({
      countryId : [null],
      countryName :  ['']
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
    this.countrySubscription = this.services.getCountries().subscribe({
      next: (response: GenConfigACountry[]) => {
        this.genConfigACountries = response;
        this.dataSource = new MatTableDataSource<GenConfigACountry>(this.genConfigACountries);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResult = false;
        this.initForm();
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResult = true;
        this.isRateLimiTeached = false;
      }
    });
  }

  ngSubmit() {
    if (this.genConfigACountryForm.valid) {
      const formData: GenConfigACountry = this.genConfigACountryForm.value;
      if (formData.countryId) {
        // Update existing student
        this.updateRecord();
      } else {
        // Insert new student
        this.insertRecord();
      }
    }
  }

  onSubmit()
  {
    if (this.genConfigACountryForm.get('countryId')!.value) {
      this.updateRecord();
    } else {
      this.insertRecord();
    }
  }

  insertRecord()
  {
    if (this.genConfigACountryForm.valid) {
      const countryIdValue = this.genConfigACountryForm.get('countryId')?.value;
      if (countryIdValue === null) this.genConfigACountryForm.patchValue({ countryId: 0 });
      const newRecord: GenConfigACountry = this.genConfigACountryForm.value;
      console.log(newRecord);
      this.services.insertCountry(newRecord).subscribe(() => {
        this.onLoad(); // Refresh the user list after inserting a new user
        // this.resetForm(); //
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

  onEdit(row : GenConfigACountry)
  {
    if (row.countryId) {
      this.genConfigACountryForm.patchValue({
        ...row
      });
    } else {
      this.resetForm();
    }
  }

  updateRecord()
  {
    if (this.genConfigACountryForm.valid) {
      const updateRecord: GenConfigACountry = this.genConfigACountryForm.value;
      this.services.updateCountry(updateRecord).subscribe(() => {
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

  onDetail(countryId : number)
  {
    this.services.getByCountryId(countryId)
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

  onDelete(row : GenConfigACountry)
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

  private deleteRecord(row : GenConfigACountry)
  {
    this.services.deleteCountry(row.countryId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000
      });
    });
  }

}
