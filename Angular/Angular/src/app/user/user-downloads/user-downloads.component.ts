import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AllAppsInfo, DownloadedAppsInfo } from 'src/app/interface/user';
import { AppService } from 'src/app/services/app.service';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-downloads',
  templateUrl: './user-downloads.component.html',
  styleUrls: ['./user-downloads.component.scss'],
})
export class UserDownloadsComponent implements OnInit {
  constructor(private router: Router, private services: UserService , private loginService : LoginService) {}

  ngOnInit(): void {
    this.services.getAllApps().subscribe({
      next: (response) => {
        this.appDetails = response;
        const userId = this.loginService.getUserId();
        this.services.getDownloadedApps(userId).subscribe({
          next: (response) => {
            this.downloadedApps = response;
          },
          error: (error) => {
          },
        });
      },
    });
  }

  public redirectTospecificApp(appId: string) {
    this.router.navigate(['user/specificApp', appId]);
  }

  downloadedApps: DownloadedAppsInfo[] = [];
  appDetails: AllAppsInfo[] = [];
}