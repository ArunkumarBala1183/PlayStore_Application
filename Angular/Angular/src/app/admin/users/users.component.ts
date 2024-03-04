import { HttpStatusCode } from '@angular/common/http';
import { Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { GetUsers } from 'src/app/interface/get-users';
import { UserService } from 'src/app/services/user.service';
// import { error } from 'console';
// import { MyApiService } from 'src/app/my-api.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})

export class UsersComponent implements OnInit {

  searchUsers: string = '';
  displayedColumns: string[] = ['name', 'emailId' , 'userRoles'];
  dataSource = new MatTableDataSource<any>();

  userDetails : GetUsers[] = []
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

  constructor(private service : UserService ){}

  ngOnInit() {
     this.getAllUsers();
  }
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  
  applyFilter() {
    this.dataSource.filter = this.searchUsers.trim().toLowerCase();
  }

  getAllUsers()
  {
    this.service.getAllUsers()
    .subscribe({
      next : response => {
        if(response.status == HttpStatusCode.Ok)
        {
            this.userDetails = response.body as GetUsers[]
            console.log(this.userDetails)
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
  

  // get filteredUsers() {
  //   return this.users.filter(user => user.Username.toLowerCase().includes(this.searchUsers.toLowerCase()) || user.Role.toLowerCase().includes(this.searchUsers.toLowerCase()));
  // }
}
