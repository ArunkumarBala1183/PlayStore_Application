import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { userInfo } from 'src/app/interface/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  userData! : userInfo
  
  constructor(private router: Router, private loginService: LoginService, private service: UserService,private toastr : ToastrService) { }

  ngOnInit(): void {
    const userId = this.loginService.getUserId();
    this.service.getUserData(userId).subscribe(
      {
        next: (response) => {
          this.userData = response;
        },
        error: error => {
          this.toastr.error('User Not Found');
        },
      });
  }

  public resetPassword() {
    this.router.navigate(['user/reset-password'])
  }
}

