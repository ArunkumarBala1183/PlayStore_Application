import { APP_ID, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import {
  AllAppsInfo,
  AppReviewsInfo,
  SpecificAppInfo,
} from 'src/app/interface/user';

@Component({
  selector: 'app-specific-app',
  templateUrl: './specific-app.component.html',
  styleUrls: ['./specific-app.component.scss'],
})
export class SpecificAppComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private service: UserService,
    private formBuilder: FormBuilder
  ) {
    this.reviewForm = this.formBuilder.group({
      commands: ['', Validators.required],
      rating: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    window.scrollTo(0, 0);
    this.route.params.subscribe((params) => {
      const appId: Guid = params['appId'];
      console.log(appId);
      this.service.getAppsById(appId).subscribe({
        next: (responses) => {
          this.appDetail = responses;
          console.log(this.appDetail[0].publisherName);
          console.log(this.appDetail[0].publishedDate);
        },
        error: (error) => {
          console.log(error);
        },
      });
      this.service.getReviews(appId).subscribe({
        next: (response) => {
          this.appReview = response;
        },
        error: (error) => {
          console.log(error);
        },
      });
    });

    this.service.getAllApps().subscribe({
      next: (response) => {
        this.appInfo = response;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  Downloadfile() {
    this.route.params.subscribe((params) => {
      const appId: Guid = params['appId'];
      const userId = this.appInfo[0].userId;

      this.service.PostAppFile(appId,userId).subscribe(
        {
        next: (response) => {
          console.log(response);
          const blob = new Blob([response]);
          const url = window.URL.createObjectURL(blob);
          const anchor = document.createElement('a');
          anchor.setAttribute('type', 'hidden');
          anchor.href = url;
          anchor.download = new Date().toISOString() + '.zip';
          anchor.click();
          anchor.remove();
        },
        error : error => {
          console.log(error);
          
        }
      });
    });
  }

  onSubmit() {
    if (this.reviewForm.valid) {
      console.log(this.reviewForm);
      const formData = this.reviewForm.value;
      const appId = this.route.snapshot.params['appId'];
      const userId = this.appInfo[0].userId;
      formData.appId = appId;
      formData.userId = userId;

      formData.additionalValue = this.service.postReview(formData).subscribe({
        next: (response) => {
          console.log('Form Submitted', response);
        },
        error: (error) => {
          console.log(error);
        },
      });
      alert('Form Submitted');
    } else {
      alert('enter valid details');
    }
  }

  appDetail: SpecificAppInfo[] = [];
  appReview: AppReviewsInfo[] = [];
  appInfo: AllAppsInfo[] = [];
  reviewForm: FormGroup;
  currentRating = 0;
  stars = Array(5);
  averageRating = 3;
  Downloads = 10000;
  // rating function for the user to rate the specific app.
  rating(value: number) {
    this.currentRating = value;
    this.reviewForm.patchValue({ rating: value });
    console.log(this.currentRating);
  }
}
