import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { App } from '../../interface/app';
import { AppService } from '../../services/app.service';

@Component({
  selector: 'app-app-create',
  templateUrl: './app-create.component.html',
  styleUrls: ['./app-create.component.scss']
})
export class AppCreateComponent implements OnInit {

  public selectedApp: App = {} as App;
  public action: string = '';
  private editMode: boolean = false;
  constructor(private router: Router, private route: ActivatedRoute,
    private appService: AppService) { }

  ngOnInit(): void {
    this.editMode = this.route.snapshot.paramMap.get('id') != null;
    if (this.editMode) {
      this.selectedApp = this.appService.selectedApp;
      this.action = 'Edit';
    }
    else {
      this.action = 'Create';
    }
  }
  redirectToCancel() {
    this.router.navigate(['app']);
  }
  save() {
    console.log(this.selectedApp);
  }
}
