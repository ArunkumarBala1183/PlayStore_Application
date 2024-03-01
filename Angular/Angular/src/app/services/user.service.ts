import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl : string = environment.apiBaseAddress + "Users"
  
  constructor(private http : HttpClient) 
  { }

  getAllUsers()
  {
    return this.http.get(this.baseUrl + "/GetAllUsers" , {observe : "response"});
  }
}
