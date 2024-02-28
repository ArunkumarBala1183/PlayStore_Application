import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-specific-app',
  templateUrl: './specific-app.component.html',
  styleUrls: ['./specific-app.component.scss']
})

export class SpecificAppComponent implements OnInit {

onSubmit() 
{
  if(this.reviewForm.valid)
  {
    alert("Form Submitted");
  }
  else{
    alert("enter valid details");
  }
}

  constructor(private route:ActivatedRoute, private service:UserService, private formBuilder : FormBuilder)
  {
    this.reviewForm = this.formBuilder.group({
      reviews : ['',Validators.required],
    })
  }

  ngOnInit(): void {
    window.scrollTo(0,0);
    this.route.params.subscribe(params => {
      const appId : string = params['appId'];
      this.appDetail = this.service.AllAppsInfo.find(a => a.appId === appId);
      console.log(this.appDetail);
    })
  }

  appDetail : any;
  
  stars =  Array(5);
  currentRating = 0;
  averageRating = 3;
  Downloads = 10000;

// rating function for the user to rate the specific app. 
rating(rating: number) {
this.currentRating = rating;
console.log(this.currentRating);
}

reviewForm : FormGroup;



}
