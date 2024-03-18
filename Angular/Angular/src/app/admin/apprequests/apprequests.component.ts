import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Apprequests } from 'src/app/interface/apprequests';
import { AppRequestsService } from 'src/app/services/app-requests.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-apprequests',
  templateUrl: './apprequests.component.html',
  styleUrls: ['./apprequests.component.scss']
})
export class ApprequestsComponent implements OnInit {
  appRequests: Apprequests[] = [];
  isLoading: boolean = false;

  constructor(private router: Router, private service: AppRequestsService) { }

  ngOnInit(): void {
    this.getAllRequests();
  }

  getAllRequests() {
    this.isLoading = true; // Set loading to true before fetching data
    this.service.getAppRequests().subscribe({
      next: response => {
        this.appRequests = response.body as Apprequests[];
        this.isLoading = false; // Set loading to false after fetching data
      },
      error: error => {
        this.appRequests = [];
        console.log(error);
        this.isLoading = false; // Set loading to false if there's an error
      }
    });
  }

  viewRequest(appId : string){
    this.userservice.sendAppId(appId);
    this.router.navigate(["admin/request-details"]) 
   }



}
