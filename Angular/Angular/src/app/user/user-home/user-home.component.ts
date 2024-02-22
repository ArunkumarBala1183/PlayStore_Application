import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.scss']
})
export class UserHomeComponent {

constructor(private router : Router)
{
}
specificApp() 
{
  this.router.navigate(['user/specificApp']);
}

averageRating = 3;
// Total Downloads
Downloads = 10000;

}


