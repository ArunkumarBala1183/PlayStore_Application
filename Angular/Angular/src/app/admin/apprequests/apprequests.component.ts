import { Component , OnInit} from '@angular/core';
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

  appRequests : Apprequests[] | undefined 
  
  constructor(private router: Router , private service : AppRequestsService,private userservice:UserService){}
  
  
  ngOnInit(): void {

    this.getAllRequests();
  }
// complete have to add
  getAllRequests() 
  {
    this.service.getAppRequests()
      .subscribe({
        next: response => {
          this.appRequests = response.body as Apprequests[]
        },
        error: error => {
          console.log(error)
        }
      })
  }
  

  viewRequest(appId : string){
    this.userservice.sendAppId(appId);
    this.router.navigate(["admin/request-details"]) 
   }



}
