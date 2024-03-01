import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminComponent } from './admin.component';
import { MyDownloadsComponent } from './my-downloads/my-downloads.component';
import { CategoryComponent } from './category/category.component';
import { LogsComponent } from './logs/logs.component';
import { ProfileComponent } from './profile/profile.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AppsComponent } from './apps/apps.component';
import { ApprequestsComponent } from './apprequests/apprequests.component';
import { RequestdetailsComponent } from './requestdetails/requestdetails.component';
import { AboutComponent } from './about/about.component';
import { ChangePasswordComponent } from './change-password/change-password.component';


const routes: Routes = [
  {
    path: 'admin', component: AdminComponent, children:
      [
        {
          path: 'dashboard', component: DashboardComponent, title: 'Admin Dashboard'
        },
        // {
        //   path:'home', redirectTo : '/admin' , pathMatch : 'full'
        // },
       
        {
          path: 'apps', component: AppsComponent, title: 'Admin apps'
        },
       
        {
          path: 'users', component: UsersComponent, title: 'Users'
        },
        
        {
          path: 'myDownloads', component: MyDownloadsComponent, title: 'MyDownloads'
        },
        {
          path: 'category', component: CategoryComponent, title: 'category'
        },
        {
          path: 'logs', component: LogsComponent, title: 'Logs'
        },
        {
          path: 'profile', component: ProfileComponent, title: 'Profile'
        },
        {
          path: 'apprequests', component: ApprequestsComponent, title: 'apprequests'
        },

        {
          path: 'requestdetails', component: RequestdetailsComponent, title: 'requestdetails'
        },

        {
          path: 'about', component: AboutComponent, title: 'about'
        },

        {
          path: 'changePassword', component: ChangePasswordComponent, title: 'changePassword'
        }
      ]
  }
]



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AdminRoutingModule { }
