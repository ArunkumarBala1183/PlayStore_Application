import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { UsersComponent } from './users/users.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminComponent } from './admin.component';
import { RouterModule } from '@angular/router';
import { MyDownloadsComponent } from './my-downloads/my-downloads.component';
import { CategoryComponent } from './category/category.component';
import { LogsComponent } from './logs/logs.component';
import { ProfileComponent } from './profile/profile.component';
import { SharedModule } from '../shared/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppsComponent } from './apps/apps.component';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { ApprequestsComponent } from './apprequests/apprequests.component';
import { RequestdetailsComponent } from './requestdetails/requestdetails.component';
import { AboutComponent } from './about/about.component';






@NgModule({
  declarations: [
    UsersComponent,
    AdminHomeComponent,
    AdminComponent,
    MyDownloadsComponent,
    CategoryComponent,
    LogsComponent,
    ProfileComponent,
    DashboardComponent,
    AppsComponent,
    ApprequestsComponent,
    RequestdetailsComponent,
    AboutComponent,     
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    RouterModule,
    FormsModule,
    TableModule,
    PaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    MatTableModule
  ],
  exports:[
    // AdminComponent
  ]
  
})
export class AdminModule { }
