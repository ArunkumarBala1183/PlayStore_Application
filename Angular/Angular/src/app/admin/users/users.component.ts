import { Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
// import { error } from 'console';
// import { MyApiService } from 'src/app/my-api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
 /**
  *
  */
//  constructor(private myService:MyApiService) {
  
//  }
  searchUsers: string = '';
  displayedColumns: string[] = ['Username', 'Email', 'Role'];
  dataSource = new MatTableDataSource<any>();
  // data:any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  // Sample user data
  users = [
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'prem', Email: 'prem632@gmail.com', Role: 'dev' },
    { Username: 'bharath', Email: 'bharath@gmail.com', Role: 'dev' },
    { Username: 'arun', Email: 'arun@gmail.com', Role: 'dev' },
    { Username: 'Ashiq', Email: 'ashiq@gmail.com', Role: 'User' },
    { Username: 'daniel', Email: 'daniel@gmail.com', Role: 'dev' },
    { Username: 'vinoth', Email: 'vinoth@gmail.com', Role: 'User' },
    { Username: 'Pradeep', Email: 'pradeep@gmail.com', Role: 'dev' },
    { Username: 'dhivyan', Email: 'dhivya@gmail.com', Role: 'User' },
    { Username: 'arunkumar', Email: 'arukumar@gmail.com', Role: 'dev' },
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'kumar', Email: 'nithishkumar632@gmail.com', Role: 'dev' },
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'kumar', Email: 'nithishkumar632@gmail.com', Role: 'dev' },
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'kumar', Email: 'nithishkumar632@gmail.com', Role: 'dev' },
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'kumar', Email: 'nithishkumar632@gmail.com', Role: 'dev' },
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'kumar', Email: 'nithishkumar632@gmail.com', Role: 'dev' },
    { Username: 'Nithish', Email: 'nithishkumar632@gmail.com', Role: 'User' },
    { Username: 'kumar', Email: 'nithishkumar632@gmail.com', Role: 'dev' },
    // Add more user objects as needed
  ];

  ngOnInit() {
    // Initialize dataSource after users array is initialized
     this.dataSource = new MatTableDataSource<any>(this.users);
    // this.myService.getAllUsers().subscribe(
    //   {
    //     next:response=>
    //     {
    //       console.log(response);
    //       this.data=response;
    //     },
    //     error:error=>
    //     {
    //       console.log(error)
    //     }
    //   }
    // )
  }
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  // Update table data based on search
  applyFilter() {
    this.dataSource.filter = this.searchUsers.trim().toLowerCase();
  }
  

  // get filteredUsers() {
  //   return this.users.filter(user => user.Username.toLowerCase().includes(this.searchUsers.toLowerCase()) || user.Role.toLowerCase().includes(this.searchUsers.toLowerCase()));
  // }
}