import { Component, OnInit } from '@angular/core'
import { MainMenu } from './shared/models/mainMenu';
import { SubMenu } from './shared/models/subMenu';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscription, switchMap, tap } from 'rxjs';
import { AppService } from './app.service';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:7110/pberp/';
  // sidebarCollapsed = false;
  handleAllMenus: MainMenu[] = [];
  mainMenus: MainMenu[] = [];
  allMenus: MainMenu[] = [];
  subMenus: SubMenu[] = [];
  nestedSubMenus: SubMenu[] = [];
  isLoadingResults = true;
  isRateLimitReached = false;

  showFiller = false;
  showStatistics: boolean = false;

  private manusSubscription: Subscription | undefined;
  /**
   *
   */
  constructor(
    private http: HttpClient,
    private services: AppService

  ) { }

  ngOnInit(): void {
    // this.handleManus();
    this.onLoad();
  }

  onLoad() {
    this.manusSubscription = this.services.getManus().subscribe({
      next: (response: MainMenu[]) => {
        this.allMenus = [];
        this.allMenus = response;
        this.mainMenus = [];
        this.mainMenus = this.allMenus.filter(x => Number(x.parentId) === 0);
        this.isLoadingResults = false;

        console.log(this.mainMenus);
      },
      error: (error) => {
        console.log(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  // SubMenu from secDScreen by screenId (after login)
  getSubMenus(screenId: number) {
    debugger;
    this.subMenus = [];
    this.subMenus = this.allMenus.filter(sm => sm.parentId === screenId && sm.parentId !== 0);
    console.log(this.subMenus)
    // this.http.get<SubMenu[]>(this.baseUrl + `layout/screens?parentId=${screenId}`).subscribe({
    //   next : res => {this.subMenus = res; console.log(this.subMenus)},
    //   error : err => console.log(err)
    // });
  }

    // Nested SubMenu from secDScreen by screenId (after login)
    getNestedSubMenus(screenId: number) {
      debugger;
      // this.nestedSubMenus = [];
      this.nestedSubMenus = this.allMenus.filter(x => x.parentId === screenId && x.parentId !== 0);
      console.log(this.nestedSubMenus)
      // console.log(this.nestedSubMenus.map( x => x.screenName.trim().toLowerCase().split(' ').join('')));
      // this.http.get<SubMenu[]>(this.baseUrl + `layout/screens?parentId=${screenId}`).subscribe({
      //   next : res => {this.nestedSubMenus = res; console.log(this.nestedSubMenus)},
      //   error : err => console.log(err)
      // });
    }


  sidebarCollapsed = false;
  toggleSidebar() {
    this.sidebarCollapsed = !this.sidebarCollapsed;
  }

  // // Handle Manus
  // handleManus() {
  //   this.http.get<MainMenu[]>(this.baseUrl + 'layout/modules?companyId=1&userId=1').pipe(
  //     tap(response => {
  //       this.handleAllMenus = [];
  //       this.handleAllMenus = response;
  //       this.isLoadingResults = false;
  //       // console.log(response);
  //       console.log(this.handleAllMenus);
  //     },
  //       (error) => {
  //         console.log(error);
  //         this.isLoadingResults = false;
  //         this.isRateLimitReached = true;
  //       }
  //     ),
  //     switchMap(async () => this.getMainMenus())
  //   ).subscribe();
  // }

  // MainMenu Without Login
  // getMainMenus() {
  //   // console.log(this.handleAllMenus)
  //   this.mainMenus = [];
  //   this.mainMenus = this.handleAllMenus.filter(mm => mm.parentId === 0);
  //   console.log(this.mainMenus)
  // }

  // // SubMenu from secDScreen by screenId (after login)
  // getSubMenus(screenId : number)
  // {
  //   this.subMenus = [];
  //   this.subMenus = this.handleAllMenus.filter(sm => sm.parentId === screenId && sm.parentId !== 0);
  //   console.log(this.subMenus)
  //   // this.http.get<SubMenu[]>(this.baseUrl + `layout/screens?parentId=${screenId}`).subscribe({
  //   //   next : res => {this.subMenus = res; console.log(this.subMenus)},
  //   //   error : err => console.log(err)
  //   // });
  // }

  // // Nested SubMenu from secDScreen by screenId (after login)
  // getNestedSubMenus(screenId: number) {
  //   this.nestedSubMenus = [];
  //   this.nestedSubMenus = this.handleAllMenus.filter(x => x.parentId === screenId && x.parentId !== 0);
  //   console.log(this.nestedSubMenus)
  //   // console.log(this.nestedSubMenus.map( x => x.screenName.trim().toLowerCase().split(' ').join('')));
  //   // this.http.get<SubMenu[]>(this.baseUrl + `layout/screens?parentId=${screenId}`).subscribe({
  //   //   next : res => {this.nestedSubMenus = res; console.log(this.nestedSubMenus)},
  //   //   error : err => console.log(err)
  //   // });
  // }





  // Handle Manus
  // // MainMenu from secDScreen by companyId and userId (when click login button)
  // getMainMenus()
  // {
  //   this.http.get<MainMenu[]>(this.baseUrl + 'layout/modules?companyId=1&userId=1').subscribe({
  //     next: response => this.mainMenus = response,
  //     error: error => console.log(error),
  //     complete: () => {
  //       console.log('request completed');
  //     }
  //   });
  // }

}

// eduABuildings : EduABuildingInfoToReturn[] = [];

// constructor(private http : HttpClient){}

// ngOnInit(): void {
//   this.http.get('https://localhost:7110/pberp/eduA-buildings').subscribe(
//   (response: Pagination): void=>
//   {
//     this.eduABuildings = response.data;
//   },error =>{
//     console.log(error);
//   });

// this.http.get<Pagination<EduABuildingInfoToReturn[]>>('https://localhost:7110/pberp/eduA-buildings').subscribe({
//   next: response => this.eduABuildings = response.data, // what to do next
//   error: error => console.log(error), // what to do if there is an error
//   complete: () => {
//     console.log('request completed');
//     console.log('extra statment');
//   }
// })
// }