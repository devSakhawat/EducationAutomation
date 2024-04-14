import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators,  } from '@angular/forms';
import { User } from 'src/app/shared/models/security/user';
import { SecurityService } from '../security.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserGroup } from 'src/app/shared/models/security/userGroup';
import { Observable, Subscription, of } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { startWith, map, tap, catchError } from 'rxjs/operators';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-sec-b-user',
  templateUrl: './sec-b-user.component.html',
  styleUrls: ['./sec-b-user.component.css']
})
export class SecBUserComponent implements AfterViewInit, OnInit {
  // userForm! : FormGroup;  
  userGroupList!: Observable<UserGroup[]>;
  // userGroups!: UserGroup[];
  // filteredUserGroups!: Observable<UserGroup[]>; 


  users : User[] = [];
  user  : User = {} as User;
  isLoadingResults = true;
  isRateLimitReached = false;

  dataSource! : MatTableDataSource<User>;

// for auto complete
  userForm!: FormGroup;

  colorarraylist!: UserGroup[];
  filteroptionslist!: Observable<UserGroup[]>
  formcontrol = new FormControl('');

  options = ["MD", "Sakhawat", "Abir"]
  optionss!: UserGroup[];


  displayedColumns: string[] = [
    'userId',
    'loginName',
    'loginNameLocal',
    'password',
    // 'userGroupId',
    'userGroupName',
    'startDate',
    'endDate',
    'actions'
  ];

  private usersSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('UserDetailDialog', { static: true }) UserDetailDialog!: TemplateRef<any>;
  @ViewChild('ConfirmationDelete', { static: true }) confirmationDelete!: TemplateRef<any>;
  /**
   *
   */
  constructor(
    private formBuilder : FormBuilder,
    private services : SecurityService,
    private snackbar : MatSnackBar,
    private dialog : MatDialog
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    
    // Get the list of user groups from the server.
    // this.userGroups = this.services.getUserGroups().pipe(
    //   map(userGroups => userGroups as UserGroup[])
    // );


    // Check if the value of the UserGroupId field is not null.
    // if (this.userForm.get('userGroupId') !== null) {
    //   // Update the value of the userGroup property on the userForm object with the UserGroup object that has the matching ID.
    //   this.userGroups.subscribe(groups => {
    //     const selectedGroup = groups.find(userGroup => userGroup.userGroupId === this.userForm.get('userGroupId')!.value);
    //     this.userForm.patchValue({
    //       userGroup: selectedGroup
    //     });
    //   });
    // }

    // // Get the list of user groups from the server.
    // this.userGroups = this.services.getUserGroups().pipe(
    //   map(userGroups => userGroups as UserGroup[])
    // );


    // // Check if the value of the UserGroupId field is not null.
    // if (this.userForm.get('UserGroupId') !== null) {
    //   // Update the value of the userGroup property on the userForm object with the UserGroup object that has the matching ID.
    //   this.userForm.patchValue({
    //     userGroup: this.userGroups.find(userGroup => userGroup.userGroupId === this.userForm.get('UserGroupId')!.value)
    //   });
    // }
    this.dataSource = new MatTableDataSource<User>();
    this.loadUsers();
    this.services.getUserGroups().subscribe(response =>{
      this.colorarraylist = [];
      this.colorarraylist = response;
    });

    this.filteroptionslist = this.userForm.valueChanges.pipe(
      startWith(''), map(value => this._LISTFILTER(this.colorarraylist || ''))
    );

    // this.filteroptionslist = this.formcontrol.valueChanges.pipe(
    //   startWith(''), map(value => this._LISTFILTER(value || ''))
    // );
  }

  // _LISTFILTER(value: string): UserGroup[] {
  //   const searchvalue = value.toLocaleLowerCase();
  //   return this.colorarraylist.filter(option => option.userGroupName.toLocaleLowerCase().includes(searchvalue));
  // }


  // getUserGroup()
  // {
  //   this.services.getUserGroups().subscribe(userGroups => {
  //     this.userGroups =[];
  //     this.userGroups = userGroups;
  //     this.userForm.patchValue({userId : 0});
  //   })
  // }



      // this.employeeOptions = this.employeeEvaluationForm.get('empCode').valueChanges
    // .pipe(
    //   startWith(''),
    //   map(value => this._filter(this.employees, 'empCode', 'empName', value))
    // );
    // this.getEmployees();


    // onUserGroupInputChange(event: Event): void {
    //   const inputElement = event.target as HTMLInputElement;
    //   const filterValue = inputElement.value.toLowerCase();
    //   this.filteredUserGroups = of(this.filterUserGroups(filterValue));
    // }


  // ngOnInit(): void {
  //   this.initForm();
  //   this.loadUsers();
  //   this.userGroupList = this.services.getUserGroups();
    
  //   // this.filteredUserGroups = this.formcontrol.valueChanges.pipe(
  //   //   startWith(''), map(value => this._LISTFILTER(value || ''))
  //   // )
   
  //   // this.services.getUserGroups().subscribe((groups: UserGroup[]) => {
  //   //   this.userGroupList = groups;
  //   //   this.filteredUserGroups = this.userForm.get('userGroupId')!.valueChanges.pipe(
  //   //     startWith(''),
  //   //     map(name => typeof name === 'string' ? this.filterUserGroups(name) : this.userGroups.slice())
  //   //   );
  //   // });

  //   // this.services.getUserGroups().subscribe((groups: UserGroup[]) => {
  //   //   this.userGroupList = groups;
  //   //   this.setupFilteredUserGroups();
  //   // });

  //   // this.services.getUserGroups().subscribe((groups: UserGroup[]) => {
  //   //   this.userGroupList = groups;
  //   // });

  // //  // Set up filteredUserGroups using FormControl valueChanges
  // //  this.filteredUserGroups = this.userForm.get('userGroupId')!.valueChanges.pipe(
  // //   startWith(''),
  // //   map(value => typeof value === 'string' ? this.filterUserGroups(value) : this.userGroupList.slice())
  // // );

  //   // this.filteredUserGroups = this.userForm.get('userGroupId')!.valueChanges.pipe(
  //   //   startWith(''),
  //   //   map(value => typeof value === 'string' ? this.filterUserGroups(value) : this.userGroupList)
  //   // );

  //   // this.services.getUserGroups().subscribe((groups: UserGroup[]) => {
  //   //   this.userGroupList = groups;
  //   //   this.filteredUserGroups = this.userForm.get('userGroupId')!.valueChanges.pipe(
  //   //     startWith(''),
  //   //     map(name => typeof name === 'string' ? this.filterUserGroups(name) : this.userGroups)
  //   //   );
  //   // });
  // }

  // ngAfterViewInit(): void {
  //   // this.dataSource.paginator = this.paginator;
  //   // this.dataSource.sort = this.sort;
  // }

  // setupFilteredUserGroups() {
  //   this.filteredUserGroups = this.userForm.get('userGroupId')!.valueChanges.pipe(
  //     startWith(''),
  //     map(name => typeof name === 'string' ? this.filterUserGroups(name) : this.userGroups)
  //   );
  // }
  // filterUserGroups(name: string): UserGroup[] {
  //   const filterValue = name.toLowerCase();
  //   return this.userGroupList.filter(group =>
  //     group.userGroupName.toLowerCase().includes(filterValue)
  //   );
  // }


  // displayUserGroup(userGroup: UserGroup): string {
  //   return userGroup ? userGroup.userGroupName : '';
  // }

  // onUserGroupInputChange(event: Event): void {
  //   const inputElement = event.target as HTMLInputElement;
  //   const filterValue = inputElement.value.toLowerCase();
  //   this.filteredUserGroups = of(this.filterUserGroups(filterValue));
  // }

  // onUserGroupInputChange(event: Event): void {
  //   const inputElement = event.target as HTMLInputElement;
  //   const filterValue = inputElement.value.toLowerCase();
  //   this.filteredUserGroups = this.userGroupList.filter(group =>
  //     group.userGroupName.toLowerCase().includes(filterValue)
  //   );
  // }

  // private _LISTFILTER(value: string): UserGroup[] {
  //   const searchvalue = value.toLocaleLowerCase();
  //   return this.userGroupList.filter(option => option.userGroupName.toLocaleLowerCase().includes(searchvalue));
  // }




  // filterUserGroups(name: string): UserGroup[] {
  //   const filterValue = name.toLowerCase();
  //   return this.users.filter(group => group.userGroupName.toLowerCase().includes(filterValue));
  // }
  // filterUserGroups(name: string): UserGroup[] {
  //   const filterValue = name.toLowerCase();
  //   return this.userGroups.filter(group => group.userGroupName.toLowerCase().includes(filterValue));
  // }
  // filterUserGroups(name: string): UserGroup[] {
  //   const filterValue = name.toLowerCase();
  //   return this.userGroups.filter(group =>
  //     group.userGroupName.toLowerCase().includes(filterValue)
  //   );
  // }
  
  
  // displayUserGroup(userGroup: UserGroup): string {
  //   return userGroup ? userGroup.userGroupName : '';
  // }


  

  
  // insertUser() {
  //   if(this.userForm.valid)
  //   {
  //     const newUser : User = this.userForm.value;
  //     this.services.insertUser(newUser).subscribe(() => {
  //       this.loadUsers();
  //       this.resetForm();
  //       this.snackbar.open('User created successfully!', 'Dismiss',{
  //         duration : 2000
  //       });
  //     });
  //   } else{
  //     this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
  //       duration: 2000
  //     });
  //   }
  // }

  

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  _LISTFILTER(value: UserGroup[]) {
    
    const searchvalue = value.toString().toLowerCase();
    return this.colorarraylist.filter(option => option.userGroupName.toLowerCase().includes(searchvalue));
  }

  initForm() {
    this.userForm = this.formBuilder.group({
      userId: [0],
      loginName: ['', Validators.required],
      loginNameLocal: [''],
      password: [''],
      userGroupId: ['', Validators.required],
      startDate: null,
      endDate: null,
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  
  resetForm()
  {
    this.userForm.reset();
    this.userForm.patchValue({
      UserId : 0,
    });
  }


  onSubmit()
  {
    if (this.userForm.get('userId')!.value) {
      this.updateUser();
    } else {
      this.insertUser();
    }
  }
  
  insertUser() {
    if (this.userForm.valid) {
      const userIdValue = this.userForm.get('userId')?.value;
      if(userIdValue === null) this.userForm.patchValue({userId : 0});
      const newUser: User = this.userForm.value;
      console.log(newUser);
      this.services.insertUser(newUser).subscribe(() => {
        this.loadUsers(); // Refresh the user list after inserting a new user
        this.resetForm();
        this.snackbar.open('User created successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  // convertToAppDateFormat(dateString: string): string {
  //   // Assuming dateString is in "MM/DD/YYYY" format
  //   const dateObject = new Date(dateString);
  //   return formatDate(dateObject, 'yyyy-MM-dd', 'en-US');
  // }

  onEditUser(row: User) {
    if (row.userId) {
      this.userForm.patchValue({
        ...row,
        startDate: row.startDate, 
        endDate: row.endDate,
      });
    } else {
      this.resetForm();
    }
  }

  // updateUser() {
  //   if(this.userForm.valid){
  //     const updateUser : User = this.userForm.value;
  //     this.services.updateUser(updateUser).subscribe(() => {
  //       this.loadUsers();
  //       this.userForm.reset();
  //       // this.resetForm();
  //       this.snackbar.open('User updated successfully!', 'Dismiss', {
  //         duration: 2000
  //       });
  //     });
  //   }else{
  //     this.snackbar.open('Please fill the required fields!', 'Dismiss', {
  //       duration : 2000
  //     });
  //   }
  // }

  updateUser() {
    if (this.userForm.valid) {
      const updateUser: User = this.userForm.value;
      this.services.updateUser(updateUser).subscribe(() => {
        this.loadUsers(); // Refresh the user list after updating a user
        this.userForm.reset();
        this.snackbar.open('User updated successfully!', 'Dismiss', {
          duration: 2000,
        });
      });
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  // loadUsers()
  // {
  //   this.services.getUsers().subscribe(
  //     (response : User[]) => {
  //       console.log(response);
  //       this.users = response;
  //       console.log(this.dataSource);
  //       //this.dataSource = new MatTableDataSource<User>(this.users);
  //       this.dataSource.data = this.users;
  //       this.isLoadingResults = false;
  //     },
  //     (error) => {
  //       console.log(error);
  //       this.isLoadingResults = false;
  //       this.isRateLimitReached = true;
  //     }
  //   );
  // }

  // loadUsers() {
  //   this.services.getUsers().subscribe(
  //     (response: User[]) => {
  //       this.users = response;
  //       this.dataSource = new MatTableDataSource<User>(this.users); // Create a new instance
  //       console.log(this.dataSource.data); // Optional: Verify the data is set correctly
  //       this.dataSource.paginator = this.paginator; // Set the paginator after initializing the data source
  //       this.dataSource.sort = this.sort; // Set the sort after initializing the data source
  //       this.isLoadingResults = false;
  //     },
  //     (error) => {
  //       console.error(error);
  //       this.isLoadingResults = false;
  //       this.isRateLimitReached = true;
  //     }
  //   );
  // }

  loadUsers() {
    this.usersSubscription = this.services.getUsers().subscribe({
      next: (response: User[]) => {
        this.users = response;
        this.dataSource = new MatTableDataSource<User>(this.users);
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

  ngOnDestroy() {
    // Unsubscribe from the observable to prevent memory leaks
    if (this.usersSubscription) {
      this.usersSubscription.unsubscribe();
    }
  }


  confirmationRowDelete(user: User) {
    const dialogRef = this.dialog.open(this.confirmationDelete, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        // User clicked on "Delete" in the dialog
        this.deleteUser(user);
      }
    });
  }

  private deleteUser(user: User) {
    this.services.deleteUser(user.userId).subscribe(() => {
      this.loadUsers();
      this.snackbar.open('User group deleted successfully!', 'Dismiss', {
        duration: 3000,
      });
    });
  }

  userDetail(userId: number) {
    this.services.getUserByUserId(userId)
      .pipe(
        tap((userGroupData) => {
          const dialogRef = this.dialog.open(this.UserDetailDialog, {
            width: '100%',
            data: userGroupData,
          });
        }),
        catchError((error) => {
          console.log(error);
          // Handle the error here if needed
          // You can return a default value or re-throw the error
          // if you want to propagate it further.
          return [];
        })
      )
      .subscribe();
  }

  

  // userDetail(userId: number) {
  //   this.services.getUserByUserId(userId).subscribe(
  //     (userData) => {
  //         this.dialog.open(this.UserDetailDialog, {
  //         width: '800px',
  //         data: userData,
  //       });
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }

}
