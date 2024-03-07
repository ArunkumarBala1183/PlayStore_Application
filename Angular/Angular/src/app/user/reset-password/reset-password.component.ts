import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent {


  changePasswordForm : FormGroup;

  constructor(private formBuilder : FormBuilder){
    this.changePasswordForm = this.formBuilder.group({
      password : ['',Validators.required],
      confirmPassword : ['',[Validators.required,Validators.minLength(8)]]
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
      alert("Form Submitted");     
    }
    else{
      alert("Enter all the details");
    }
  }
}
