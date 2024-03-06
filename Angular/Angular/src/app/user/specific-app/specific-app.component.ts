import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-specific-app',
  templateUrl: './specific-app.component.html',
  styleUrls: ['./specific-app.component.scss']
})

export class SpecificAppComponent implements OnInit {
  userId: any;

onSubmit() 
{
  if(this.reviewForm.valid)
  { 
    alert("Form Submitted");
  }
  else{
    // console.log(this.reviewForm);
    alert("enter valid details");
  }
}

  constructor(private route:ActivatedRoute, private service:UserService, private formBuilder : FormBuilder)
  {
    this.reviewForm = this.formBuilder.group({
      AppId:Guid,
      UserId:Guid,
      reviews : ['',Validators.required],
      rating: [0, Validators.required]
    })
  }

  ngOnInit(): void {
    window.scrollTo(0,0);
    console.log(this.reviewForm.value);
    this.route.params.subscribe(params => {
      const appId : Guid = params['appId'];
      
      
     
      
    })
  }

  appDetail : any;
  appReview : any;
  
  stars =  Array(5);
  currentRating = 0;
  averageRating = 3;
  Downloads = 10000;
 

// rating function for the user to rate the specific app. 
rating(rating: number) {
this.currentRating = rating;
console.log(this.currentRating);
this.reviewForm.controls['rating'].setValue(rating);

}

reviewForm : FormGroup;



}
