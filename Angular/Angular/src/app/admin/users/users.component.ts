import { HttpStatusCode } from '@angular/common/http';
import { Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { GetUsers } from 'src/app/interface/get-users';
import { SearchUser } from 'src/app/interface/search-user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})

export class UsersComponent implements OnInit {

  searchUsers: SearchUser = {searchDetails : ''};
  displayedColumns: string[] = ['name', 'emailId' , 'userRoles'];
  dataSource = new MatTableDataSource<any>();
  errorMessage : string = ''
  isUserFound : boolean = true
  isDropdownOpen: boolean = false

  userDetails : GetUsers[] = []
  // data:any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  
  constructor(private service : UserService,private router:Router ){}

  ngOnInit() {
     this.getAllUsers();
  }
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  
  getUserDetails() {
    this.service.getUserDetails(this.searchUsers)
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
