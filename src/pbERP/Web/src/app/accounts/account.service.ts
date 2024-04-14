import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { EduABuildingInfoToReturn } from '../shared/models/eduABuildingInfoToReturn';
import { EduABuildingParams } from '../shared/models/eduABuildingParams';
import { EduABuilding } from '../shared/models/eudABuilding';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:7110/pberp/';

  constructor(private http: HttpClient) { }

  getEduABuilding(eduParams : EduABuildingParams)
  {
    let params = new HttpParams();

    if(eduParams.companyId > 0) params = params.append('companyId', eduParams.companyId);
    params = params.append('sort', eduParams.search);
    params = params.append('pageIndex', eduParams.pageNumber);
    params = params.append('pageSize', eduParams.pageSize);

    if(eduParams.search) params = params.append('search', eduParams.search)

    return this.http.get<Pagination<EduABuildingInfoToReturn[]>>(this.baseUrl + 'eduA-buildings', {params});
  }

  getEduABuildingsWithoutParams() {
    return this.http.get<Pagination<EduABuilding[]>>(this.baseUrl + 'eduB-buildings-without-params');
  }


}
