import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';
import { AllAppsInfo } from 'src/app/interface/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.scss'],
})
export class UserHomeComponent implements OnInit {
  isLoading:boolean=false

  constructor(
    private router: Router,
    private service: UserService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.isLoading=true
    this.service.getAllApps().subscribe({
      next: (response) => {
          this.application = response;
          this.isLoading=false
      },
      error: (error) => {
        console.error(error); 
        this.isLoading=false       
      },
    });
  }

  application: AllAppsInfo[] = [];

  public redirectTospecificApp(appId: Guid) 
  {
    this.service.sendAppId(appId);
    this.router.navigate(['user/specific-app'])
  }

  searchInput: string = '';

  public filterItems() {
    return this.application.filter(
      (app: AllAppsInfo) =>
        app.name.toLowerCase().includes(this.searchInput.toLowerCase()) ||
        app.categoryName.toLowerCase().includes(this.searchInput.toLowerCase())
    );
  }
}


