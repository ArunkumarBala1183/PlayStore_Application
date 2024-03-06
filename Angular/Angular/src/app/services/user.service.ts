import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SearchUser } from '../interface/search-user';

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

  getUserDetails(searchDetails : SearchUser)
  {
    return this.http.post(this.baseUrl + "/SearchUserDetails" , searchDetails, {observe : "response"});

  }
}
