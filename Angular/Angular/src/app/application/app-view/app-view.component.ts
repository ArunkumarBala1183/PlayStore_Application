import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { App } from '../../interface/app';
import { AppService } from '../../services/app.service';

@Component({
  selector: 'app-app-view',
  templateUrl: './app-view.component.html',
  styleUrls: ['./app-view.component.scss']
})
export class AppViewComponent implements OnInit {

  public selectedApp: App;

  constructor(private appService: AppService, private router: Router) {
    this.selectedApp = this.appService.selectedApp;
  }

  ngOnInit(): void {
    this.selectedApp = this.appService.selectedApp;
  }
  redirectToCancel() {
    this.router.navigate(['app']);
  }
}
