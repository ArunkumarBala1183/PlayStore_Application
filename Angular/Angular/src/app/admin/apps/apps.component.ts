import { Component , OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ListApps } from 'src/app/interface/list-apps';
import { AppInfoService } from 'src/app/services/app-info.service';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-apps',
  templateUrl: './apps.component.html',
  styleUrls: ['./apps.component.scss']
})
export class AppsComponent implements OnInit{
  menu = false;

  appDetails: ListApps[] | undefined
  searchTerm: string = '';
 



  constructor(private service: AppInfoService, private route: Router, private loginService : LoginService) 
  {
      
  }


  ngOnInit(): void 
  {
    this.getAllApps();
  }

  show() {
    this.menu = !this.menu;
  }


  get allApps()
  {
    return this.appDetails?.filter(app => app.name.toLowerCase().includes(this.searchTerm.toLowerCase()))
  }

  getDownloadPage(appId : Guid)
  {
    this.route.navigate(["admin/downloadPage" , appId]);
  }

  getAllApps() {
    this.service.GetAllApps()
      .subscribe({
        next: response => {
          this.appDetails = response.body as ListApps[]
        },
        error: error => {
          console.log(error)
        }
      })
  }

  IsAlreadyInstalled(appId : Guid) : boolean
  {
    return true
  }

}
