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
import { HttpErrorResponse } from '@angular/common/http';

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
      this.getSpecificApp(appId)
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

  getSpecificApp(appId : Guid)
  {
    this.service.getAppsById(appId).subscribe({
      next: (responses) => {
        this.appDetail = responses;
      },
      error: (error) => {
        console.log(error);
      },
    });
    
    this.service.getReviews(appId).subscribe({
      next: (response) => {
        this.appReview = response;
        this.updateDisplayedReviews();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  public Downloadfile() {
    this.route.params.subscribe((params) => {
      const appId: Guid = params['appId'];
      const userId = this.appInfo[0].userId;

      this.service.PostAppFile(appId, userId).subscribe({
        next: (response) => {
          const blob = new Blob([response]);
          const url = window.URL.createObjectURL(blob);
          const anchor = document.createElement('a');
          anchor.setAttribute('type', 'hidden');
          anchor.href = url;
          anchor.download = new Date().toISOString() + '.zip';
          anchor.click();
          anchor.remove();

          this.getSpecificApp(appId)

        },
        error: (error) => {
          console.error('Error Occurred :', error);
          if (error instanceof HttpErrorResponse) {
            if (error.status === 400) {
              this.alreadyDownloaded = true;
              const errorMessage =
                error.error instanceof Blob
                  ? 'Already Downloaded'
                  : error.error;
              console.error(errorMessage);
            }
          }
        },
      });
    });
  }

  public onSubmit() {
    if (this.reviewForm.valid) {
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

  public showMoreReviews() {
    this.showAllReviews = true;
    this.updateDisplayedReviews();
  }

  public updateDisplayedReviews() {
    const maxReviewsToShow = this.showAllReviews
      ? this.appReview.length
      : Math.min(2, this.appReview.length);
    this.displayedReviews = this.appReview.slice(0, maxReviewsToShow);
  }

  appDetail: SpecificAppInfo[] = [];
  appReview: AppReviewsInfo[] = [];
  displayedReviews: any[] = [];
  showAllReviews = false;
  appInfo: AllAppsInfo[] = [];
  reviewForm: FormGroup;
  currentRating = 0;
  stars = Array(5);
  alreadyDownloaded = false;

  public rating(value: number) {
    this.currentRating = value;
    this.reviewForm.patchValue({ rating: value });
  }
}
