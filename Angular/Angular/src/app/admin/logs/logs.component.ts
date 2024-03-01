import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';


@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit{
  
  usernameFilter: string = '';
  dateFilter: string ='';
  appFilter: string = '';
  
  users = [
      { Username: 'Nithish', Date: '2024-03-01', AppName: 'Instagram' },
      { Username: 'kumar', Date: '2024-03-01', AppName: 'twitter' },
      { Username: 'dani', Date: '2024-03-01', AppName: 'calculator' },
      { Username: 'arun', Date: '2024-03-01', AppName: 'Instagram' },
      { Username: 'ashiq', Date: '2024-03-01', AppName: 'whatsapp' },
      { Username: 'Bala', Date: '2024-02-23', AppName: 'whatsapp' },
      { Username: 'Ashok', Date: '2024-02-24', AppName: 'Instagram' },
      { Username: 'Kavya', Date: '2024-02-23', AppName: 'whatsapp' },
      { Username: 'Lokesh', Date: '2024-02-23', AppName: 'Calculator' },
      { Username: 'Bharath', Date: '2024-02-23', AppName: 'whatsapp' },
      { Username: 'Kishore', Date: '2024-02-23', AppName: 'twitter' },
      { Username: 'ashiq', Date: '2024-02-24', AppName: 'whatsapp' },
      { Username: 'ashiq', Date: '2024-02-24', AppName: 'whatsapp' }
      // Add more user data here
  ];

  filteredUsers: any[] = this.users; // Initialize with all users

  ngOnInit(): void {
    
   
    this.dateFilter = new Date().toISOString().split('T')[0];
    this.applyFilters();
  }


  applyFilters() {
      this.filteredUsers = this.users.filter(user =>
          (!this.usernameFilter || user.Username.toLowerCase().includes(this.usernameFilter.toLowerCase())) &&
          (!this.dateFilter || user.Date === this.dateFilter) &&
          (!this.appFilter || user.AppName.toLowerCase().includes(this.appFilter.toLowerCase()))
      );
  }

// Pagination variables
pageSize = 5; // Initial page size
pageIndex = 0; // Initial page index
pagedUsers: any[] = []; // Data for the current page

constructor() {
  this.setPageData();
}

onPageChange(event: any) {
  this.pageIndex = event.pageIndex;
  this.pageSize = event.pageSize;
  this.setPageData();
}

setPageData() {
  const startIndex = this.pageIndex * this.pageSize;
  this.pagedUsers = this.filteredUsers.slice(startIndex, startIndex + this.pageSize);
}


}
