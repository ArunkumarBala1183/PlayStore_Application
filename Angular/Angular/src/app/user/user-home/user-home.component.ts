import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { AllAppsInfo } from 'src/app/interface/user';
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
  this.service.getAllApps().subscribe(response => {
    this.application=response;
    console.log(this.application);
    console.log(this.application[0].description);
    console.log(this.application[0].userId);
  })
}

application : AllAppsInfo[] = [];
// application = Array();
// application:any;

averageRating = 3;

Downloads = 10000;

redirectTospecificApp(appId: Guid) 
{
  this.router.navigate(['user/specificApp',appId]);
  
}

searchInput : string = '';

filterItems()
{
  return this.application.filter((app:AllAppsInfo) => 
      app.name.toLowerCase().includes(this.searchInput.toLowerCase()) || 
      app.categoryName.toLowerCase().includes(this.searchInput.toLowerCase())
    )
}
}


