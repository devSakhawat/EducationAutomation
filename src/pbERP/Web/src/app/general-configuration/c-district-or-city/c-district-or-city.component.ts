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
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { GenConfigCDistrictOrCity } from 'src/app/shared/models/GeneralConfiguration/genConfigCDistrictOrCity';

@Component({
  selector: 'app-c-district-or-city',
  templateUrl: './c-district-or-city.component.html',
  styleUrls: ['./c-district-or-city.component.css']
})
export class CDistrictOrCityComponent implements AfterViewInit, OnInit {
  districtsOrCities: GenConfigCDistrictOrCity[] = [];
  districtOrCity: GenConfigCDistrictOrCity = {} as GenConfigCDistrictOrCity;
  districtOrCityForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<GenConfigCDistrictOrCity>;

  divisions!: genConfigBDivisionOrState[];
  filterDivisions!: genConfigBDivisionOrState[];

  displayedColumns: string[] = [
    'districtId',
    'districtName',
    'divisionName',
    'actions'
  ]

  private districtSubscription: Subscription | undefined;

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
    this.districtOrCityForm = this.fb.group({
      districtId: [0],
      districtName: ['', Validators.required],
      divisionId: [0],
      divisionName: ['']
    });
  }

  resetForm() {
    this.districtOrCityForm.reset();
    this.districtOrCityForm.patchValue({
      districtId: [null],
      divisionId: [0],
      divisionName: [null]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<GenConfigCDistrictOrCity>();
    this.services.getDivisionsOrStates().subscribe((response) => {
      this.divisions = [];
      for(const data of response)
      {
        if(data.divisionName !== null && !this.divisions.some((x) => x.divisionId === data.divisionId))
        {
          this.divisions.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.districtSubscription) this.districtSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();

  }

  onLoad() {
    this.districtSubscription = this.services.getDistrictsOrCities().subscribe({
      next: (response: GenConfigCDistrictOrCity[]) => {
        this.districtsOrCities = response;
        this.dataSource = new MatTableDataSource<GenConfigCDistrictOrCity>(this.districtsOrCities);
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
    console.log(this.districtOrCityForm.get('districtId')!.value);
    (this.districtOrCityForm.get('districtId')!.value) ? this.updateRecord(): this.insertRecord();
  }

  applyFilter(event: Event, field: string)
  {
    const filterValue = (event.target as HTMLInputElement).value;
    if(field === 'divisionId')
    {
      this.filterDivisions = [];
      this.filterDivisions = this.divisions.filter((x: genConfigBDivisionOrState) => x.divisionName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  onDivisionSelected(event: MatAutocompleteSelectedEvent)
  {
    const division: genConfigBDivisionOrState = event.option.value;
    this.districtOrCityForm.patchValue({
      divisionId: division.divisionId,
      divisionName: division.divisionName
    });
  }

  insertRecord() {
    if (this.districtOrCityForm.valid) {
      const districtIdValue = this.districtOrCityForm.get('districtId')?.value;
      if (districtIdValue === null) this.districtOrCityForm.patchValue({ districtId: 0 });
      const newRecord: GenConfigCDistrictOrCity = this.districtOrCityForm.value;
      console.log(newRecord);
      this.services.insertDistrictOrCity(newRecord).subscribe(() => {
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

  onEdit(row: GenConfigCDistrictOrCity) {
    if (row.districtId) {
      this.districtOrCityForm.patchValue({
        ...row
      });
    }
    else {
      this.resetForm();
    }
  }

  updateRecord() {
    if (this.districtOrCityForm.valid) {
      const updateRecord: GenConfigCDistrictOrCity = this.districtOrCityForm.value;
      this.services.updateDistrictOrCity(updateRecord).subscribe(() => {
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

  onDetail(districtId: number) {
    this.services.getByDistrictOrCityId(districtId)
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

  onDelete(row: GenConfigCDistrictOrCity)
  {
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data: row
    });

    dialogRef.afterClosed().subscribe((response) => {
      if(response) this.deleteRecord(row);
    });
  }

  private deleteRecord(row: GenConfigCDistrictOrCity)
  {
    this.services.deleteDistrictOrCity(row.districtId).subscribe(() =>{
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
