import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { LoginService } from '../services/login.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../services/user.service';
import { DeveloperAppInfo } from '../interface/user';
import { HttpStatusCode } from '@angular/common/http';

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
      },
      error: (error) => {
        if(error.status == HttpStatusCode.NotFound)
        {
          this.isDeveloper = false;
        }
        console.log(error);
      },
    });
  }

  homePage = environment.homeRoute;
  downloadsPage = environment.downloadsRoute;
  aboutUsPage = environment.aboutUsRoute;
  myAppsPage = environment.myAppsRoute;
  newAppPage = environment.newAppRoute;
  userProfilePage = environment.userProfileRoute;
  specificAppPage = environment.specificAppRoute;
  resetPasswordPage = environment.resetPasswordRoute;


  // for highlighting the Selected/Current component.
  isHighlightComponent: string = this.homePage;
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
