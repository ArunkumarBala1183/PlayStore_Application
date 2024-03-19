import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {
  EmailExists,
  Login,
  PasswordChange,
  Register,
  otpVerify,
} from '../interface/login';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  apiBaseAddress = environment.apiBaseAddress;
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
    this.jwtHelper = new JwtHelperService();
  }
  public checkUser(email: EmailExists) {
    return this.http.post(
      this.apiBaseAddress + 'Login/CheckEmailExistence',
      email,
      { observe: 'response' }
    );
  }

  public forgotPassword(email: EmailExists) {
  
    return this.http.post(
      this.apiBaseAddress + 'Login/forgot-Password',
      email,
      { observe: 'response' }
    );
  }
  public addUser(userData: Register) {
   
    return this.http.post(this.apiBaseAddress + 'Login/register', userData, {
      observe: 'response',
    });
  }
  public verifyLogin(credentials: Login) {
    
    return this.http.post(
      this.apiBaseAddress + 'Login/User-Login',
      credentials
    );
  }
  public verifyOtp(otp: otpVerify) {
    return this.http.post(this.apiBaseAddress + 'Login/validate-otp', otp);
  }
  public changePassword(password: PasswordChange) {
    return this.http.post(
      this.apiBaseAddress + 'Login/reset-password',
      password
    );
  }
  public refreshToken(expiredToken: string) {
    
    const refreshTokenCommand = { ExpiredToken: expiredToken };
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(
      this.apiBaseAddress + 'Login/refresh-token',
      JSON.stringify(refreshTokenCommand),
      { headers }
    );
  }
  public logout(): boolean {
    sessionStorage.removeItem('accessToken');
    const token = sessionStorage.getItem('accessToken');
    if (token == null) {
      return true;
    }
    return false;
  }
  public isAuthenticated(): boolean {
    const token = this.getToken();
    
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }
  public getToken(): string | null {
    return sessionStorage.getItem('accessToken');
  }
  public getUserRole(): string | null {
    const token = sessionStorage.getItem('accessToken');
   
    if (token) {
     
      const decodedToken = this.jwtHelper.decodeToken(token);
      console.log(decodedToken);
      const role = decodedToken['role'];
     
      return role;
    }
    return null;
  }
  public getUserId() {
    const token = sessionStorage.getItem('accessToken');

    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      const userId = decodedToken
        ? decodedToken[
            'http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata'
          ]
        : null;
      
      return userId;
    }
  }

  public isTokenExpired(): boolean {
    
    const token = this.getToken();
    if (token) {
      const expirationDate = this.jwtHelper.getTokenExpirationDate(token);
      
      const currentTime = new Date().getTime();
      const expirationTime = expirationDate ? expirationDate.getTime() : 0;
      const timeDifference = expirationTime - currentTime;
      
      const oneMinute = 1 * 60 * 1000;
      if (timeDifference < oneMinute) {
       
        return true;
      }
    }
    return false;
  }
}
