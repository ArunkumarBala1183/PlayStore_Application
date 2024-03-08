import { Component, OnInit } from '@angular/core';
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

  constructor(private service : UserService , private loginService : LoginService)
  {

  }

  ngOnInit(): void {
    this.getAppDownloads()
  }

  apps = [
  
    { name: 'Instagram', rating: '4.3', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' },
    { name: 'Whatsapp', rating: '4.5', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/3536/3536445.png' },
    { name: 'Twitter', rating: '2.1', category: 'social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/3256/3256013.png' },
  ]

  getAppDownloads()
  {
    const userId = this.loginService.getUserId()

    this.service.getDownloadedApps(userId)
    .subscribe({
      next : response =>{
        this.downloadedApps = response
        console.log(this.downloadedApps)
      },
      error : error => {
        console.log(error)
      }
    })
  }
}
