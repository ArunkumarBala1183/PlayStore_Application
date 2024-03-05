import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LogFilter } from '../interface/log-filter';


@Injectable({
  providedIn: 'root'
})
export class AppInfoService {

  private baseUrl : string = environment.apiBaseAddress;

  constructor(private http : HttpClient) { }

  GetAllApps()
  {
      return this.http.get(this.baseUrl + "AppInfo/GetAllApps" , {observe: "response"});
  }

  getAppData(appId : string)
  {
    return this.http.get(this.baseUrl + "AppData/GetAppData/" + appId, {
      responseType: 'blob'
    });
  }

  getAppLogs(filterDetails : LogFilter)
  {
      return this.http.post(this.baseUrl + "AppInfo/GetAppLogs" , filterDetails , {observe : "response"})
  }
  
  
}
