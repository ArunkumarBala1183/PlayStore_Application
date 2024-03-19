import { Component } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmailExists } from 'src/app/interface/login';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.scss'],
})
export class ForgotpasswordComponent {
  constructor(
    private router: Router,
    private loginService: LoginService,
    private toastr: ToastrService
  ) {}
  forgotPassword: NgForm | any;
  emailId: string = '';
  otp: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  emailVerify = false;
  otpVerify = false;
  password = false;
  showOtp = false;
  public passwordCheck() {
    if (this.newPassword.length > 8) {
      return true;
    }
    return false;
  }
  public passwordChange() {
    if (this.newPassword === this.confirmPassword) {
      return false;
    }
    return true;
  }
  public otpCheck() {
    if (this.otp.length > 6 || this.otp.length < 6 || isNaN(+this.otp)) {
      return true;
    }
    return false;
  }
  emailSuccess: any;

  onSubmit(form: NgForm) {
    if (form.valid) {
      if (!this.emailVerify) {
        const emailExists: EmailExists = { emailId: this.emailId };
        this.toastr.info('Otp sending to your Email');
        this.loginService.forgotPassword(emailExists).subscribe({
          next: (response) => {
            this.toastr.success('Otp Sent to your Email');
            this.emailSuccess = response;
           
            this.emailVerify = true;
            this.showOtp = true;
          },
          error: (error) => {
            this.toastr.error('Incorrect EmailId');
          
            return;
          },
        });
      } else {
        if (!this.otpVerify) {
          
          this.loginService.verifyOtp(form.value).subscribe({
            next: (response) => {
             
              this.otpVerify = true;
              this.showOtp = !this.showOtp;
              this.toastr.info('Enter Your New Password');
            },
            error: (error) => {
              
              this.toastr.warning('Incorect otp');
              return;
            },
          });
        } else {
         
          this.loginService.changePassword(form.value).subscribe({
            next: (response) => {
            
              this.toastr.success('Password Changed Successfully');
              this.router.navigate(['login']);
              form.reset();
              this.emailVerify = false;
              this.otpVerify = false;
            },
            error: (error) => {
              this.toastr.error('Password confirm password doesnot match ');
            },
          });
        }
      }
    }
   
  }
}
