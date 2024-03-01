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


requestapps=[
  { name: 'Instagram', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' ,Description:'Social media app for all the users.can upload photos videos and so on lorem nhdbdkndjnsdknfssnvkjsnvjcnvkjvuisf'},
  { name: 'Instagram', category: 'Education', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' ,Description:'Social media app for all the users.can upload '},
  { name: 'Instagram', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' ,Description:'Social media app for all the users.can upload photos videos and so on lorem '},
]
}
