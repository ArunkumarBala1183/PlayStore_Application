import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppService } from 'src/app/services/app.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-developer-my-apps',
  templateUrl: './developer-my-apps.component.html',
  styleUrls: ['./developer-my-apps.component.scss']
})
export class DeveloperMyAppsComponent implements OnInit {
  constructor(private router:Router, private services : UserService){}

  ngOnInit(): void {
    this.developedApps = this.services.DeveloperAppInfo;
  }

  developedApps : any;
  redirectTospecificApp(appId : string) 
  {
    this.router.navigate(['user/specificApp',appId]);
  }
  
  averageRating = 3;
  // Total Downloads
  Downloads = 1000000;
}
