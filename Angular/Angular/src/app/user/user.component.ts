import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
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
  
  constructor(private router : Router) {
  }

  isHighlightComponent: string = this.homePage;
  public highlightSelectedComponent(component: string) 
  {
      this.isHighlightComponent = component;
  }

public logout() {
  this.router.navigate([''])
}
navbar = false;
public showMenu() {
this.navbar = !this.navbar;
}
}
