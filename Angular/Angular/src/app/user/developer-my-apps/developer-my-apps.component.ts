import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-developer-my-apps',
  templateUrl: './developer-my-apps.component.html',
  styleUrls: ['./developer-my-apps.component.scss']
})
export class DeveloperMyAppsComponent {
  constructor(private router:Router){}
  specificApp() 
  {
    this.router.navigate(['user/specificApp']);
  }
  
  averageRating = 3;
  // Total Downloads
  Downloads = 1000000;
}
