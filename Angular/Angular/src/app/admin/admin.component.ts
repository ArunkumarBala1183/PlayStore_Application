import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {

  constructor(private router: Router){}
  menu = false;
  show()
  {
  
    this.menu=!this.menu;
  }

  about()
  {
    this.router.navigate(["admin/about"]) 
  }
}
