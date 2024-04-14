import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { MainMenu } from '../shared/models/mainMenu';
import { SubMenu } from '../shared/models/subMenu';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  baseUrl = 'https://localhost:7110/pberp/';

  constructor(private http: HttpClient) { }

  // MainMenu from CompanylinkModule
  getMainMenu()
  {
    // return this.http.get<Pagination<MainMenu[]>>(this.baseUrl + "layout/modules?companyId=1")
    return this.http.get<MainMenu[]>(this.baseUrl + "layout/modules?companyId=1")
  }

  // SubMenu from SecDScreen
  getSubMenu()
  {
    return this.http.get<SubMenu[]>(this.baseUrl + "layout/screens?moduleId=1")
  }

  // MainMenu from CompanylinkModule by company id
  getMainMenuByCompanyId(companyId : number)
  {
    return this.http.get<MainMenu[]>(this.baseUrl + `layout/modules?companyId=${companyId}`)
  }

  // SubMenu from SecDScreen using by module id
  getSubMenuByModuleId(moduleId: number)
  {
    // return this.http.get<SubMenu[]>(`YOUR_API_ENDPOINT?moduleId=${moduleId}`);
    return this.http.get<SubMenu[]>(this.baseUrl + `layout/screens?moduleId=${moduleId}`)
  }
}
