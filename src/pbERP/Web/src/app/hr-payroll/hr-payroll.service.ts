import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HrHBloodGroup } from '../shared/models/hrHBloodGroup';

@Injectable({
  providedIn: 'root'
})

export class HrPayrollService {
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getBloodGroupByBloodGroupId( BloodGroupId : number)
  {
    return this.http.get<HrHBloodGroup[]>(this.baseUrl + `hr/blood-group/key?key=${BloodGroupId}`)
  }
  
  // Get Students
  getBloodGroups()
  {
    return this.http.get<HrHBloodGroup[]>(this.baseUrl + 'hr/blood-groups' );
  }
}
