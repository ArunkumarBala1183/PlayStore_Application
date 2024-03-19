import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';
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
  constructor(private router: Router, private services: UserService , private loginService : LoginService, private toastr : ToastrService) {}
  downloadedApps: DownloadedAppsInfo[] = [];
  ngOnInit(): void {
        this.isLoading = true;
        const userId = this.loginService.getUserId();
        this.services.getDownloadedApps(userId).subscribe({
          next: response => {
            this.downloadedApps = response;
            this.isLoading=false
          },
          error: error =>
          {
            this.toastr.error('No App Found');
            this.isLoading = false;
          },
          complete : () => {
            this.isLoading = false;
          }
          });
  }

  isLoading : boolean = false;
  public redirectTospecificApp(fileid: Guid) {
    this.services.sendAppId(fileid);
    this.router.navigate(['/user/specific-app'])
  }
}
