import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent {

  constructor(private router : Router) {
  }

logout() {
  this.router.navigate([''])
}
navbar = false;
showMenu() {
this.navbar = !this.navbar;
}

}
