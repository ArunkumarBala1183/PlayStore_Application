import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})


export class ChangePasswordComponent{
 changePasswordForm : FormGroup;
 
  constructor(private formBuilder : FormBuilder, private loginService : LoginService, private service : UserService, private toastr : ToastrService){
    this.changePasswordForm = this.formBuilder.group({
      password : ['',Validators.required],
      confirmPassword : ['',Validators.required]
    })
  }
 
  public checkPassword():boolean
  {
    const password = this.changePasswordForm.get('password')?.value;
    const confirmPassword = this.changePasswordForm.get('confirmPassword')?.value;
    return password === confirmPassword;
  }
 
 
  public onSubmit() {
    if(this.changePasswordForm.valid){
      const userId = this.loginService.getUserId();
      const password = this.changePasswordForm.get('password')?.value;
      this.service.postPassword(userId , password).subscribe({
        next: (response : any) => {
          // if(response == 'Password Changed Successfully')
          console.log(response);
          this.toastr.success('Password Changed',);
          console.log(response);
        },
        error : error =>
        {
          console.error(error);
        },
        complete:()=>{
          this.changePasswordForm.reset();
        }
      });
    }
   
  }
}