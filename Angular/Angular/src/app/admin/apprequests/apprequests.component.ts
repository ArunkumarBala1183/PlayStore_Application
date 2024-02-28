import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-apprequests',
  templateUrl: './apprequests.component.html',
  styleUrls: ['./apprequests.component.scss']
})
export class ApprequestsComponent {
  constructor(private router: Router){}
  

  viewRequest(){

    this.router.navigate(["admin/requestdetails"]) 
   }


requestapps=[
  { name: 'Instagram', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' ,Description:'Social media app for all the users.can upload photos videos and so on lorem nhdbdkndjnsdknfssnvkjsnvjcnvkjvuisf'},
  { name: 'Instagram', category: 'Education', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' ,Description:'Social media app for all the users.can upload '},
  { name: 'Instagram', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' ,Description:'Social media app for all the users.can upload photos videos and so on lorem '},
]
}
