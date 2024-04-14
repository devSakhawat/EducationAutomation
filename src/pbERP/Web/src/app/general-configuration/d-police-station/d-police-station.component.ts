import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Subscription, catchError, count, tap } from 'rxjs';
import { GeneralConfigurationService } from '../general-configuration.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { GenConfigCDistrictOrCity } from 'src/app/shared/models/GeneralConfiguration/genConfigCDistrictOrCity';
import { GenConfigDPoliceStation } from 'src/app/shared/models/GeneralConfiguration/GenConfigDPoliceStation';

@Component({
  selector: 'app-d-police-station',
  templateUrl: './d-police-station.component.html',
  styleUrls: ['./d-police-station.component.css']
})
export class DPoliceStation implements AfterViewInit, OnInit {
  pss: GenConfigDPoliceStation[] = [];
  ps: GenConfigDPoliceStation = {} as GenConfigDPoliceStation;
  psForm!: FormGroup;

  isLoadingResult = true;
  isRateLimiTeached = false;
  dataSource!: MatTableDataSource<GenConfigDPoliceStation>;

  districts!: GenConfigCDistrictOrCity[];
  filterDistricts!: GenConfigCDistrictOrCity[];

  displayedColumns: string[] = [
    'policeStationId',
    'districtName',
    'policeStationName',
    'postalCode',
    'actions'
  ]

  private psSubscription: Subscription | undefined;

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
    this.psForm = this.fb.group({
      policeStationId: [0],
      districtName: ['', Validators.required],
      policeStationName: ['', Validators.required],
      postalCode: ['', Validators.required],
      districtId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<GenConfigDPoliceStation>();
    this.services.getDistrictsOrCities().subscribe((response) => {
      this.districts = [];
      for(const data of response)
      {
        if(data.districtName !== null && !this.districts.some((x) => x.districtId === data.districtId))
        {
          this.districts.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.psSubscription) this.psSubscription.unsubscribe();
  }

  resetForm() {
    this.psForm.reset();
    this.psForm.patchValue({
      policeStationId: 0,
      districtId: 0,
      districtName: null
    });
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();

  }

  onLoad() {
    this.psSubscription = this.services.getPoliceStations().subscribe({
      next: (response: GenConfigDPoliceStation[]) => {
        this.pss = response;
        this.dataSource = new MatTableDataSource<GenConfigDPoliceStation>(this.pss);
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

  onSubmit() {
    console.log(this.psForm.get('policeStationId')!.value);
    (this.psForm.get('policeStationId')!.value) ? this.updateRecord(): this.insertRecord();
  }

  applyFilter(event: Event, field: string)
  {
    const filterValue = (event.target as HTMLInputElement).value;
    if(field === 'districtId')
    {
      this.filterDistricts = [];
      this.filterDistricts = this.districts.filter((x: GenConfigCDistrictOrCity) => x.districtName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  onDistrictSelected(event: MatAutocompleteSelectedEvent)
  {
    const district: GenConfigCDistrictOrCity = event.option.value;
    this.psForm.patchValue({
      districtId: district.districtId,
      districtName: district.districtName
    });
  }

  insertRecord() {
    if (this.psForm.valid) {
      const psIdValue = this.psForm.get('policeStationId')?.value;
      if (psIdValue === null) this.psForm.patchValue({ policeStationId: 0 });
      const newRecord: GenConfigDPoliceStation = this.psForm.value;
      this.services.insertPoliceStation(newRecord).subscribe(() => {
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

  onEdit(row: GenConfigDPoliceStation) {
    if (row.policeStationId) {
      this.psForm.patchValue({
        ...row
      });
    }
    else {
      this.resetForm();
    }
  }

  updateRecord() {
    if (this.psForm.valid) {
      const updateRecord: GenConfigDPoliceStation = this.psForm.value;
      this.services.updatePoliceStation(updateRecord).subscribe(() => {
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

  onDetail(policeStationId: number) {
    this.services.getByPoliceStationId(policeStationId)
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

  onDelete(row: GenConfigDPoliceStation)
  {
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data: row
    });

    dialogRef.afterClosed().subscribe((response) => {
      if(response) this.deleteRecord(row);
    });
  }

  private deleteRecord(row: GenConfigDPoliceStation)
  {
    this.services.deletePoliceStation(row.policeStationId).subscribe(() =>{
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