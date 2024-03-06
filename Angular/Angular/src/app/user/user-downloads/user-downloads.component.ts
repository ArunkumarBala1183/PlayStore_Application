import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppService } from 'src/app/services/app.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-downloads',
  templateUrl: './user-downloads.component.html',
  styleUrls: ['./user-downloads.component.scss']
})
export class UserDownloadsComponent implements OnInit {
  constructor(private router:Router, private services : UserService){}
  
  ngOnInit(): void {
    
    

  }

  redirectTospecificApp(appId : string) 
  {
    this.router.navigate(['user/specificApp',appId]);
  }


 

  
  averageRating = 3;
  // Total Downloads
  Downloads = 1000000;
}
