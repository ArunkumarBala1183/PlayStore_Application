import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';

import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.scss']
})
export class UserHomeComponent implements OnInit {  
constructor(private router : Router, private service: UserService)
{
}
  ngOnInit(): void {
    
    // this.getApps();
    
    
  }

  // getApps()
  // {
   
  // }


application : any;

averageRating = 3;

Downloads = 10000;

redirectTospecificApp(appId: Guid) 
{
  this.router.navigate(['user/specificApp',appId]);
}

searchInput : string = '';

// filterItems()
// {
//   return this.application.filter(app => 
//       app.appName.toLowerCase().includes(this.searchInput.toLowerCase()) || 
//       app.appCategory.toLowerCase().includes(this.searchInput.toLowerCase())   
//     )
// }
}


