import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit  {
minDate:Date | undefined;
 constructor(public formbuilder:FormBuilder , public router:Router ,private loginService:LoginService){
  this.minDate = new Date();
 this.minDate.setFullYear(this.minDate.getFullYear() - 18);
 }
 public register:FormGroup | any;
  ngOnInit(): void {
    this.initForm();
  }
  initForm()
  {
    this.register=this.formbuilder.group(
      {
       userName:['',Validators.required],
       emailId:['',[Validators.required,Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$')]],
       mobile:['',[Validators.required,Validators.maxLength(10)]],
       password:['',[Validators.required,Validators.minLength(8)]] ,
       confirmPassword:['',[Validators.required,Validators.minLength(8)]],
       dob:['',Validators.required]

      }
    )
  }
  validationMessages={
    userName:{
     required:'Username is Required'
    },
    emailId:{
      required:'Email is Required',
      email:'Please enter valid email address'
    },
    mobile:
    {
      required:'Mobile Number is Required',
      valid:'Entire valid Mobile Number '
    },

    password:{
      required:'Password is required !!!!',
      minlength: 'Password must contain at least 8 characters  one uppercase letter, one lowercase letter, one number and one special case'
    },
    confirmPassword:
    {
      required:'Password and Confirm password must be same'
    },
    dob:
    {
      required:'Date of Birth is Required'
    }
   }
   passwordMatch():boolean{
     const password=this.register.get('password').value;
     const confirmPassword=this.register.get('confirmPassword').value;
     return password===confirmPassword;
   }
   checkEmail(event:any):void{
    console.log(event.target.value);
    const email=event.target.value;
    this.loginService.checkUser(email);
    
   }
   validDob = false;

checkDob(event: any): void {
  const dobString: string = event.target.value;
  const dob: Date = new Date(dobString);
  
  if (dob > this.minDate!) {
    console.log(this.minDate)
    console.log(dob);
    this.validDob = true;
  } else {
    console.log(this.minDate)
    console.log(dob);
    this.validDob = false;
  }
}
onSubmit()
{
 if(this.register.valid)
 {
  console.log(this.register.value)
  const email=this.register.controls.emailId.value;
  console.log(email);
  this.loginService.addUser(this.register.value)
  alert('Register Success.Enter Password to Login');
  this.router.navigate(['login'],{queryParams:{emailId:email}});
 }
 else
 {
  alert('Please fill the input fields correctly');
 }

}
}
