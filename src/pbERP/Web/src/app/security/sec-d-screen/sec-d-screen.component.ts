import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort, MatSortHeader } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, Subscription, catchError, map, of, startWith, tap } from 'rxjs';
import { SecurityService } from '../security.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { SecDScreen } from 'src/app/shared/models/security/secDScreen';
import { GeneralConfigurationService } from 'src/app/general-configuration/general-configuration.service';
import { Module } from 'src/app/shared/models/GeneralConfiguration/module';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-sec-d-screen',
  templateUrl: './sec-d-screen.component.html',
  styleUrls: ['./sec-d-screen.component.css']
})

export class SecDScreenComponent implements AfterViewInit, OnInit {

  screens: SecDScreen[] = [];
  screen: SecDScreen = {} as SecDScreen;
  isLoadingResults = true;
  isRateLimitReached = false;
  screenForm!: FormGroup;

  moduleList!: Module[];
  parentList!: SecDScreen[];

  filterOptionsModuleList!: Module[];
  filterOptionsParentList!: SecDScreen[];

  dataSource!: MatTableDataSource<SecDScreen>;

  displayedColumns: string[] = [
    'screenId',
    'screenName',
    'controllerName',
    'parentName',
    'moduleName',
    'actions'
  ];

  private screensSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('ScreenDetailDialog', { static: true }) ScreenDetailDialog!: TemplateRef<any>;
  @ViewChild('ConfirmationDelete', { static: true }) confirmationDelete!: TemplateRef<any>;
  /**
   *
   */
  constructor(
    private formBuilder: FormBuilder,
    private services: SecurityService,
    private snackbar: MatSnackBar,
    private dialog: MatDialog,
    private genService: GeneralConfigurationService
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<SecDScreen>();
    this.loadScreens();
    this.genService.getModules().subscribe(response => {
      this.moduleList = [];
      this.moduleList = response;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    // Unsubscribe from the observable to prevent memory leaks
    if (this.screensSubscription) {
      this.screensSubscription.unsubscribe();
    }
  }

  initForm() {
    this.screenForm = this.formBuilder.group({
      screenId: [0],
      screenName: ['', Validators.required],
      controllerName: [''],
      actionName: [''],
      moduleId: [''],
      parentId: ['']
    });
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  applyFilter(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'moduleId') {
      this.filterOptionsModuleList = [];
      this.filterOptionsModuleList = this.moduleList.filter((option: Module) => option.moduleName.toLowerCase().includes(filterValue.toLowerCase()));
    }else if (field === 'parentId') {
      this.filterOptionsParentList = this.filterParentOptions(filterValue);
    }
  }


  // Custom filter function for moduleId autocomplete options
  filterModuleOptions(value: string): Module[] {
    const filterValue = value.toLowerCase();
    return this.moduleList.filter((option: Module) => option.moduleName.toLowerCase().includes(filterValue)
    );
  }

  // Custom filter function for parentId autocomplete options
  filterParentOptions(value: string): SecDScreen[] {
    const filterValue = value.toLowerCase();
    return this.parentList.filter((option: SecDScreen) => option.parentName && option.parentName.toLowerCase().includes(filterValue)
    );
  }

  onModuleOptionSelected(event: MatAutocompleteSelectedEvent) {
    const moduleId = event.option.value.moduleId;
    this.screenForm.get('moduleId')?.setValue(moduleId);
  }

  onParentOptionSelected(event: MatAutocompleteSelectedEvent) {
    const parentId = event.option.value.parentId;
    this.screenForm.get('parentId')?.setValue(parentId);
  }

  loadScreens() {
    // this.screensSubscription = this.services.getScreens().subscribe({
    this.screensSubscription = this.services.getScreens().subscribe({
      next: (response: SecDScreen[]) => {
        this.parentList = [];
        for (const screen of response) {
          if (screen.parentId !== null && screen.parentId !== 0 && !this.parentList.some((p) => p.parentId === screen.parentId)) {
            this.parentList.push(screen);
          }
        }

        this.screens = response;
        this.dataSource = new MatTableDataSource<SecDScreen>(this.screens);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.isLoadingResults = false;
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = false;
        this.isRateLimitReached = true;
      },
    });
  }

  resetForm() {
    this.screenForm.reset();
    this.screenForm.patchValue({
      screenId: [0],
      moduleId: [0],
      parentId: [0]
    });
  }

  onSubmit() {
    if (this.screenForm.get('screenId')!.value) {
      this.updateScreen();
    } else {
      this.insertScreen();
    }
  }

  // loadScreens() {
  //   this.screensSubscription = this.services.getScreens().subscribe({
  //     next: (response: SecDScreen[]) => {
  //       this.parentList = [];

  //       for (const screen of response) {
  //         if ( screen.parentId !== null && screen.parentId !== 0 && !this.parentList.some((p) => p.parentId === screen.parentId)) {
  //           this.parentList.push(screen);
  //         }
  //       }
  //       console.log(this.parentList);
  //       this.screens = response;
  //       this.dataSource = new MatTableDataSource<SecDScreen>(this.screens);
  //       this.dataSource.paginator = this.paginator;
  //       this.dataSource.sort = this.sort;
  //       this.isLoadingResults = false;
  //     },
  //     error: (error) => {
  //       console.error(error);
  //       this.isLoadingResults = false;
  //       this.isRateLimitReached = true;
  //     },
  //   });
  // }


  insertScreen() {
    if (this.screenForm.valid) {
      const screenIdValue = this.screenForm.get('screenId')?.value;
      if (screenIdValue === null) this.screenForm.patchValue({ screenId: 0 });
      const newScreen: SecDScreen = this.screenForm.value;
      console.log(newScreen);
      this.services.insertScreen(newScreen).subscribe(() => {
        this.loadScreens(); // Refresh the user list after inserting a new user
        this.resetForm();
        this.snackbar.open('Screen created successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEditScreen(row: SecDScreen) {
    if (row.screenId) {
      this.screenForm.patchValue({
        ...row
      });
    } else {
      this.resetForm();
    }
  }

  updateScreen() {
    if (this.screenForm.valid) {
      const updateScreen: SecDScreen = this.screenForm.value;
      this.services.updateScreen(updateScreen).subscribe(() => {
        this.loadScreens(); // Refresh the user list after updating a user
        this.screenForm.reset();
        this.snackbar.open('Screen updated successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  screenDetail(screenId: number) {
    this.services.getScreenByScreenId(screenId)
      .pipe(
        tap((screenData) => {
          const dialogRef = this.dialog.open(this.ScreenDetailDialog, {
            width: '100%',
            data: screenData,
          });
        }),
        catchError((error) => {
          console.log(error);
          return [];
        })
      )
      .subscribe();
  }

  confirmationRowDelete(screen: SecDScreen) {
    const dialogRef = this.dialog.open(this.confirmationDelete, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteScreen(screen);
      }
    });
  }

  private deleteScreen(screen: SecDScreen) {
    this.services.deleteScreen(screen.screenId).subscribe(() => {
      this.loadScreens();
      this.snackbar.open('Screen deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }

}