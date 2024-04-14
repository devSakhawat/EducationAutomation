import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ACompany } from '../shared/models/Company/ACompany';
import { DTransport } from '../shared/models/Company/DTransport';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  baseUrl = environment.apiUrl + "company/";

  constructor(private http : HttpClient) { }

  //#region ACompany
  // insert record
  insertCompany(compnay : FormData) : Observable<ACompany>{
    return this.http.post<ACompany>(this.baseUrl + "compnay", compnay);
  }

  // Get records
  getCompanies() : Observable<ACompany[]>{
    return this.http.get<ACompany[]>(this.baseUrl + "companies");
  }
  //#endregion ACompany

  
  
  //#region DTransport services
  // insert DTransport
  insertTransport(transport: DTransport): Observable<DTransport> {
    
    return this.http.post<DTransport>(this.baseUrl + "transport", transport);
  }
  // Get By DTransportes
  getTransports(): Observable<DTransport[]> {
    return this.http.get<DTransport[]>(this.baseUrl + 'transports');
  }
  // Get By DTransporteId
  getByTransportId(transportId : number)
  {
    return this.http.get<DTransport>(this.baseUrl + `transport/key/${transportId}`);
  }
  // Update DTransport
  updateTransport(transportId: number, transport: DTransport): Observable<DTransport> {
    
    return this.http.put<DTransport>(`${this.baseUrl}transport/${transportId}`, transport);
  }
  // Delete DTransport
  deleteTransport(transportId: number): Observable<DTransport> {
    return this.http.delete<DTransport>(`${this.baseUrl}transport/${transportId}`);
  }
  //#endregion  DTransport services

}
