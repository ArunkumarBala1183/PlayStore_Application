import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { userInfo } from 'src/app/interface/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  userDetails! : userInfo
  constructor(private router : Router , private loginService : LoginService , private service : UserService){}
  
  ngOnInit(): void {
    this.getUserProfile();
  }

  changePassword() {
  this.router.navigate(["admin/change-password"]);
  }

  getUserProfile()
  {
    this.service.getUserData(this.loginService.getUserId())
    .subscribe({
      next : response => {
        this.userDetails = response
      },
      error : error => {
        console.log(error)
      }
    })
  }

}
