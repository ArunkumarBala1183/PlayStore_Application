import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

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
    const headers = new HttpHeaders({
      'Content-Type' : 'application/json'
    });

    return this.http.get(this.baseUrl + "AppData/GetAppData/" + appId, {
      headers : headers,
      responseType: 'blob'
    });
  }

  
}
