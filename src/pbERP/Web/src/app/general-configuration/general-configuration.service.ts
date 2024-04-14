import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Module } from '../shared/models/GeneralConfiguration/module';
import { Observable } from 'rxjs';
import { GenConfigACountry } from '../shared/models/GeneralConfiguration/genConfigACountry';
import { genConfigBDivisionOrState } from '../shared/models/GeneralConfiguration/genConfigBDivisionOrState';
import { GenConfigCDistrictOrCity } from '../shared/models/GeneralConfiguration/genConfigCDistrictOrCity';
import { GenConfigDPoliceStation } from '../shared/models/GeneralConfiguration/GenConfigDPoliceStation';
import { GenConfigEGender } from '../shared/models/GeneralConfiguration/genConfigEGender';
import { GenConfigFBloodGroup } from '../shared/models/GeneralConfiguration/genConfigFBloodGroup';
import { GenConfigGReligion } from '../shared/models/GeneralConfiguration/GenConfigGReligion';

@Injectable({
  providedIn: 'root'
})
export class GeneralConfigurationService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  //#endregion SEC B SecDScreen

  //#region Gen Config A Country
  getCountries(): Observable<GenConfigACountry[]> {
    return this.http.get<GenConfigACountry[]>(this.baseUrl + 'gen-config/countries');
  }

  getByCountryId(countryId: number) {
    return this.http.get<GenConfigACountry>(this.baseUrl + `gen-config/country/key/${countryId}`);
  }

  insertCountry(country: GenConfigACountry): Observable<GenConfigACountry> {
    return this.http.post<GenConfigACountry>(this.baseUrl + 'gen-config/country', country);
  }

  updateCountry(country: GenConfigACountry) {
    return this.http.put<GenConfigACountry>(`${this.baseUrl}gen-config/country/${country.countryId}`, country);
  }

  deleteCountry(countryId: number): Observable<GenConfigACountry> {
    return this.http.delete<GenConfigACountry>(`${this.baseUrl}gen-config/country/${countryId}`);
  }
  //#endregion Gen Config A Country

  //#region Gen Config B Division or State
  getDivisionsOrStates() : Observable<genConfigBDivisionOrState[]>
  {
    return this.http.get<genConfigBDivisionOrState[]>(this.baseUrl + 'gen-config/division-states');
  }

  getByDivisionOrStateId(divisionId : number)
  {
    return this.http.get<genConfigBDivisionOrState>(`${this.baseUrl}gen-config/division-state/key/${divisionId}`);
  }

  insertDivisionOrState(divisionOrState : genConfigBDivisionOrState)
  {
    return this.http.post<genConfigBDivisionOrState>(`${this.baseUrl}gen-config/division-state`, divisionOrState);
  }

  updateDivisionOrState(divisionOrState : genConfigBDivisionOrState)
  {
    return this.http.put<genConfigBDivisionOrState>(`${this.baseUrl}gen-config/division-state/${divisionOrState.divisionId}`, divisionOrState);
  }

  deleteDivisionOrState(divisionId : number)
  {
    return this.http.delete<genConfigBDivisionOrState>(`${this.baseUrl}gen-config/division-state/${divisionId}`);
  }
  //#endregion Gen Config B Division or State

  //#region Gen Config C District or City
  getDistrictsOrCities() : Observable<GenConfigCDistrictOrCity[]>
  {
    return this.http.get<GenConfigCDistrictOrCity[]>(this.baseUrl + 'gen-config/districts-cities');
  }

  getByDistrictOrCityId(districtId : number)
  {
    return this.http.get<GenConfigCDistrictOrCity>(`${this.baseUrl}gen-config/district-city/key/${districtId}`);
  }

  insertDistrictOrCity(districtOrCity : GenConfigCDistrictOrCity)
  {
    return this.http.post<GenConfigCDistrictOrCity>(`${this.baseUrl}gen-config/district-city`, districtOrCity);
  }

  updateDistrictOrCity(districtOrCity : GenConfigCDistrictOrCity)
  {
    return this.http.put<GenConfigCDistrictOrCity>(`${this.baseUrl}gen-config/district-city/${districtOrCity.districtId}`, districtOrCity);
  }

  deleteDistrictOrCity(districtId : number)
  {
    return this.http.delete<GenConfigCDistrictOrCity>(`${this.baseUrl}gen-config/district-city/${districtId}`);
  }
  //#endregion Gen Config C District or City

  //#region Gen Config D Police Station
  getPoliceStations() : Observable<GenConfigDPoliceStation[]>
  {
    return this.http.get<GenConfigDPoliceStation[]>(this.baseUrl + 'gen-config/police-stations');
  }

  getByPoliceStationId(policeStationId : number)
  {
    return this.http.get<GenConfigDPoliceStation>(`${this.baseUrl}gen-config/police-station/key/${policeStationId}`);
  }

  insertPoliceStation(policeStation : GenConfigDPoliceStation)
  {
    debugger;
    return this.http.post<GenConfigDPoliceStation>(`${this.baseUrl}gen-config/police-station`, policeStation);
  }

  updatePoliceStation(policeStation : GenConfigDPoliceStation)
  {
    return this.http.put<GenConfigDPoliceStation>(`${this.baseUrl}gen-config/police-station/${policeStation.policeStationId}`, policeStation);
  }

  deletePoliceStation(policeStationId : number)
  {
    return this.http.delete<GenConfigDPoliceStation>(`${this.baseUrl}gen-config/police-station/${policeStationId}`);
  }
  //#endregion Gen Config C District or City

  //#region Gen Config E Gender
  getGenders() : Observable<GenConfigEGender[]>
  {
    return this.http.get<GenConfigEGender[]>(this.baseUrl + 'gen-config/genders');
  }

  getByGenderId(genderId : number)
  {
    return this.http.get<GenConfigEGender>(`${this.baseUrl}gen-config/gender/key/${genderId}`);
  }

  insertGender(gender : GenConfigEGender)
  {
    return this.http.post<GenConfigEGender>(`${this.baseUrl}gen-config/gender`, gender);
  }

  updateGender(gender : GenConfigEGender)
  {
    return this.http.put<GenConfigEGender>(`${this.baseUrl}gen-config/gender/${gender.genderId}`, gender);
  }

  deleteGender(genderId : number)
  {
    return this.http.delete<GenConfigEGender>(`${this.baseUrl}gen-config/gender/${genderId}`);
  }
  //#endregion Gen Config E Gender

  //#region Gen Config D Police Station

  getBloodGroups() : Observable<GenConfigFBloodGroup[]>
  {
    return this.http.get<GenConfigFBloodGroup[]>(this.baseUrl + 'gen-config/blood-groups');
  }

  getByBloodGroupId(bloodGroupId : number)
  {
    return this.http.get<GenConfigFBloodGroup>(`${this.baseUrl}gen-config/blood-group/key/${bloodGroupId}`);
  }

  insertBloodGroup(bloodGroup : GenConfigFBloodGroup)
  {
    return this.http.post<GenConfigFBloodGroup>(`${this.baseUrl}gen-config/blood-group`, bloodGroup);
  }

  updateBloodGroup(bloodGroup : GenConfigFBloodGroup)
  {
    return this.http.put<GenConfigFBloodGroup>(`${this.baseUrl}gen-config/blood-group/${bloodGroup.bloodGroupId}`, bloodGroup);
  }

  deleteBloodGroup(bloodGroupId : number)
  {
    return this.http.delete<GenConfigFBloodGroup>(`${this.baseUrl}gen-config/blood-group/${bloodGroupId}`);
  }
  //#endregion Gen Config E Gender

  //#region Gen Config D Police Station
  getReligions() : Observable<GenConfigGReligion[]>
  {
    return this.http.get<GenConfigGReligion[]>(this.baseUrl + 'gen-config/religions');
  }

  getByReligionId(religionId : number)
  {
    return this.http.get<GenConfigGReligion>(`${this.baseUrl}gen-config/religion/key/${religionId}`);
  }

  insertReligion(religion : GenConfigGReligion)
  {
    return this.http.post<GenConfigGReligion>(`${this.baseUrl}gen-config/religion`, religion);
  }

  updateReligion(religion : GenConfigGReligion)
  {
    return this.http.put<GenConfigGReligion>(`${this.baseUrl}gen-config/religion/${religion.religionId}`, religion);
  }

  deleteReligion(religionId : number)
  {
    return this.http.delete<GenConfigGReligion>(`${this.baseUrl}gen-config/religion/${religionId}`);
  }
  //#endregion Gen Config E Gender







  //Component: Module
  getModules(): Observable<Module[]> {
    return this.http.get<Module[]>(this.baseUrl + 'gen/modules');
  }






}
