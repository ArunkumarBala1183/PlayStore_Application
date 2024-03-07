import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { AllAppsInfo, DeveloperAppInfo, SpecificAppInfo } from 'src/app/interface/user';
import { AppService } from 'src/app/services/app.service';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-developer-my-apps',
  templateUrl: './developer-my-apps.component.html',
  styleUrls: ['./developer-my-apps.component.scss']
})
export class DeveloperMyAppsComponent implements OnInit {
  constructor(private router:Router, private service : UserService , private loginService:LoginService){}

  ngOnInit(): void {
    const userId = this.loginService.getUserId();

    this.service.getDeveloperApps(userId).subscribe({
      next: (response) => {
        this.developedApps = response;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
      
    
  appDetails : AllAppsInfo[] = [];
  developedApps : DeveloperAppInfo[] = [];
  public redirectTospecificApp(appId : Guid) 
  {
    this.router.navigate(['user/specificApp',appId]);
  }
}
