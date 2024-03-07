import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { LoginService } from '../services/login.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent {
  homePage = environment.homeRoute;
  downloadsPage = environment.downloadsRoute;
  aboutUsPage = environment.aboutUsRoute;
  myAppsPage = environment.myAppsRoute;
  newAppPage = environment.newAppRoute;
  userProfilePage = environment.userProfileRoute;
  specificAppPage = environment.specificAppRoute;
  resetPasswordPage = environment.resetPasswordRoute;

  constructor(private router: Router, private loginService:LoginService,private toastr:ToastrService) {}

  isHighlightComponent: string = this.homePage;
  public highlightSelectedComponent(component: string) {
    this.isHighlightComponent = component;
  }

  public logout() {
    const response=this.loginService.logout();
     if(response)
     {
       this.toastr.show('Logout Successful')
       this.router.navigate(['']);
     }
  }
  navbar = false;
  public showMenu() {
    this.navbar = !this.navbar;
  }
}
