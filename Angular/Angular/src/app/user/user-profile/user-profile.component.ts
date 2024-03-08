import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserProfileInfo } from 'src/app/interface/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  ngOnInit(): void {
  }
constructor(private router:Router , private Service : UserService, private loginService : LoginService) {}

public resetPassword() 
{
  this.router.navigate(['user/resetPassword'])
}
}

