import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../../services/login.service';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit{

    
  userId !: Guid;
  changePasswordForm !: FormGroup;

  constructor(private formBuilder : FormBuilder, private loginService : LoginService, private service : UserService, private toastr : ToastrService, private router : Router){
    
  }
  ngOnInit(): void {
    this.initForm();
  }
initForm(){
  this.changePasswordForm = this.formBuilder.group({
    password : ['' , Validators.required],
    newPassword : ['',[Validators.required]],
    confirmPassword : ['',Validators.required]
  })
}
  public checkPassword():boolean
  {
    const newPassword = this.changePasswordForm.get('newPassword')?.value;
    const confirmPassword = this.changePasswordForm.get('confirmPassword')?.value;
    return newPassword === confirmPassword;
  }


  isPasswordMatch = false;
  public checkingPassword(event : any)
  {
    this.userId = this.loginService.getUserId();
    const existingPassword = event.target.value;
    console.log(existingPassword);
    this.service.getPassword(this.userId , existingPassword).subscribe({
      next : response => {
        if(response)
        {
          this.isPasswordMatch = true;
        }
        else {
          this.isPasswordMatch = false;
          this.toastr.info('Password doesnot Match')
        }
      },
      error : error => {
        console.error(error);
      }
    })
  }

  public onSubmit() {
    if(this.changePasswordForm.valid && this.isPasswordMatch){
      this.userId = this.loginService.getUserId();
      const newPassword = this.changePasswordForm.get('newPassword')?.value;
      this.service.patchPassword(this.userId , newPassword).subscribe({
        next:( response:any) => {
          console.log(response);
          if(response.message ==true)
          {
            this.toastr.success('Password Changed');
            this.router.navigate(['user/user-profile']);
          }
          else{
            this.toastr.info("Password already Exists")
          }
         
        },
        error : error =>
        {
          console.error(error);
        }
      });
    }
   
  }
}
