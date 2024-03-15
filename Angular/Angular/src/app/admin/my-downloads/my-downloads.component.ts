import { Component, OnInit } from '@angular/core';
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
  isDownloaded : boolean = false;

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
        this.isDownloaded = true
      },
      error : error => {
        this.isDownloaded = false
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
