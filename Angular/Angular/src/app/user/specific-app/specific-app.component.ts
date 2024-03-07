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
import { LoginService } from 'src/app/services/login.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-specific-app',
  templateUrl: './specific-app.component.html',
  styleUrls: ['./specific-app.component.scss'],
})
export class SpecificAppComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private service: UserService,
    private formBuilder: FormBuilder,
    private loginService : LoginService,
    private toastr : ToastrService
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
  }
    // this.service.getAllApps().subscribe({
    //   next: (response) => {
    //     this.appInfo = response;
    //   },
    //   error: (error) => {
    //     console.log(error);
    //   },
    // });


  getSpecificApp(appId : Guid)
  {
    this.service.getAppsById(appId).subscribe({
      next: (responses) => {
        this.appDetail = responses;
        console.log(this.appDetail)
        const publishedDate = this.appDetail[0].publishedDate.toString().split('T')[0];//2024-03-01
        this.appDetail[0].publishedDate = publishedDate;
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
      const userId = this.loginService.getUserId();
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
      const userId = this.loginService.getUserId();
      formData.appId = appId;
      formData.userId = userId;

      formData.additionalValue = this.service.postReview(formData).subscribe({
        next: (response) => {
          console.log('Form Submitted', response);
          this.getSpecificApp(appId);
        },
        error: (error) => {
          console.log(error);
        },
      });
      this.toastr.success('Form Submitted');
    } 
    else {
      this.toastr.error('Enter Valid Details');
    }
  }

  public showMoreReviews() {
    this.showAllReviews = true;
    this.updateDisplayedReviews();
  }

  public updateDisplayedReviews() {
    const maxReviewsToShow = this.showAllReviews ? this.appReview.length : Math.min(3, this.appReview.length);
    this.displayedReviews = this.appReview.slice(0, maxReviewsToShow);
  }

  appDetail: SpecificAppInfo[] = [];
  appReview: AppReviewsInfo[] = [];
  displayedReviews: any[] = [];
  showAllReviews = false;
  // appInfo: AllAppsInfo[] = [];
  reviewForm: FormGroup;
  currentRating = 0;
  stars = Array(5);
  alreadyDownloaded = false;

  public rating(value: number) {
    this.currentRating = value;
    this.reviewForm.patchValue({ rating: value });
  }
}
