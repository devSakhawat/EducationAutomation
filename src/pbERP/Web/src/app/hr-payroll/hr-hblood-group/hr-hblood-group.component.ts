import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { HrPayrollService } from '../hr-payroll.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { HrHBloodGroup } from 'src/app/shared/models/hrHBloodGroup';

@Component({
  selector: 'app-hr-hblood-group',
  templateUrl: './hr-hblood-group.component.html',
  styleUrls: ['./hr-hblood-group.component.css']
})
export class HrHbloodGroupComponent implements AfterViewInit, OnInit {
  displayedColumns : string[] = [
    'BloodGroupId', 'BloodGroupNameEnglish', 'BloodGroupNameLocal', 'actions'
  ];

  // dataSource! : MatTableDataSource<HrHBloodGroup>;
  // isLoadingResults = true;
  // isRateLimitReached = false;
  // bloodGroupForm : HrHBloodGroup = {} as HrHBloodGroup;
  // selectedBloodGroup : HrHBloodGroup = {} as HrHBloodGroup;


  dataSource!: MatTableDataSource<HrHBloodGroup>;
  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;
  bloodGroupForm: HrHBloodGroup = {} as HrHBloodGroup;
  selectedBloodGroup: HrHBloodGroup = {} as HrHBloodGroup;


  @ViewChild(MatPaginator, { static : true }) paginator! : MatPaginator;
  @ViewChild(MatSort) sort! : MatSort;
  @ViewChild('BloodGrouppDialog') BloodGrouppDialog!: TemplateRef<any>;
  
  constructor(
    private bloodGroupService: HrPayrollService,
    private snackBar : MatSnackBar,
    private dialog : MatDialog 
    ) {  }

    
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<HrHBloodGroup>();
    this.  getBloodGroup();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getBloodGroup()
  {
    // debugger;
    this.bloodGroupService.getBloodGroups().subscribe(
      (response : HrHBloodGroup[]) => {
        this.dataSource.data = response;
        this.resultsLength = response.length;
        this.isLoadingResults = false;
        console.log(this.dataSource.data);
      },
      (error) => {
        console.log(error);
        this.isLoadingResults = false;
        this.isRateLimitReached = true;
      }
    );
  }

  applyFilter(event: Event)
  {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if(this.dataSource.paginator)
    {
      this.dataSource.paginator.firstPage();
    }
  }

  editBloodGroup(bloodGroup : HrHBloodGroup): void{
    this.selectedBloodGroup = { ...bloodGroup };
  }

  detailBloodGroup(bloodGroup : HrHBloodGroup): void{
    this.selectedBloodGroup = { ...bloodGroup }
  }

  deleteBloodGroup(bloodGroup: HrHBloodGroup) : void{
    this.selectedBloodGroup = { ...bloodGroup }
  }
}
