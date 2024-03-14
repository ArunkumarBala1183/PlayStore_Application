import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './services/login.service';
import { Observable, of} from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import{map,catchError}from 'rxjs/operators'


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: LoginService, private router: Router, private toastr:ToastrService) {}

  canActivate(route:ActivatedRouteSnapshot, state:RouterStateSnapshot):Observable<boolean> {
    if (!this.authService.isAuthenticated())
     {
       this.toastr.error('Please login')
      this.router.navigate(['login']); 
      return of(false); 
    } 
    if(this.authService.isTokenExpired())
    {
      const expiredToken=this.authService.getToken();
      if(expiredToken)
      {
       return this.authService.refreshToken(expiredToken).pipe(
          
            map((refreshToken:any)=>
            {
              const accessToken=refreshToken.accessToken;
              localStorage.setItem('accessToken',accessToken)
              console.log(accessToken + 'Token Refreshed...................................');
              return true;
            }),
            catchError(
            (error:any)=>
            {
              console.log(error)
              this.toastr.info('Session expired . Please Login')
              this.router.navigate(['login']);

              return of(false);
            })
          
        )
      }
      else
      {
        this.toastr.info('Session expired.Please Login')
        this.router.navigate(['login']);
        return of(false);
      }
      }
      const requiredRole=route.data.role;
      if(requiredRole)
      {
        const userRole=this.authService.getUserRole();
        if(requiredRole && requiredRole.includes(userRole))
        {
          console.log(userRole +'..............................')
          return of(true);
        }
        else{
          this.router.navigate(['login']);
          return of(false);
        }

      }
      
      return of(true);
     
     
    }

}
   

  
