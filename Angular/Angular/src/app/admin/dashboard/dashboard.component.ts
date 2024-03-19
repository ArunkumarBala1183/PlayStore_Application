import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { AppInfoService } from 'src/app/services/app-info.service';
import { HttpStatusCode } from '@angular/common/http';
import {  ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GetUsers } from 'src/app/interface/get-users';
import { SearchUser } from 'src/app/interface/search-user';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

//dashboard
  responseDetails : any
  dates! : string[]
  count! : number[]
  isDropdownOpen = false;

//Users
  searchUsers: SearchUser = {searchDetails : ''};
  displayedColumns: string[] = ['name', 'emailId' , 'userRoles'];
  dataSource = new MatTableDataSource<any>();
  errorMessage : string = ''
  isUserFound : boolean = true
  userDetails : GetUsers[] = []
  // data:any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  ngOnInit(): void {
    this.getAppDownloads();
    this.getAllUsers();
  }

  constructor(private service: AppInfoService,private router : Router, private services: UserService) { }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  
  getUserDetails() {
    this.services.getUserDetails(this.searchUsers)
    .subscribe({
      next : response => {
        this.isUserFound = true
        this.userDetails = response.body as GetUsers[]
        this.dataSource = new MatTableDataSource<GetUsers>(this.userDetails); // Specify the type here
        this.dataSource.paginator = this.paginator;
      },
      error: error => {
        this.isUserFound = false
        this.errorMessage = error.error.message
      }
    })
  }

  getAllUsers()
  {
    this.services.getAllUsers()
    .subscribe({
      next : response => {
        if(response.status == HttpStatusCode.Ok)
        {
            this.userDetails = response.body as GetUsers[]
            this.dataSource = new MatTableDataSource<GetUsers>(this.userDetails); // Specify the type here
            this.dataSource.paginator = this.paginator;
        }
        else
        {
          console.log(response.body)
        }
      },
      error : error => {
        console.log(error)
      }
    })
  }

  createChart() {
    const dates = this.generateDates(); // Function to generate dates
    const ctx = document.getElementById('appDownloadsChart') as HTMLCanvasElement;
    const myChart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: dates,
        datasets: [{
          label: 'No of apps downloaded in a day',
          data: this.count,
          backgroundColor: [
            'rgba(153, 102, 255, 1)', // Purple // Red
            'rgba(153, 102, 255, 1)', // Purple // Blue
            'rgba(153, 102, 255, 1)', // Purple // Yellow
            'rgba(153, 102, 255, 1)', // Purple // Green
            'rgba(153, 102, 255, 1)', // Purple
            'rgba(153, 102, 255, 1)',
            'rgba(153, 102, 255, 1)', // Purple // Orange
          ],
          
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 0
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
  generateDates(): string[] {
    const today = new Date();
    const dates = [];
    for (let i = 6; i >= 0; i--) {
        const day = new Date(today);
        day.setDate(today.getDate() - i);
        const dayOfMonth = day.getDate();
        let suffix = "";
        if (dayOfMonth === 1 || dayOfMonth === 21 || dayOfMonth === 31) {
            suffix = "st";
        } else if (dayOfMonth === 2 || dayOfMonth === 22) {
            suffix = "nd";
        } else if (dayOfMonth === 3 || dayOfMonth === 23) {
            suffix = "rd";
        } else {
            suffix = "th";
        }
        const month = day.toLocaleDateString('en-GB', { month: 'long' }); // Get full month name
        const formattedDate = `${dayOfMonth}${suffix} ${month}`; // Format: DDDth MONTH
        dates.push(formattedDate);
        
    }
    return dates;
    
}


  getAppDownloads() {
    this.service.getAllAppDownloadDetails()
      .subscribe({
        next: response => {
          this.responseDetails = response.body

          // this.dates = this.responseDetails.dates as string[]
          this.count = this.responseDetails.count

          this.createChart();
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


