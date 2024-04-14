import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { SecurityService } from '../security.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { UserGroup } from 'src/app/shared/models/security/userGroup';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { FormBuilder, FormGroup, NgForm, Validators, FormGroupName } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-sec-a-user-group',
  templateUrl: './sec-a-user-group.component.html',
  styleUrls: ['./sec-a-user-group.component.css']
})
// export class SecAUserGroupComponent implements AfterViewInit, OnInit {
export class SecAUserGroupComponent implements AfterViewInit, OnInit {

  userGroupForm!: FormGroup;
  userGroups: UserGroup[] = [];
  userGroup : UserGroup = {} as UserGroup;
  isLoadingResults = true;
  isRateLimitReached = false;

  dataSource!: MatTableDataSource<UserGroup>;

  displayedColumns: string[] = [
    'userGroupId',
    'userGroupName',
    'userGroupNameLocal',
    'userGroupDescription',
    'userGroupDescriptionLocal',
    'userGroupSaleDiscount',
    'actions'
  ];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('UserGroupDetailDialog', { static: true }) userGroupDetailDialog!: TemplateRef<any>;
  @ViewChild('ConfirmationDelete', { static: true }) confirmationDelete!: TemplateRef<any>;

  constructor(
    private formBuilder: FormBuilder,
    private securityService: SecurityService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.initForm();
   }

  ngOnInit() {
    this.dataSource = new MatTableDataSource<UserGroup>();
    this.loadUserGroups();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  initForm() {
    this.userGroupForm = this.formBuilder.group({
      userGroupId: [0], // Default value for userGroupId is 0
      userGroupName: ['', Validators.required],
      userGroupNameLocal: [''],
      userGroupDescription: [''],
      userGroupDescriptionLocal: [''],
      userGroupSaleDiscount: [0], // Default value for userGroupSaleDiscount is 0
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  loadUserGroups() {
    this.securityService.getUserGroups().subscribe(
      (response: UserGroup[]) => {
        this.dataSource.data = response;
        console.log(this.dataSource.data);
        this.userGroups = response;
        this.dataSource = new MatTableDataSource<UserGroup>(this.userGroups);
        this.isLoadingResults = false;
      },
      (error) => {
        console.error(error);
        this.isLoadingResults = false;
        this.isRateLimitReached = true;
      }
    );
  }

  onSubmit() {
    if (this.userGroupForm.get('userGroupId')!.value) {
      this.updateUserGroup();
    } else {
      this.insertUserGroup();
    }
  }

  resetForm() {
    console.log('Resetting form...');
    this.userGroupForm.reset();
    this.userGroupForm.patchValue({
      userGroupId: 0,
      userGroupSaleDiscount : 0
      // Any other form controls that you want to reset to their default values
    });
  }

  insertUserGroup() {
    if (this.userGroupForm.valid) {
      const newUserGroup: UserGroup = this.userGroupForm.value;
      this.securityService.insertSecAUserGroup(newUserGroup).subscribe(() => {
        this.loadUserGroups();
        this.resetForm();
        this.snackBar.open('User group created successfully!', 'Dismiss', {
          duration: 3000,
        });
      });
    } else {
      this.snackBar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 3000,
      });
    }
  }

  onEditClick(row: UserGroup) {
    if (row.userGroupId) {
      // If userGroupId is present, patch the form with the row values
      this.userGroupForm.patchValue(row);
    } else {
      // If userGroupId is not present, reset the form to its default values
      this.resetForm();
    }
  }

  updateUserGroup() {
    if (this.userGroupForm.valid) {
      const updatedUserGroup: UserGroup = this.userGroupForm.value;
      this.securityService.updateUserGroup(updatedUserGroup).subscribe(() => {
        this.loadUserGroups();
        // this.userGroupForm.reset();
        this.resetForm();
        this.snackBar.open('User group updated successfully!', 'Dismiss', {
          duration: 3000,
        });
      });
    } else {
      this.snackBar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 3000,
      });
    }
  }

  confirmDeleteUserGroup(userGroup: UserGroup) {
    const dialogRef = this.dialog.open(this.confirmationDelete, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        // User clicked on "Delete" in the dialog
        this.deleteUserGroup(userGroup);
      }
    });
  }

  private deleteUserGroup(userGroup: UserGroup) {
    this.securityService.deleteUserGroup(userGroup.userGroupId).subscribe(() => {
      this.loadUserGroups();
      this.snackBar.open('User group deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }

  userGroupDetail(userGroupId: number) {
    this.securityService.getUserGroupByUserGroupId(userGroupId).subscribe(
      (userGroupData) => {
        const dialogRef = this.dialog.open(this.userGroupDetailDialog, {
          width: '500px',
          data: userGroupData
        });
      },
      (error) => {
        console.error(error);
      }
    );
  }

  userGroupDetails(userGroupId: number) {
    // debugger;

    this.securityService.getUserGroups().subscribe(
      (response: UserGroup[]) => {
        this.dataSource.data = response;
        console.log(this.dataSource.data);
        this.userGroups = response;
        this.dataSource = new MatTableDataSource<UserGroup>(this.userGroups);
        this.isLoadingResults = false;
      },
      (error) => {
        console.error(error);
        this.isLoadingResults = false;
        this.isRateLimitReached = true;
      }
    );

    this.securityService.getUserGroupByUserGroupId(userGroupId).subscribe(
      (userGroupData) => {
        const dialogRef = this.dialog.open(this.userGroupDetailDialog, {
          width: '100%',
          data: userGroupData
        });
      },
      (error) => {
        console.log(error);
      }
    );
  }



}
