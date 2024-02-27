import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginData: FormGroup | any;
  public userData: any;
  registerRedirect = environment.registerRedirect;
  forgotPasswordRedirect = environment.forgotPasswordRedirect;

  constructor(public router: Router, public formbuilder: FormBuilder, private route: ActivatedRoute, public datepipe: DatePipe, private loginService: LoginService) {

  }

  getEmail(): void {
    this.route.queryParams.subscribe(params => {

      if (params.emailId) {
        this.loginData.patchValue({

          emailId: params.emailId

        });

      }
    });
  }

  validationMessages = {
    emailId: {
      required: 'Email is Required',
      email: 'Please enter valid email address'
    },
    password: {
      required: 'Password is required !!!!',
    }
  }
  ngOnInit(): void {
    this.initForm();
    this.getEmail();
    const currentDate: Date = new Date();
    console.log(currentDate);
    const formattedDate = this.datepipe.transform(currentDate);
    console.log(formattedDate);
  }
  initForm() {
    this.loginData = this.formbuilder.group(
      {
        emailId: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]]
      }
    )
  }

  onSubmit() {
    if (this.loginData.valid) {
      console.log(this.loginData.value);
      this.loginService.verifyLogin(this.loginData.value)
      alert('Login Successful');
      this.router.navigate(['user']);
    }


  }
  login() {
    this.router.navigate(['app']);
  }
}
