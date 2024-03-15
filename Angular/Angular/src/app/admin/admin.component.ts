import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  

  constructor(private router: Router, private loginService:LoginService,private toastr:ToastrService){}
  menu:boolean = true;
  


  
  show()
  {
    this.menu=!this.menu;
  }

  about()
  {
    this.router.navigate(["admin/about"]) 
  }
  
  public logout()
  {
     const response=this.loginService.logout();
     if(response)
     {
       this.toastr.show('Logout Successful')
       this.router.navigate(['']);
     }
  }
}
