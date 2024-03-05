import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AppLogs } from 'src/app/interface/app-logs';
import { LogFilter } from 'src/app/interface/log-filter';
import { AppInfoService } from 'src/app/services/app-info.service';




@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {
  currentPage = 1;
  pageSize = 10; // Adjust as needed
  totalItems = 0;
  

  filterDetails : LogFilter = {
    appName : '',
    downloadedDate : '',
    fromDate : '',
    userName : ''
  }

  appLogDetails! : AppLogs[]

  isLogFound : boolean = false

  errorMessage : string = ''
  

  // users = [
  //   { Username: 'Nithish', Date: '2024-03-01', AppName: 'Instagram' },
  //   { Username: 'kumar', Date: '2024-03-01', AppName: 'twitter' },
  //   { Username: 'dani', Date: '2024-03-01', AppName: 'calculator' },
  //   { Username: 'arun', Date: '2024-03-01', AppName: 'Instagram' },
  //   { Username: 'ashiq', Date: '2024-03-01', AppName: 'whatsapp' },
  //   { Username: 'Bala', Date: '2024-03-23', AppName: 'whatsapp' },
  //   { Username: 'Ashok', Date: '2024-03-24', AppName: 'Instagram' },
  //   { Username: 'Kavya', Date: '2024-03-23', AppName: 'whatsapp' },
  //   { Username: 'Lokesh', Date: '2024-02-23', AppName: 'Calculator' },
  //   { Username: 'Bharath', Date: '2024-02-23', AppName: 'whatsapp' },
  //   { Username: 'Kishore', Date: '2024-02-23', AppName: 'twitter' },
  //   { Username: 'ashiq', Date: '2024-02-23', AppName: 'whatsapp' },
  //   { Username: 'ashiq', Date: '2024-02-23', AppName: 'whatsapp' }
  //   // Add more user data here
  // ];

  // filteredUsers: any[] = this.users; // Initialize with all users

  ngOnInit(): void {
    this.filterDetails.downloadedDate = new Date().toISOString().split('T')[0];
    this.getAppLogs()
    // this.applyFilters();
  }


  
  

  // // Pagination variables
  // pageSize = 5; // Initial page size
  // pageIndex = 0; // Initial page index
  // pagedUsers: any[] = []; // Data for the current page

  constructor(private service : AppInfoService , private toastr : ToastrService) {
   
  }

  

  getAppLogs()
  {
    this.service.getAppLogs(this.filterDetails)
    .subscribe({
      next : response => {
        this.isLogFound = true
        this.appLogDetails = response.body as AppLogs[]
      },
      error : error => {
        this.isLogFound = false
        this.errorMessage = error.error.message
      }
    })
  }

  
  


}
