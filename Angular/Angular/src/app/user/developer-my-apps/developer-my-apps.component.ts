import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {
  DeveloperAppInfo,
} from 'src/app/interface/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-developer-my-apps',
  templateUrl: './developer-my-apps.component.html',
  styleUrls: ['./developer-my-apps.component.scss'],
})
export class DeveloperMyAppsComponent implements OnInit {
  constructor(
    private service: UserService,
    private loginService: LoginService,
    private toastr : ToastrService
  ) {}

  ngOnInit(): void {

    // To Fetch the Apps Developed by the Developer.
    const userId = this.loginService.getUserId();     // Fetches the UserId from the Token.
    this.service.getDeveloperApps(userId).subscribe({   
      next: (response) => {     // returns AppDetails as the response.
        this.developedApps = response;
      },
      error: (error) => {
        this.toastr.error('No App Uploaded');
      },
    });
  }
  
  developedApps: DeveloperAppInfo[] = []; // Interface for Apps Developed by Developer

  // public redirectTospecificApp(appId: Guid) {
  //   this.router.navigate(['user/specificApp', appId]);
  // }
}
