import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-downloads',
  templateUrl: './user-downloads.component.html',
  styleUrls: ['./user-downloads.component.scss']
})
export class UserDownloadsComponent {
  constructor(private router:Router){}
  specificApp() 
  {
    this.router.navigate(['user/specificApp']);
  }
  
  averageRating = 3;
  // Total Downloads
  Downloads = 1000000;
}
