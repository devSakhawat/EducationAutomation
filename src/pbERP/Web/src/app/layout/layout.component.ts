import { Component, OnInit } from '@angular/core';
import { MainMenu } from 'src/app/shared/models/mainMenu';
import { SubMenu } from 'src/app/shared/models/subMenu';
import { LayoutService } from './layout.service';
import { AccountComponent  } from 'src/app/accounts/account.component';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit{

  mainMenus : MainMenu[] = [];
  subMenus : SubMenu[] = [];

  /**
   *
   */
  constructor(private layoutService : LayoutService){}

  ngOnInit(): void {
    this.getMainMenu();
  }

  // main menu by companyId and userID
  // getMainMenu()
  // {
  //   this.layoutService.getMainMenu().subscribe(
  //     response =>{this.mainMenus = response;},
  //     error =>{console.log(error)}
  //   );
  // }

  // main menu by companyId
  getMainMenu()
  {
    this.layoutService.getMainMenuByCompanyId().subscribe(
      response =>{this.mainMenus = response; console.log(response)},
      error =>{console.log(error)}
    );
  }

  // sub menu by moduleId
  getSubMenu(parentId : number)
  {
    this.layoutService.getSubMenuByParentId(parentId).subscribe(
      response =>{this.subMenus = response; console.log(response)},
      error => {console.log(error)}
    );
  }
}
