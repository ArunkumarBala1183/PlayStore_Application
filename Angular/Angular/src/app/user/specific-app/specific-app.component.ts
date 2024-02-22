import { Component } from '@angular/core';

@Component({
  selector: 'app-specific-app',
  templateUrl: './specific-app.component.html',
  styleUrls: ['./specific-app.component.scss']
})
export class SpecificAppComponent {
  stars =  Array(5);
  currentRating = 0;
  averageRating = 3;
  Downloads = 10000;

// rating function for the user to rate the specific app. 
rating(rating: number) {
this.currentRating = rating;
console.log(this.currentRating);
}
}
