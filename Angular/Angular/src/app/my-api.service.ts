import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MyApiService {
baseAddress=environment.apiBaseAddress;
  constructor(private http: HttpClient) { }
getAllUsers()
{
  return this.http.get(this.baseAddress + "UserController/getUsers");
}
}
