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

  apps = [

    { name: 'Instagram', rating: '4.3', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' },
    { name: 'Whatsapp', rating: '4.5', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/3536/3536445.png' },
    { name: 'Calculator', rating: '2.1', category: 'Education', imageUrl: 'https://cdn-icons-png.flaticon.com/128/9710/9710545.png' },
    { name: 'Twitter', rating: '2.1', category: 'social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/3256/3256013.png' },
    { name: 'Calculator', rating: '2.1', category: 'Education', imageUrl: 'https://cdn-icons-png.flaticon.com/128/9710/9710545.png' },
    { name: 'BMI', rating: '4.1', category: 'Entertainment', imageUrl: 'https://cdn-icons-png.flaticon.com/128/10659/10659540.png' }

    // Add more app objects here
  ];

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

  get filteredApps() {
    
    return this.apps.filter(app => app.name.toLowerCase().includes(this.searchTerm.toLowerCase()));
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
