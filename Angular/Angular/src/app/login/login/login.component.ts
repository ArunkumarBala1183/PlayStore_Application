import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/services/login.service';
import { redirect } from 'src/app/shared/routings/redirect';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginData: FormGroup | any;
  public userData: any;
  registerRedirect = redirect.registerRedirect;
  forgotPasswordRedirect = redirect.forgotPasswordRedirect;


  constructor(public router: Router, public formbuilder: FormBuilder, private route: ActivatedRoute, public datepipe: DatePipe, private loginService: LoginService, private toastr: ToastrService) {

  }

 public getEmail(): void {
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
    // const currentDate: Date = new Date();
    // console.log(currentDate);
    // const formattedDate = this.datepipe.transform(currentDate);
    // console.log(formattedDate);
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
      this.loginService.verifyLogin(this.loginData.value).subscribe(
        {
          next: (response: any) => {
            // console.log(response);
            // console.log(response.accessToken);
            const accessToken = response.accessToken;
            // console.log(accessToken);
            localStorage.setItem('accessToken', accessToken);
            this.toastr.success('Login Successful');
            const role = this.getRole();
            if (role !== null) {

              if (role === 'User') {
                this.router.navigate(['user']);

              }
              else {
                this.router.navigate(['admin'])

              }



            }
            else {
              return;
              this.toastr.info('Unable to fetch role .Please Login')
            }
          },
          error: error => {
            this.toastr.error('Incorrect Email or Password')
            // console.log(error);
            return;
          }
        }
      )

    }


  }
 public getRole() {
    const role = this.loginService.getUserRole();
    if (role !== null) {
      return role;
    }
    else {
      return null;
    }
  }
 public login() {
    this.router.navigate(['app']);
  }
}


