import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MainMenu } from '../shared/models/mainMenu';
import { SubMenu } from '../shared/models/subMenu';

@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  baseUrl = 'https://localhost:7110/pberp/';

  constructor(private http: HttpClient) { }

  // MainMenu from CompanylinkModule
  getMainMenuByCompanyId()
  {
    // return this.http.get<Pagination<MainMenu[]>>(this.baseUrl + "layout/modules?companyId=1")
    return this.http.get<MainMenu[]>(this.baseUrl + "layout/modules?companyId=1&userId=1")
  }

  // SubMenu from SecDScreen
  getSubMenu()
  {
    return this.http.get<SubMenu[]>(this.baseUrl + "layout/screens?moduleId=1")
  }

  // MainMenu from secDScreen by companyId and userId
  getMainMenu(companyId : number, userId : number)
  {
    return this.http.get<MainMenu[]>(this.baseUrl + `layout/modules?companyId=${companyId}&userId=${userId}`)
  }

  // SubMenu from SecDScreen using by module id
  getSubMenuByParentId(parentId: number)
  {
    // return this.http.get<SubMenu[]>(`YOUR_API_ENDPOINT?moduleId=${moduleId}`);
    return this.http.get<SubMenu[]>(this.baseUrl + `layout/screens?parentId=${parentId}`)
  }
}
