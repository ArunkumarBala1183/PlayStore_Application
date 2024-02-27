import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router) { }
  public userName: string = '';
  public password: string = '';
  public showSpinner: boolean = false;

  ngOnInit(): void {
    console.log("Login component came")
  }

  login() {
    // alert("Login triggered")

    this.router.navigate(["admin/dashboard"])
  }
}
