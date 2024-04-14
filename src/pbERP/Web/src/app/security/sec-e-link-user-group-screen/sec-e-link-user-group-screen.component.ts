import { AfterViewInit, ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { LinkUserGroupScreen } from 'src/app/shared/models/security/linkUserGroupScreen';
import { SecDScreen } from 'src/app/shared/models/security/secDScreen';
import { UserGroup } from 'src/app/shared/models/security/userGroup';
import { SecurityService } from '../security.service';
import { Subscription, catchError, merge, startWith, tap } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { FormBuilder, FormGroup, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { __values } from 'tslib';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-sec-e-link-user-group-screen',
  templateUrl: './sec-e-link-user-group-screen.component.html',
  styleUrls: ['./sec-e-link-user-group-screen.component.css']
})

export class SecELinkUserGroupScreenComponent implements AfterViewInit, OnInit {

  linkUserGroupScreens: LinkUserGroupScreen[] = [];
  linkUserGroupScreenList: LinkUserGroupScreen[] = [];
  linkUserGroupScreen: LinkUserGroupScreen = {} as LinkUserGroupScreen;
  isLoadingResults = true;
  isRateLimitReached = false;

  linkUserGroupScreenForm!: FormGroup;

  permissionForm! : FormGroup;
  screensArray!: FormArray;

  userGroupList!: UserGroup[];
  screenList: SecDScreen[] = [];

  filterOptionsUserGroupList!: UserGroup[];
  filterOptionScreenList: SecDScreen[] = [];

  dataSource!: MatTableDataSource<LinkUserGroupScreen>;

  displayedColumns: string[] = [
    'userGroupName',
    'actions'
  ]

  private linkUserGroupScreenSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('LinkUserGroupScreenDetailDialog', { static: true }) linkUserGroupScreenDetailDialog!: TemplateRef<any>;
  @ViewChild('ConfirmationDelete', { static: true }) confirmationDelete!: TemplateRef<any>;
  @ViewChild('InsertUserGroupScreenPermission') InsertUserGroupScreenPermission!: TemplateRef<any>;


  constructor(
    private services: SecurityService,
    private fb: FormBuilder,
    private snackbar: MatSnackBar,
    private dialog: MatDialog,
    private cdr : ChangeDetectorRef 
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    this.loadLinkUserGroupScreens();

    this.services.getUserGroups().subscribe(response => {
      this.userGroupList = [];
      for (const data of response) {
        if (data.userGroupName !== null && !this.userGroupList.some((x) => x.userGroupId === data.userGroupId)) {
          this.userGroupList.push(data);
        }
      }
    });

    this.services.getScreens().subscribe(response => {
      for (const data of response) {
        if (data.screenName !== null && !this.screenList.some((x) => x.screenId === data.screenId)) {
          this.screenList.push(data);
          // this.allScreen.push(data);
        }
      }
    });
  }

  ngAfterViewInit(): void {
    if (this.dataSource) {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  ngOnDestroy() {
    (this.linkUserGroupScreenSubscription) ? this.linkUserGroupScreenSubscription.unsubscribe() : this.linkUserGroupScreenSubscription;
  }

  initForm() {
    this.linkUserGroupScreenForm = this.fb.group({
      linkUserGroupScreenId: [0],
      userGroupId: [null],
      screenId: [null],
      screenName: [null],
      userGroupName: [null],
    });
  }

  convertToFormControl(absCtrl: AbstractControl | null): FormControl {
    return absCtrl as FormControl;
  }

  openInsertUserGroupScreenDialog(row: LinkUserGroupScreen) {
    this.permissionForm = this.fb.group({
      linkUserGroupScreenId: [0],
      userGroupId: [null],
      userGroupName: [null],
      screens: this.fb.array([]),
    });

    // Table this.linkUserGroupScreen condition (return screenId list whoes userGroupId is the userGroupName Button userGroupId)
    const selectedScreens = this.linkUserGroupScreens.filter(screen => screen.userGroupId === row.userGroupId).map(screen => screen.screenId);
    const screensWithCheckboxes = this.screenList.map(screen => {
      return {
        ...screen,
        isChecked: selectedScreens.includes(screen.screenId)
      };
    });

    this.screensArray = this.permissionForm.get('screens') as FormArray;
    for (const screen of screensWithCheckboxes) {
      const screenGroup = this.fb.group({
        isChecked: new FormControl(screen.isChecked),
        screenName: new FormControl(screen.screenName),
        screenId: new FormControl(screen.screenId),
      });
      this.screensArray.push(screenGroup);
    }
    console.log(this.screensArray);

    this.permissionForm.patchValue({
      linkUserGroupScreenId: 0,
      userGroupId: row.userGroupId,
      userGroupName: row.userGroupName
      // screenList: screensWithCheckboxes
    });

    this.dialog.open(this.InsertUserGroupScreenPermission, {
      width: '500px',
      data: {
        screens: this.permissionForm.get('screens') as FormArray
      }
    });

    // dialogRef.afterClosed().subscribe((result) => {
    //   if (result) {
    //     // Handle form submission
    //     const selectedScreenIds = result.screens
    //       .filter((screen: any) => screen.isChecked)
    //       .map((screen: any) => screen.screenId);

    //     // Here, you can call the API to post the selected screenIds for the given userGroupId
    //     // You can use the 'selectedScreenIds' array to send the selected data to the server
    //     // For example:
    //     const postData = {
    //       linkUserGroupScreenId: row.linkUserGroupScreenId,
    //       userGroupId: row.userGroupId,
    //       screenIds: selectedScreenIds
    //     };

    //     // Now, you can call your service method to post the data to the server
    //     // this.services.saveLinkUserGroupScreen(postData).subscribe((response) => {
    //     //   // Handle the response, if needed
    //     // });
    //   }
    // });
  }

  submitData() {
    if (this.permissionForm.valid) {
      const formData = this.permissionForm.value;

      // Get the selected screens with isChecked = true
      const selectedScreens = formData.screens.filter((screen: any) => screen.isChecked);

      // Create an array of LinkUserGroupScreen objects
      const linkUserGroupScreensArray: LinkUserGroupScreen[] = selectedScreens.map((screen: any) => ({
        linkUserGroupScreenId: 0,
        userGroupId: formData.userGroupId,
        screenId: screen.screenId,
        screenName: screen.screenName,
        userGroupName: formData.userGroupName,
      }));
      console.log(linkUserGroupScreensArray);

      this.services.batchInsertLinkUserGroupScreen(linkUserGroupScreensArray).subscribe(
        () => {
          this.loadLinkUserGroupScreens();
          this.resetForm();
          this.snackbar.open('Data successfully inserted!', 'Dismiss', {
            duration: 2000,
          });
  
          this.cdr.detectChanges(); // Manually trigger change detection
        },
        (error) => {
          console.error('Error inserting data:', error);
        }
      );
    }
    else{
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  resetForm() {
    this.linkUserGroupScreenForm.reset();
    this.linkUserGroupScreenForm.patchValue({
      linkUserGroupScreenId: 0
    });
  }
  loadLinkUserGroupScreens() {
    this.linkUserGroupScreenSubscription = this.services.getLinkUserGroupScreens().subscribe({
      next: (response: LinkUserGroupScreen[]) => {
        for (const data of response) {
          if (data.userGroupId !== null && data.userGroupId !== 0 && !this.linkUserGroupScreenList.some((x) => x.userGroupId === data.userGroupId)) {
            this.linkUserGroupScreenList.push(data);
          }
        }

        this.linkUserGroupScreens = response;
        this.dataSource = new MatTableDataSource<LinkUserGroupScreen>(this.linkUserGroupScreenList);

        // Move the paginator assignment here
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

        this.isLoadingResults = false;
      },
      error: (error) => {
        console.log(error);
        this.isLoadingResults = false;
        this.isRateLimitReached = true;
      }
    });
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();

  }



  onSubmit() {
    (this.linkUserGroupScreenForm.get('linkUserGroupScreenId')!.value) ? this.updateLinkUserGroupScreen() : this.insertLinkUserGroupScreen();
  }

  onUserGroupSelected(event: MatAutocompleteSelectedEvent) {
    const userGroup: UserGroup = event.option.value;
    this.linkUserGroupScreenForm.patchValue({
      userGroupId: userGroup.userGroupId,
      userGroupName: userGroup.userGroupName
    });
  }

  onScreenSelected(event: MatAutocompleteSelectedEvent) {
    const screen: SecDScreen = event.option.value;
    this.linkUserGroupScreenForm.patchValue({
      screenId: screen.screenId,
      screenName: screen.screenName
    });
  }

  applyFilter(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'userGroupId') {
      this.filterOptionsUserGroupList = [];
      this.filterOptionsUserGroupList = this.userGroupList.filter((x: UserGroup) => x.userGroupName.toLowerCase().includes(filterValue.toLowerCase()));
    } else if (field === 'screenId') {
      this.filterOptionScreenList = this.screenList.filter((x: SecDScreen) => x.screenName.toLowerCase().includes(filterValue.trim().toLowerCase()));
    }
  }

  batchInsertLinkUserGroupScreen() {
    console.log(this.screenList);
    console.log(this.userGroupList);
    console.log(this.linkUserGroupScreenForm.get('screenId'));
    console.log(this.linkUserGroupScreenForm.get('userGroupId'));
    debugger;
    if (this.linkUserGroupScreenForm.valid) {
      const linkUserGroupScreenIdValue = this.linkUserGroupScreenForm.get('linkUserGroupScreenId')?.value;
      if (linkUserGroupScreenIdValue === null) this.linkUserGroupScreenForm.patchValue({ linkUserGroupScreenId: 0 });

      const newLinkUserGroupScreen: LinkUserGroupScreen = this.linkUserGroupScreenForm.value;
      console.log(newLinkUserGroupScreen);
      this.services.insertLinkUserGroupScreen(newLinkUserGroupScreen).subscribe(() => {
        console.log(newLinkUserGroupScreen);
        this.loadLinkUserGroupScreens();
        this.resetForm();
        this.snackbar.open('Data successfully inserted!', 'Dismiss', {
          duration: 2000,
        })
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  insertLinkUserGroupScreen() {
    // console.log(this.screenList);
    // console.log(this.userGroupList);
    // console.log(this.linkUserGroupScreenForm.get('screenId'));
    // console.log(this.linkUserGroupScreenForm.get('userGroupId'));
    if (this.linkUserGroupScreenForm.valid) {
      const linkUserGroupScreenIdValue = this.linkUserGroupScreenForm.get('linkUserGroupScreenId')?.value;
      if (linkUserGroupScreenIdValue === null) this.linkUserGroupScreenForm.patchValue({ linkUserGroupScreenId: 0 });

      const newLinkUserGroupScreen: LinkUserGroupScreen = this.linkUserGroupScreenForm.value;
      console.log(newLinkUserGroupScreen);
      this.services.insertLinkUserGroupScreen(newLinkUserGroupScreen).subscribe(() => {
        console.log(newLinkUserGroupScreen);
        this.loadLinkUserGroupScreens();
        this.resetForm();
        this.snackbar.open('Data successfully inserted!', 'Dismiss', {
          duration: 2000,
        })
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEditLinkUserGroupScreen(row: LinkUserGroupScreen) {
    (row.linkUserGroupScreenId) ? this.linkUserGroupScreenForm.patchValue({ ...row }) : this.resetForm();
  }

  updateLinkUserGroupScreen() {
    if (this.linkUserGroupScreenForm.valid) {
      const updateLinkUserGroupScreen: LinkUserGroupScreen = this.linkUserGroupScreenForm.value;
      this.services.updateLinkUserGroupScreen(updateLinkUserGroupScreen).subscribe(() => {
        this.loadLinkUserGroupScreens();
        this.resetForm();
        this.snackbar.open('Data successfully updated!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  linkUserGroupScreenDetail(linkUserGroupScreenId: number) {
    this.services.getLinkUserGroupScreensById(linkUserGroupScreenId)
      .pipe(
        tap((linkUserGroupScreenData) => {
          this.dialog.open(this.linkUserGroupScreenDetailDialog, {
            width: '800px',
            data: linkUserGroupScreenData
          })
        }),
        catchError((error) => {
          console.log(error);
          return [];
        })
      )
      .subscribe();
  }

  confirmationRowDelete(row: LinkUserGroupScreen) {
    const dialogRef = this.dialog.open(this.confirmationDelete, {
      width: '800px'
    })

    dialogRef.afterClosed().subscribe((result) => {
      if (result) this.deleteLinkUserGroupScreen(row);
    });
  }
  
  private deleteLinkUserGroupScreen(row: LinkUserGroupScreen) {
    this.services.deleteLinkUserGroupScreen(row.linkUserGroupScreenId).subscribe(() => {
      this.loadLinkUserGroupScreens();
      this.resetForm();
      this.snackbar.open('record successfully deleted!', 'Dismiss', {
        duration: 2000,
      });
    });
  }
}


  // Example:
  // // Specify the `disabled` property at control creation time:
  // form = new FormGroup({
  //   first: new FormControl({value: 'Nancy', disabled: true}, Validators.required),
  //   last: new FormControl('Drew', Validators.required)
  // });

  // // Controls can also be enabled/disabled after creation:
  // form.get('first')?.enable();
  // form.get('last')?.disable();
