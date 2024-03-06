import { Component , OnInit} from '@angular/core';
import { ListApps } from 'src/app/interface/list-apps';
import { AppInfoService } from 'src/app/services/app-info.service';

@Component({
  selector: 'app-apps',
  templateUrl: './apps.component.html',
  styleUrls: ['./apps.component.scss']
})
export class AppsComponent implements OnInit{
  menu = false;

  appDetails: ListApps[] | undefined
  searchTerm: string = '';



  constructor(private service: AppInfoService) 
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

}
