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

  isLoading : boolean=false;




  downloadedApps!: DownloadedAppsInfo[]

  constructor(private router: Router,private service : UserService , private loginService : LoginService)
  {

  }

  ngOnInit(): void {
    this.getAppDownloads()
  }



  getAppDownloads()
  {
    this.isLoading=true;
    const userId = this.loginService.getUserId()

    this.service.getDownloadedApps(userId)
    .subscribe({
      next : response =>{
        this.downloadedApps = response
        this.isLoading=false
      },
      error : error => {
        this.downloadedApps = []
        console.log(error)
        this.isLoading=false
      }
    })
  }
  public redirectTospecificApp(fileid: Guid) {
    console.log(fileid);
    
    this.router.navigate(['/admin/downloadPage', fileid])
    
  }
}
