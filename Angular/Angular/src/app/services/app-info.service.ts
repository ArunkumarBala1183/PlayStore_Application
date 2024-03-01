import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
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
}
