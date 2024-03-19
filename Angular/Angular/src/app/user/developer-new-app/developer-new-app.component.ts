import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
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

  public appData!: FormGroup; // Assigning the appData to the Form.
  fileSize = false; // to check the size of the compressed file. not more than 2mb.
  multipleFile = false; //  to check the selected images for App Screenshots. minimum 2 and maximum of 3 images.
  category: CategoryInfo[] = [];     
  AppFile: File | null = null; // to store the value of the App File from the form.
  Logo: File | null = null; // to store the value of the App Logo from the form.
  appImages: File[] = []; // to store the value of the App screenshots from the form.
  logoFileFormatCheck = false;
  screenshotFileFormatCheck = false;
  appFileFormatCheck = true;
  isDeveloper = false;
  validLogo = false;
  validAppFile = false;
  validScreenShots = false;

  constructor(
    public router: Router,
    public formbuilder: FormBuilder,
    private service: UserService,
    private loginService:LoginService,
    private toastr : ToastrService
  ) {}

  ngOnInit(): void {
    
    this.initForm();
    this.getCategory();
   
  }

  public getCategory()
  {
    this.service.getCategory().subscribe({
      next: (response) => {
        this.category = response;
      },
      error : error => 
      {
        this.toastr.error('Category Failed to Fetch');
      }
    });
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

  // to check the app File
  public handleFile(event: any): void {
    const files = event.target.files[0];
    if (files) {      // is file is not Empty.
      const file = files;
      const filetype = file.type;
      const maxFileSize = 2 * 1024 * 1024; // 2 MB
      if (file.size > maxFileSize) {      // fileSize > 2MB
        this.fileSize = true;
        event.target.value = '';
        return;
      }
      else if(filetype === 'application/x-zip-compressed')      // check the file is of zip format.
      {
        this.appFileFormatCheck = true;
        this.fileSize = false;
        this.AppFile = event.target.files[0];
        this.validAppFile = true;
      }
      else
      {
        this.appFileFormatCheck = false;
        event.target.value = '';
        this.fileSize = false;
      }     
    }
  }
  // validating the App screenshots
  public multipleFiles(event: any): void {
    const files: FileList = event.target.files;
    if (files.length > 3 || files.length <= 1) {      // file Length not more than 3 or less than 2.
      this.multipleFile = true;
      event.target.value = '';
      return;
    }
    this.multipleFile = false;
    for (let i = 0; i < files.length; i++) {
      if (files[i] && ['image/jpeg', 'image/png'].includes(files[i].type)) {      // file format should be image
        this.appImages.push(files.item(i)!);
        this.screenshotFileFormatCheck = false;
        this.validScreenShots = true;

      } else {
        this.screenshotFileFormatCheck = true;
        event.target.value ='';
      }
    }
  }

  // validating the App Logo.
  public handleLogo(event: any): void {
    this.Logo = event.target.files[0];
    if (this.Logo && ['image/jpeg', 'image/png'].includes(this.Logo.type)) {      // file should be of Image format.
      this.logoFileFormatCheck = false;
      this.validLogo = true;
    } else {
      this.logoFileFormatCheck = true;
      event.target.value = '';
    }
  }
 
  // App Upload Form Submission.
  public onSubmit() {
    if (this.appData.valid && this.validAppFile && this.validLogo && this.validScreenShots) {
      const userData =  this.loginService.getUserId();      // fetching userId from Tokens.
      const formData = new FormData();
      formData.append('UserId', userData);
      formData.append('Logo', this.Logo!);
      formData.append('AppFile', this.AppFile!);
      formData.append('Name', this.appData.get('Name')?.value);
      formData.append('CategoryId', this.appData.get('CategoryId')?.value);
      formData.append('PublisherName', this.appData.get('PublisherName')?.value);
      formData.append('Description', this.appData.get('Description')?.value);
      for (let i = 0; i < this.appImages.length; i++) {
        formData.append('appImages', this.appImages[i]);
      }
      this.service.postApplication(formData).subscribe({
        next: (response : any) => {
          if(response === 400)      // 400 --> Returns if the The App already Exists.
          {
            this.toastr.error('App Name already exist , Try giving different Name');
          }
          else{
          this.toastr.success('Form Submitted',);
          this.service.setIsDeveloper(true);
          this.router.navigate(['user/my-apps']);
          }
        },
        error: (error) => {
          this.toastr.error('Application not uploaded');
        },
        complete:()=>{
          this.appData.reset();
        }
      });
    }
    else {
      this.toastr.error('Incorrect File Submission');
    }
  }
}
                            