import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { ApplicationLogs } from 'src/app/interface/application-logs';
import { AppInfoService } from 'src/app/services/app-info.service';

@Component({
  selector: 'app-appliationlogs',
  templateUrl: './appliationlogs.component.html',
  styleUrls: ['./appliationlogs.component.scss']
})
export class AppliationlogsComponent implements OnInit {

  displayedColumns: string[] = ['userId','timeStamp','level', 'location','message','exception'];
  appLogSource = new MatTableDataSource<any>();

  appLogDetails!: ApplicationLogs[]
  isDropdownOpen: boolean = false

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private service: AppInfoService , private router : Router) { }

  ngOnInit(): void {
    this.getAppLogs();
  }

  public getAppLogs() {
    this.service.getlogDetails()
      .subscribe({
        next: response => {
          this.appLogDetails = response.body as ApplicationLogs[]
          this.appLogSource = new MatTableDataSource<ApplicationLogs>(this.appLogDetails)
          this.appLogSource.paginator = this.paginator
        },
        error: error => {
          console.log(error)
        }
      })
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  goTOAppDownloads()
  {
    this.router.navigate(["admin/dashboard"]);
  }

  goTOLogPage()
  {
    this.router.navigate(["admin/applicationLogs"]);
  }

  goToUser() {
    this.router.navigate(["admin/users"]);
    }
}
