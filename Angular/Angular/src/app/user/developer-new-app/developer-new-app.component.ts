import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-developer-new-app',
  templateUrl: './developer-new-app.component.html',
  styleUrls: ['./developer-new-app.component.scss']
})
export class DeveloperNewAppComponent implements OnInit {

    public uploadApp:FormGroup | any;
    public appData:any;
    fileSize=false;
    multipleFile=false;
    constructor(public router:Router, public formbuilder:FormBuilder)
    {}
    ngOnInit(): void {
      this.initForm();
    }
    initForm()
    {
      this.appData=this.formbuilder.group(
        {
          appName:['',Validators.required],
          category:['',Validators.required],
          publisherName:['', Validators.required],
          description:['', Validators.required],
          appLogo:[null,Validators.required],
          appFile:[null,Validators.required],
          screenshot:[null,Validators.required]
        }
      )
    }
    validationMessages={
      appName:{
        required:'App Name is required'
      },
      category:{
        required:'Required '
      },
      publisherName:{
        required:'Required '
      },
      desription:{
        required:'Description for App is Required'
      },
      appLogo:{
        required:'App Logo is Required'
      },
      appFile:{
        required:'App File is Required'
      },
      screenshot:
      {
        required:'Screenshot of App is Required'
      }
    }
    // appFileValidator(control: AbstractControl) {
    //   const files: FileList = control.value;
    //   if (files && files.length > 0) {
    //     const file = files[0];
    //     const maxFileSize = 2 * 1024 * 1024; // 2 MB
    //     if (file.size > maxFileSize) {
    //       return { invalidFileSize: true };
    //     }
    //   }
    //   return null;
    // }
    handleFile(event:any):void
    {
      const files: FileList = event.target.files;
      if (files && files.length > 0) {
        const file = files[0];
        const maxFileSize = 2 * 1024 * 1024; // 2 MB
        if (file.size > maxFileSize) {
          this.fileSize=true;
          console.log('File exceeds more than 2mb');
          event.target.value='';
          return;
            };
            this.fileSize=false;
        }
    }
    multipleFiles(event:any) :void{
      const files:FileList=event.target.files;
      if(files.length>3 || files.length<=1)
      {
        this.multipleFile=true;
        event.target.value='';
        return;
      }
      this.multipleFile=false;
    }
    categories=Array('social','entertainment');
     
    onSubmit()
    {
     
    }
    }

