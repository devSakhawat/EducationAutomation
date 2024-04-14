import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { MainMenu } from './shared/models/mainMenu';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getManus(): Observable<MainMenu[]>{
    return this.http.get<MainMenu[]>(this.baseUrl + 'layout/modules?companyId=1&userId=1');
     
  }

}
