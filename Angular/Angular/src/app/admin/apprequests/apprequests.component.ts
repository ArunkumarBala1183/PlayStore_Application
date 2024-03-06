import { Component , OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { Apprequests } from 'src/app/interface/apprequests';
import { AppRequestsService } from 'src/app/services/app-requests.service';

@Component({
  selector: 'app-apprequests',
  templateUrl: './apprequests.component.html',
  styleUrls: ['./apprequests.component.scss']
})

export class ApprequestsComponent implements OnInit {

  appRequests : Apprequests[] | undefined 
  
  constructor(private router: Router , private service : AppRequestsService){}
  
  
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

    this.router.navigate(["admin/requestdetails", appId]) 
   }



}
