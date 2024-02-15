import { Component, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { App } from '../../interface/app';
import { AppService } from '../../services/app.service';
import { DialogComponent } from '../../shared/components/dialog/dialog.component';

@Component({
  selector: 'app-app-list',
  templateUrl: './app-list.component.html',
  styleUrls: ['./app-list.component.scss']
})
export class AppListComponent implements OnInit {

  public appList: App[] = [];
  displayedColumns: string[] = ['id', 'name', 'description', 'developer', 'price', 'addedOn', 'actions'];
  constructor(private appService: AppService, private router: Router, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.appService.getApp().subscribe((res: App[]) => {
      if (res) {
        this.appList = res;
      }
    });
  }
  public redirectToCreate() {
    this.router.navigate(['app/create'])
  }
  public redirectToEdit(element: App) {
    this.appService.selectedApp = element;
    this.router.navigate(['app/edit', element.id])
  }
  public redirectToView(element: App) {
    this.appService.selectedApp = element;
    this.router.navigate(['app/view'])
  }
  public delete(element: App) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        title: "Warning", message: "Are you sure to delete the " + element.name, buttonName: "Proceed"
      },
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.appList = this.appList.filter(obj => obj !== element);
    });
  }
}
