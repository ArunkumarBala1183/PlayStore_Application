import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AllAppsInfo, CategoryInfo } from 'src/app/interface/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-developer-new-app',
  templateUrl: './developer-new-app.component.html',
  styleUrls: ['./developer-new-app.component.scss']
})
export class DeveloperNewAppComponent implements OnInit {

    // public uploadApp:FormGroup | any;
    public appData !: FormGroup;
    fileSize=false;
    multipleFile=false;
    category : CategoryInfo[] = [];
    appInfo : AllAppsInfo[] = [];
    AppFile : File | null = null;
    Logo : File | null = null;
    appImages : File[] = [];

    logoFileFormatCheck = false;
    screenshotFileFormatCheck = false;
    appFileFormatCheck = false;

    constructor(public router:Router, public formbuilder:FormBuilder, private service : UserService)
    {}
    ngOnInit(): void {
      this.initForm();
      this.service.getCategory().subscribe(
        {
          next : response =>{
            this.category = response;
            console.log(this.category);
            console.log(this.category.length);

          }
        })
        this.service.getAllApps().subscribe(
          {
            next : response => 
            {
              this.appInfo = response;
            },
            error : error => {
              console.log(error);
            }
          })
    }
    initForm()
    {
      this.appData=this.formbuilder.group(
        {
          Name:['',[Validators.required,Validators.maxLength(5)]],
          CategoryId:['',Validators.required],
          PublisherName:['', Validators.required],
          Description:['', Validators.required],
          Logo:[null,Validators.required],
          // Logo : [this.Logo],
          AppFile:[null,Validators.required],
          appImages:[null,Validators.required]
        }
      )
    }
    validationMessages={
      Name:{
        required:'App Name is required'
      },
      CategoryId:{
        required:'Required '
      },
      PublisherName:{
        required:'Required '
      },
      Description:{
        required:'Description for App is Required'
      },
      Logo:{
        required:'App Logo is Required'
      },
      AppFile:{
        required:'App File is Required'
      },
      appImages:
      {
        required:'Screenshot of App is Required'
      }
    }

    handleFile(event:any):void
    {
      const files = event.target.files[0];
      if (files) {
        const file = files;
        const maxFileSize = 2 * 1024 * 1024; // 2 MB
        if (file.size > maxFileSize) {
          this.fileSize=true;
          console.log('File exceeds more than 2mb');
          event.target.value='';
          return;
            };
            this.fileSize=false;
            this.AppFile = event.target.files[0];
            const filetype = this.AppFile?.type;
            if(filetype !== 'application/zip')
            {
              this.appFileFormatCheck = true;
              event.target.value = '';
            }
            else
            {
              this.appFileFormatCheck = false;
            }
            console.log(this.AppFile);      
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
      for(let i = 0; i < files.length; i++)
      {
        if(files[i] && ['image/jpeg' , 'image/png'].includes(files[i].type))
        {
          this.appImages.push(files.item(i)!);    
          this.screenshotFileFormatCheck = false;
        }
        else{
          this.screenshotFileFormatCheck = true;
        }
      }
      console.log(this.appImages);
    }
    // categories=Array('social','entertainment');
     
    handleLogo(event : any) : void {
      this.Logo = event.target.files[0];
      if(this.Logo && ['image/jpeg', 'image/png'].includes(this.Logo.type))
      {
        this.logoFileFormatCheck = false;
      }
      else {
      this.logoFileFormatCheck = true;
      event.target.value = '';
      }
      console.log(this.Logo);      
    }
    onSubmit()
    {
      if(this.appData.valid)
      {
        // const formData = this.appData.value;
        // const userId = this.appInfo[0].userId;
        // const categoryId = this.appData.get('CategoryId')?.value;
        // const selectedCategorycategory = this.category.find(obj => obj.categoryId == categoryId);
        // const categoryName = selectedCategorycategory?.categoryName;
        // console.log(categoryName);
        // console.log(categoryId);        
        // formData.UserId = userId;
        // formData.categoryName = categoryName;
        //console.log(this.appImages);
        // formData.AppFile = this.AppFile;
        // formData.Logo = this.Logo;
        // formData.appImages = this.appImages;
        // console.log(formData);
        // formData.additionalValue = 
  //       this.service.postApplication(formData).subscribe(
  //         {
  //           next : respose => {
  //             console.log('form submitted',respose);
  //           },
  //           error : error =>
  //           {
  //             console.log(error);
  //           }
  //         }
  //       )
  //     }  
  //   }
  // }


         const userId = this.appInfo[0].userId;
         
        const formData = new FormData();
        formData.append('UserId' , userId.toString());

        formData.append('Logo' , this.Logo!);
        formData.append('AppFile' , this.AppFile!);
        formData.append('Name' , this.appData.get('Name')?.value);
        formData.append('CategoryId' , this.appData.get('CategoryId')?.value);
        formData.append('PublisherName' , this.appData.get('PublisherName')?.value);
        formData.append('Description' , this.appData.get('Description')?.value);
        for(let i = 0; i < this.appImages.length ; i ++)
        {
          formData.append('appImages' , this.appImages[i]);
        }
        console.log(formData);
        
        // const UserId = this.appInfo[0].userId
        this.service.postApplication(formData).subscribe(
          {
            next : response => {
              alert("Form Submitted");
            },
            error : error => {
              console.log(error);

            }
          }
        )
      }
    }
  }