import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Email, Login, PasswordChange, Register, otpVerify } from '../interface/login';


@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiBaseAddress=environment.apiBaseAddress;
  constructor(private http:HttpClient) { }
  checkUser(email:Email)
  {
     return this.http.post(this.apiBaseAddress + "login/checkuser" , email);
  }
  addUser(userData: Register)
  { 
    return this.http.post(this.apiBaseAddress +"login/addUser",userData );

  }
  verifyLogin(credentials:Login)
  {
    return this.http.post(this.apiBaseAddress + "login/verifyLogin" ,credentials);

  }
  verifyOtp(otp:otpVerify)
  {
    return this.http.post(this.apiBaseAddress +"login/verifyOtp", otp);
  }
  changePassword(password :PasswordChange)
  {
     return this.http.post(this.apiBaseAddress +"login/changePassword" ,password);
  }
}
