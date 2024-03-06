import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {  EmailExists, Login, PasswordChange,  Register, otpVerify } from '../interface/login';
import { JwtHelperService } from '@auth0/angular-jwt';



@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiBaseAddress=environment.apiBaseAddress;
  constructor(private http:HttpClient , private jwtHelper:JwtHelperService) {
    this.jwtHelper=new JwtHelperService();
   }
public  checkUser(email: EmailExists) {
   
  
    return this.http.post(this.apiBaseAddress + 'Login/CheckEmailExistence', email, {observe: 'response' });
  }


  
 public forgotPassword(email:EmailExists)
  {
    
    console.log(email);
     return this.http.post(this.apiBaseAddress + 'Login/forgot-Password' , email ,{  observe: 'response' });
  }
 public addUser(userData: Register)
  { 
    console.log(userData);
    return this.http.post(this.apiBaseAddress +'Login/register',userData , {observe:'response'} );

  }
 public verifyLogin(credentials:Login)
  { console.log(credentials);
    return this.http.post(this.apiBaseAddress + "Login/User-Login" ,credentials);

  }
 public verifyOtp(otp:otpVerify)
  {
    return this.http.post(this.apiBaseAddress +"Login/validate-otp", otp);
  }
 public changePassword(password :PasswordChange)
  {
     return this.http.post(this.apiBaseAddress +"Login/reset-password" ,password);
  }
 public refreshToken(expiredToken:string)
  {
    console.log('refreshtokenService')
    console.log(expiredToken)
    const refreshTokenCommand = { ExpiredToken: expiredToken };
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
      return this.http.post(this.apiBaseAddress + "Login/refresh-token",JSON.stringify(refreshTokenCommand),{headers});
  }
 public logout():void
  {
       localStorage.removeItem('accessToken');
  }
 public isAuthenticated():boolean
  {
    const token=this.getToken();
    console.log(token);
    return !!token 
     && !this.jwtHelper.isTokenExpired(token);
  }
 public getToken():string|null
  {
    return localStorage.getItem('accessToken');
  }
 public getUserRole():string |null{
    const token=localStorage.getItem('accessToken');
    // console.log('.........Arun.........'+ token);
    if(token)
    {
      // console.log('..................'+ token);
      const decodedToken=this.jwtHelper.decodeToken(token);
      const role=decodedToken['role'];
      // console.log('...............'+ role)
      return role;
    }
    return null;
  }
 public isTokenExpired():boolean
  { 
    console.log('token expired called')
   const token=this.getToken();
   if(token)
   {
    const expirationDate=this.jwtHelper.getTokenExpirationDate(token);
    console.log(expirationDate)
    const currentTime=new Date().getTime();
    const expirationTime=expirationDate? expirationDate.getTime() :0;
    const timeDifference=expirationTime-currentTime;
    console.log(timeDifference)
    const oneMinute=1*60*1000;
    if(timeDifference<oneMinute)
    {console.log('refresh..........')
      return true;
    }
   }
    return false;

  }
 
}
