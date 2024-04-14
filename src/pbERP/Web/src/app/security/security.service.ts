import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { UserGroup } from '../shared/models/security/userGroup'
import { User } from '../shared/models/security/user'
import { SecDScreen } from "../shared/models/security/secDScreen";
import { LinkUserGroupScreen } from "../shared/models/security/linkUserGroupScreen";


@Injectable({providedIn : 'root'})

export class SecurityService{
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }
//#region Sec A UserGroup
  insertSecAUserGroup(userGroup : UserGroup): Observable<UserGroup>{
    console.log(userGroup);
    return this.http.post<UserGroup>(this.baseUrl + 'sec/user-group', userGroup);
  }

  getUserGroups() : Observable<UserGroup[]>
  {
    return this.http.get<UserGroup[]>(this.baseUrl + 'sec/user-groups');
  }

  getUserGroupsAutoComplete()
  {
    return this.http.get<UserGroup[]>(this.baseUrl + 'sec/user-groups');
  }

  getUserGroupByUserGroupId(userGroupId : number)
  {
    // return this.http.get<UserGroup[]>(this.baseUrl + `sec/user-group/key?key=${userGroupId}`);
    return this.http.get<UserGroup[]>(this.baseUrl + `sec/user-group/key/${userGroupId}`);
  }

  updateUserGroup(userGroup : UserGroup)
  {

    return this.http.put<UserGroup>(`${this.baseUrl}sec/user-group/${userGroup.userGroupId}`, userGroup);
  }

  deleteUserGroup(userGroupId: number): Observable<UserGroup> {
    return this.http.delete<UserGroup>(`${this.baseUrl}sec/user-group/${userGroupId}`);
  }

//#endregion
  
  //#region SEC B User
  insertUser(user : User) : Observable<User>
  {
    return this.http.post<User>(this.baseUrl + 'sec/user', user);
  }

  getUsers()
  {
    return this.http.get<User[]>(this.baseUrl + 'sec/users');
  }

  getUserByUserId(userId : number)
  {
    return this.http.get<User[]>(this.baseUrl + `sec/user/key/${userId}`);
  }

  updateUser(user : User)
  {
    return this.http.put<User>(`${this.baseUrl}sec/user/${user.userId}`, user);
  }

  deleteUser(userId : number) : Observable<User>{
    return this.http.delete<User>(`${this.baseUrl}sec/user/${userId}`);
  }
//#endregion SEC B User

  //#region  SEC D SCREEN
  insertScreen(screen : SecDScreen) : Observable<SecDScreen>
  {
    return this.http.post<SecDScreen>(this.baseUrl + 'sec/screen', screen);
  }

  getScreens()
  {
    return this.http.get<SecDScreen[]>(this.baseUrl + 'sec/screens');
  }

  getScreenByScreenId(screenId : number)
  {
    return this.http.get<SecDScreen[]>(this.baseUrl + `sec/screen/key/${screenId}`);
  }

  updateScreen(screen : SecDScreen)
  {
    return this.http.put<SecDScreen>(`${this.baseUrl}sec/screen/${screen.screenId}`, screen);
  }

  deleteScreen(screenId : number) : Observable<SecDScreen>{
    return this.http.delete<SecDScreen>(`${this.baseUrl}sec/screen/${screenId}`);
  }
//#endregion SEC B SecDScreen

//#region Link-User-Group-Screen
  getLinkUserGroupScreens()
  {
    return this.http.get<LinkUserGroupScreen[]>(this.baseUrl + 'sec/link-user-group-screens');
  }

  getLinkUserGroupScreensById(linkUserGroupScreenId : number)
  {
    return this.http.get<LinkUserGroupScreen>(`${this.baseUrl}sec/link-user-group-screen/key/${linkUserGroupScreenId}`);
  }

  insertLinkUserGroupScreen(linkUserGroupScreen : LinkUserGroupScreen) : Observable<LinkUserGroupScreen>
  {
    return this.http.post<LinkUserGroupScreen>(this.baseUrl + 'sec/link-user-group-screen', linkUserGroupScreen);
  }

  updateLinkUserGroupScreen(linkUserGroupScreen : LinkUserGroupScreen)
  {
    return this.http.put<LinkUserGroupScreen>(`${this.baseUrl}sec/link-user-group-screen/${linkUserGroupScreen.linkUserGroupScreenId}`, linkUserGroupScreen)
  }

  batchInsertLinkUserGroupScreen(data: LinkUserGroupScreen[]): Observable<LinkUserGroupScreen[]>{
    return this.http.post<LinkUserGroupScreen[]>(`${this.baseUrl}sec/batch-link-user-group-screen`, data);
  }

  deleteLinkUserGroupScreen(linkUserGroupScreenId : number)
  {
    return this.http.delete<LinkUserGroupScreen>(`${this.baseUrl}sec/link-user-group-screen/${linkUserGroupScreenId}`);
  }
//#endregion Link-User-Group-Screen

}