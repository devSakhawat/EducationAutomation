import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Subscription, catchError, count, tap } from 'rxjs';
import { genConfigBDivisionOrState } from 'src/app/shared/models/GeneralConfiguration/genConfigBDivisionOrState';
import { GeneralConfigurationService } from '../general-configuration.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { GenConfigACountry } from 'src/app/shared/models/GeneralConfiguration/genConfigACountry';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-b-division-or-state',
  templateUrl: './b-division-or-state.component.html',
  styleUrls: ['./b-division-or-state.component.css']
})
export class BDivisionOrStateComponent implements AfterViewInit, OnInit {
  divisionOrStates: genConfigBDivisionOrState[] = [];
  divisionOrState: genConfigBDivisionOrState = {} as genConfigBDivisionOrState;
  divisionOrStateForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<genConfigBDivisionOrState>;

  countries!: GenConfigACountry[];
  filterCountries!: GenConfigACountry[];

  displayedColumns: string[] = [
    'divisionId',
    'divisionName',
    'countryName',
    'actions'
  ]

  private divisionSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', { static: true }) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', { static: true }) deleteDialog!: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private services: GeneralConfigurationService,
    private snackbar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.initForm();
  }
  initForm() {
    this.divisionOrStateForm = this.fb.group({
      divisionId: [0],
      divisionName: ["", Validators.required],
      countryId: [0],
      countryName: ["", Validators.required],
    });
  }

  resetForm() {
    this.divisionOrStateForm.reset();
    this.divisionOrStateForm.patchValue({
      divisionId: [null],
      divisionName: [''],
      countryId: [0],
      countryName: ['']
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<genConfigBDivisionOrState>();
    this.services.getCountries().subscribe((response) => {
      this.countries = [];
      for(const data of response)
      {
        if(data.countryName !== null && !this.countries.some((x) => x.countryId === data.countryId))
        {
          this.countries.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.divisionSubscription) this.divisionSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();

  }

  onLoad() {
    this.divisionSubscription = this.services.getDivisionsOrStates().subscribe({
      next: (response: genConfigBDivisionOrState[]) => {
        this.divisionOrStates = response;
        this.dataSource = new MatTableDataSource<genConfigBDivisionOrState>(this.divisionOrStates);
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

  onSubmit() {
    console.log(this.divisionOrStateForm.get('divisionId')!.value);
    (this.divisionOrStateForm.get('divisionId')!.value) ? this.updateRecord(): this.insertRecord();
  }

  applyFilter(event: Event, field: string)
  {
    const filterValue = (event.target as HTMLInputElement).value;
    if(field === 'countryId')
    {
      this.filterCountries = [];
      this.filterCountries = this.countries.filter((x: GenConfigACountry) => x.countryName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  onCountrySelected(event: MatAutocompleteSelectedEvent)
  {
    const country: GenConfigACountry = event.option.value;
    this.divisionOrStateForm.patchValue({
      countryId: country.countryId,
      countryName: country.countryName
    });
  }

  insertRecord() {
    if (this.divisionOrStateForm.valid) {
      const divisionIdValue = this.divisionOrStateForm.get('divisionId')?.value;
      if (divisionIdValue === null) this.divisionOrStateForm.patchValue({ divisionId: 0 });
      const newRecord: genConfigBDivisionOrState = this.divisionOrStateForm.value;
      console.log(newRecord);
      this.services.insertDivisionOrState(newRecord).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record inserted successfully!', 'Dismiss', {
          duration: 1000,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEdit(row: genConfigBDivisionOrState) {
    if (row.divisionId) {
      this.divisionOrStateForm.patchValue({
        ...row
      });
    }
    else {
      this.resetForm();
    }
  }

  updateRecord() {
    if (this.divisionOrStateForm.valid) {
      const updateRecord: genConfigBDivisionOrState = this.divisionOrStateForm.value;
      this.services.updateDivisionOrState(updateRecord).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('Record updated successfully!', 'Dismiss', {
          duration: 1500
        });
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000
      })
    }
  }

  onDetail(divisionId: number) {
    this.services.getByDivisionOrStateId(divisionId)
      .pipe(
        tap((record) => {
          this.dialog.open(this.detailDialog, {
            width: '500px',
            data: record
          })
        }),
        catchError((error) => {
          console.log(error);
          return [];
        })
      ).subscribe();
  }

  onDelete(row: genConfigBDivisionOrState)
  {
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data: row
    });

    dialogRef.afterClosed().subscribe((response) => {
      if(response) this.deleteRecord(row);
    });
  }

  private deleteRecord(row: genConfigBDivisionOrState)
  {
    this.services.deleteDivisionOrState(row.divisionId).subscribe(() =>{
      this.resetForm();
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 1500
      })
    },
    (error) => {
      console.error(error);
    });
  }
}
