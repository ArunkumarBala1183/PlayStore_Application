import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { App } from '../interface/app';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  public selectedApp: App = {} as App;
  apiBaseAddress = environment.apiBaseAddress;
  constructor(private http: HttpClient) { }

  getApp() {
    return this.http.get<App[]>(this.apiBaseAddress + 'api/App');
  }
}
