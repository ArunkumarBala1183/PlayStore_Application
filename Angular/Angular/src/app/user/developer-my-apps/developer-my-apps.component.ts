import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { AllAppsInfo, DeveloperAppInfo, SpecificAppInfo } from 'src/app/interface/user';
import { AppService } from 'src/app/services/app.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-developer-my-apps',
  templateUrl: './developer-my-apps.component.html',
  styleUrls: ['./developer-my-apps.component.scss']
})
export class DeveloperMyAppsComponent implements OnInit {
  constructor(private router:Router, private service : UserService){}

  ngOnInit(): void {
    this.service.getAllApps().subscribe(
      {
        next : response => {
          this.appDetails = response;
          const userId = this.appDetails[0].userId;
          this.service.getDeveloperApps(userId).subscribe(response => {
            this.developedApps = response;
          })
        }
      }
    )
  }
  appDetails : AllAppsInfo[] = [];
  developedApps : DeveloperAppInfo[] = [];
  public redirectTospecificApp(appId : Guid) 
  {
    this.router.navigate(['user/specificApp',appId]);
  }
}
