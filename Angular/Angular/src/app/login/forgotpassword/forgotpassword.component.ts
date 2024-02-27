import { Component } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.scss']
})
export class ForgotpasswordComponent {
  constructor(private router:Router , private loginService:LoginService){

  }
  forgotPassword:NgForm|any;
  emailId:string='';
  otp:string='';
  newPassword:string='';
  confirmPassword:string='';
  emailVerify=false;
  otpVerify=false;
  password=false;
  showOtp=false;
  public passwordCheck()
  {
    if(this.newPassword.length>8)
    {
      return true;
    }
    return false;
  }
  public passwordChange()
  {
    if(this.newPassword === this.confirmPassword)
    {
      return false;
    }
    return true;
  }
  otpCheck()
  {
    
    if(this.otp.length>6 || this.otp.length<6 || isNaN(+this.otp))
    {
      return true;
    }
    return false;

  }
  onSubmit(form: NgForm) {
    if(form.valid)
    {
      if (!this.emailVerify) {
        console.log(this.emailId);
        const email=this.emailId;
        this.loginService.checkUser;
        this.emailVerify =true;
        this.showOtp=true;
      }
      else 
      {
        if (!this.otpVerify) {
          this.otpVerify = true;
          this.showOtp=!this.showOtp;
          console.log(this.otp);
  
        }
        else{
          console.log('Email:', this.emailId);
          console.log('OTP:', this.otp);
          console.log('Password:', this.newPassword);
          this.router.navigate(['login']);
          alert('Password Changed');
          form.reset();

          this.emailVerify = false;
          this.otpVerify = false;
      }

      }

      
   
    }
  }
  }

