import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { EduABuildingInfoToReturn } from '../shared/models/eduABuildingInfoToReturn';
import { AccountService } from './account.service';
import { EduABuildingParams } from '../shared/models/eduABuildingParams';
import { MatTableDataSource } from '@angular/material/table';
import { EduABuilding } from '../shared/models/eudABuilding';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';



@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})

export class AccountComponent implements OnInit {
  displayedColumns: string[] = ['buildingId', 'buildingNameEnglish', 'buildingNameLocal', 'usesType', 'companyName'];
  dataSource!: MatTableDataSource<EduABuilding>;
  posts: any;

  @ViewChild('search') searchTerm?: ElementRef;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;



  eduABuildings: EduABuildingInfoToReturn[] = [];
  eduParams = new EduABuildingParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'UsesTypeAsc', value: 'userTypeAsc' },
    { name: 'UsesTypeDesc', value: 'userTypeDesc' }
  ];

  totalCount = 0;

  constructor(private accountService: AccountService) { }
  // ngAfterViewInit() {
  //   this.dataSource.paginator = this.paginator;
  // }

  ngOnInit(): void {
    //this.getEduABuilding();
    this.eduABuildingWithoutParams();
  }

  eduABuildingWithoutParams()
  {
    this.accountService.getEduABuildingsWithoutParams().subscribe({
      next: response => {
        this.totalCount = response.count;
        this.posts = response.data;
        console.log(this.posts);
        this.dataSource = new MatTableDataSource(this.posts);
        console.log(this.dataSource)
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.dataSource);
      },
      error : error => console.log(error)
    });
  }


  // getEduABuilding() {
  //   this.accountService.getEduABuilding(this.eduParams).subscribe({
  //     next: response => {
  //       this.eduABuildings = response.data;
  //       this.eduParams.pageNumber = response.pageIndex;
  //       this.eduParams.pageSize = response.pageSize;
  //       this.totalCount = response.count;
  //     },
  //     error: error => console.log(error)
  //   })
  // }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getEduABuilding() {
    this.accountService.getEduABuilding(this.eduParams).subscribe({
      next: response => {
        this.eduParams.pageNumber = response.pageIndex;
        this.eduParams.pageSize = response.pageSize;
        this.totalCount = response.count;

        this.posts = response.data;
        this.dataSource = new MatTableDataSource(this.posts);

        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        console.log(this.dataSource);
      },
      error: error => console.log(error)
    })
  }




  onCompanySelected(companyId : number)
  {
    this.eduParams.companyId = companyId;
    this.eduParams.pageNumber = 1;
    this.getEduABuilding();
  }

  onSortSelected(event: any)
  {
    this.eduParams.sort = event.target.value;
    this.getEduABuilding();
  }

  onPageChanged(event: any)
  {
    if(this.eduParams.pageNumber !== event)
    {
      this.eduParams.pageNumber = event;
      this.getEduABuilding();
    }
  }

  OnSearch(){
    this.eduParams.search = this.searchTerm?.nativeElement.value;
    // this.eduParams.pageNumber = 1;
    this.getEduABuilding();
  }

  onReset()
  {
    if(this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.eduParams = new EduABuildingParams();
    this.getEduABuilding();
  }

}
function next(value: EduABuilding[]): void {
  throw new Error('Function not implemented.');
}

