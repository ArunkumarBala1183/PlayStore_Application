import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { DownloadedAppsInfo } from 'src/app/interface/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-my-downloads',
  templateUrl: './my-downloads.component.html',
  styleUrls: ['./my-downloads.component.scss']
})
export class MyDownloadsComponent implements OnInit {





  downloadedApps!: DownloadedAppsInfo[]

  constructor(private router: Router,private userservice : UserService , private loginService : LoginService)
  {

  }

  ngOnInit(): void {
    this.getAppDownloads()
  }



  getAppDownloads()
  {
    const userId = this.loginService.getUserId()

    this.userservice.getDownloadedApps(userId)
    .subscribe({
      next : response =>{
        this.downloadedApps = response
      },
      error : error => {
        this.downloadedApps = []
        console.log(error)
      }
    })
  }
  public redirectTospecificApp(appId: Guid) {
    // console.log(fileid);
    // this.router.navigate(['admin/download-page', fileid])
      this.userservice.sendAppId(appId);
      this.router.navigate(['admin/download-page'])
  }
}
