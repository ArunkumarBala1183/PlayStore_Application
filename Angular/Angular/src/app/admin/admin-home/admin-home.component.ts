import { Component  } from '@angular/core';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.scss']
})
export class AdminHomeComponent  {
  
  menu=false;

 
  
  show()
  {
    this.menu=!this.menu;
  }
}
