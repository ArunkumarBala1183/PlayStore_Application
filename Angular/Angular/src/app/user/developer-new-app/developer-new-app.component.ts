import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AllAppsInfo, CategoryInfo } from 'src/app/interface/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-developer-new-app',
  templateUrl: './developer-new-app.component.html',
  styleUrls: ['./developer-new-app.component.scss'],
})
export class DeveloperNewAppComponent implements OnInit {
  public appData!: FormGroup;
  fileSize = false;
  multipleFile = false;
  category: CategoryInfo[] = [];
  appInfo: AllAppsInfo[] = [];
  AppFile: File | null = null;
  Logo: File | null = null;
  appImages: File[] = [];
  logoFileFormatCheck = false;
  screenshotFileFormatCheck = false;
  appFileFormatCheck = true;

  constructor(
    public router: Router,
    public formbuilder: FormBuilder,
    private service: UserService,
    private loginService:LoginService,
    private toastr : ToastrService
  ) {}
  ngOnInit(): void {
    this.initForm();
    this.service.getCategory().subscribe({
      next: (response) => {
        this.category = response;
      },
    });
    // this.service.getAllApps().subscribe({
    //   next: (response) => {
    //     this.appInfo = response;
    //   },
    //   error: (error) => {
    //     console.log(error);
    //   },
    // });
  }
  public initForm() {
    this.appData = this.formbuilder.group({
      Name: ['', Validators.required],
      CategoryId: ['', Validators.required],
      PublisherName: ['', Validators.required],
      Description: ['', Validators.required],
      Logo: [null, Validators.required],
      AppFile: [null, Validators.required],
      appImages: [null, Validators.required],
    });
  }
  validationMessages = {
    Name: {
      required: 'App Name is required',
    },
    CategoryId: {
      required: 'Required ',
    },
    PublisherName: {
      required: 'Required ',
    },
    Description: {
      required: 'Description for App is Required',
    },
    Logo: {
      required: 'App Logo is Required',
    },
    AppFile: {
      required: 'App File is Required',
    },
    appImages: {
      required: 'Screenshot of App is Required',
    },
  };

  public handleFile(event: any): void {
    const files = event.target.files[0];
    if (files) {
      const file = files;
      const filetype = file.type;
      const maxFileSize = 2 * 1024 * 1024; // 2 MB
      if (file.size > maxFileSize) {
        this.fileSize = true;
        event.target.value = '';
        return;
      }
      else if(filetype === 'application/x-zip-compressed')
      {
        this.appFileFormatCheck = true;
        this.fileSize = false;
        this.AppFile = event.target.files[0];
      }
      else
      {
        this.appFileFormatCheck = false;
        event.target.value = '';
        this.fileSize = false;
      }     
    }
  }
  public multipleFiles(event: any): void {
    const files: FileList = event.target.files;
    if (files.length > 3 || files.length <= 1) {
      this.multipleFile = true;
      event.target.value = '';
      return;
    }
    this.multipleFile = false;
    for (let i = 0; i < files.length; i++) {
      if (files[i] && ['image/jpeg', 'image/png'].includes(files[i].type)) {
        this.appImages.push(files.item(i)!);
        this.screenshotFileFormatCheck = false;
      } else {
        this.screenshotFileFormatCheck = true;
        event.target.value ='';
      }
    }
  }

  public handleLogo(event: any): void {
    this.Logo = event.target.files[0];
    if (this.Logo && ['image/jpeg', 'image/png'].includes(this.Logo.type)) {
      this.logoFileFormatCheck = false;
    } else {
      this.logoFileFormatCheck = true;
      event.target.value = '';
    }
  }
 
  public onSubmit() {
    if (this.appData.valid) {
      const userData =  this.loginService.getUserId();
      
      const formData = new FormData();
      formData.append('UserId', userData);
      formData.append('Logo', this.Logo!);
      formData.append('AppFile', this.AppFile!);
      formData.append('Name', this.appData.get('Name')?.value);
      formData.append('CategoryId', this.appData.get('CategoryId')?.value);
      formData.append(
        'PublisherName',
        this.appData.get('PublisherName')?.value
      );
      formData.append('Description', this.appData.get('Description')?.value);
      for (let i = 0; i < this.appImages.length; i++) {
        formData.append('appImages', this.appImages[i]);
      }
      this.service.postApplication(formData).subscribe({
        next: (response : any) => {
          if(response === 400)
          {
            this.toastr.error('App Name already exist , try different Name');
          }
          else{
          this.toastr.success('Form Submitted',);
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }
}
