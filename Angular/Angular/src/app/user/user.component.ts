import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../services/user.service';
import { DeveloperAppInfo } from '../interface/user';
import { HttpStatusCode } from '@angular/common/http';
import { userRoutes } from '../shared/routings/redirect';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {

  constructor(private router: Router, private loginService:LoginService,private toastr:ToastrService,private userService:UserService) {}

  navbar = false;
  isDeveloper = false;
  developedApps : DeveloperAppInfo[] =[];


  ngOnInit(): void {
    const userId = this.loginService.getUserId();     // Fetches the UserId from the Token.
    this.userService.getDeveloperApps(userId).subscribe({   
      next: (response) => {     // returns AppDetails as the response.
        this.developedApps = response;  
        this.isDeveloper = true;
        if(response==null)
        {
        this.isDeveloper = false;
        
        }
      },
      error: (error) => {
        if(error.status == HttpStatusCode.NotFound)
        {
          this.isDeveloper = false;
        }
      },
    });
    this.userService.getIsDeveloper().subscribe(res=>{
      if(res){
        this.isDeveloper = res;
      }
    })
  }

  homePage = userRoutes.homeRoute;
  downloadsPage = userRoutes.downloadsRoute;
  aboutUsPage = userRoutes.aboutUsRoute;
  myAppsPage = userRoutes.myAppsRoute;
  newAppPage = userRoutes.newAppRoute;
  userProfilePage = userRoutes.userProfileRoute;
  specificAppPage = userRoutes.specificAppRoute;
  resetPasswordPage = userRoutes.resetPasswordRoute;
  
  // for highlighting the Selected/Current component.
  isHighlightComponent: string = '';
  public highlightSelectedComponent(component: string) {
    this.isHighlightComponent = component;
  }

  // logout
  public logout() {
    const response=this.loginService.logout();
     if(response)
     {
       this.toastr.show('Logout Successful')
       this.router.navigate(['']);
     }
  }

  // Show/Hide Menu bar for Mobile View
  public showMenu() {
    this.navbar = !this.navbar;
  }
}
