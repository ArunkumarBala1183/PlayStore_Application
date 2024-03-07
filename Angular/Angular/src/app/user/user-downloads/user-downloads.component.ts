import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AllAppsInfo, DownloadedAppsInfo } from 'src/app/interface/user';
import { AppService } from 'src/app/services/app.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-downloads',
  templateUrl: './user-downloads.component.html',
  styleUrls: ['./user-downloads.component.scss']
})
export class UserDownloadsComponent implements OnInit {
  constructor(private router:Router, private services : UserService){}
  
  ngOnInit(): void {
      this.services.getAllApps().subscribe(
        {
          next : response => 
          {
            this.appDetails = response
            const userId = this.appDetails[0].userId;
            this.services.getDownloadedApps(userId).subscribe(
              {
                next : response => {
                  console.log(this.appDetails[0].userId);
                  this.downloadedApps = response;
                  console.log(this.downloadedApps[0]);
                },
                error : error => {
                  console.log(error);          
                }
              }
            )            
          }
        }
      )
  
  }

  public redirectTospecificApp(appId : string) 
  {
    this.router.navigate(['user/specificApp',appId]);
  }

  // downloadedApps : any;

  downloadedApps : DownloadedAppsInfo[] =[];
  appDetails : AllAppsInfo[] = [];
  averageRating = 3;
  // Total Downloads
  Downloads = 1000000;
}
