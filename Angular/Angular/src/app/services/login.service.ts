import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {  EmailExists, Login, PasswordChange, Register, otpVerify } from '../interface/login';



@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiBaseAddress=environment.apiBaseAddress;
  constructor(private http:HttpClient) { }
  // checkUser(email:EmailExists)
  // {
    
  //   console.log(email);
  //    return this.http.post(this.apiBaseAddress + 'Login/CheckEmailExistence' , email ,{  observe: 'response' });
  // }

  checkUser(email: EmailExists) {
   
  
    return this.http.post(this.apiBaseAddress + 'Login/CheckEmailExistence', email, {observe: 'response' });
  }


  
  forgotPassword(email:EmailExists)
  {
    
    console.log(email);
     return this.http.post(this.apiBaseAddress + 'Login/forgot-Password' , email ,{  observe: 'response' });
  }
  addUser(userData: Register)
  { 
    console.log(userData);
    return this.http.post(this.apiBaseAddress +'Login/register',userData , {observe:'response'} );

  }
  verifyLogin(credentials:Login)
  { console.log(credentials);
    return this.http.post(this.apiBaseAddress + "Login/User-Login" ,credentials);

  }
  verifyOtp(otp:otpVerify)
  {
    return this.http.post(this.apiBaseAddress +"Login/validate-otp", otp);
  }
  changePassword(password :PasswordChange)
  {
     return this.http.post(this.apiBaseAddress +"Login/reset-password" ,password);
  }
}
