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
import { AuthGuard } from '../auth.guard';


const routes: Routes = [
  {
    path: 'admin', component: AdminComponent, children:
      [
        {
          path: 'dashboard', component: DashboardComponent, title: 'Admin Dashboard', canActivate:[AuthGuard],data:{role:'User'},
        },
        // {
        //   path:'home', redirectTo : '/admin' , pathMatch : 'full'
        // },
       
        {
          path: 'apps', component: AppsComponent, title: 'Admin apps', canActivate:[AuthGuard],data:{role:'User'},
        },
       
        {
          path: 'users', component: UsersComponent, title: 'Users', canActivate:[AuthGuard],data:{role:'User'},
        },
        
        {
          path: 'myDownloads', component: MyDownloadsComponent, title: 'MyDownloads', canActivate:[AuthGuard],data:{role:'User'},
        },
        {
          path: 'category', component: CategoryComponent, title: 'category', canActivate:[AuthGuard],data:{role:'User'},
        },
        {
          path: 'logs', component: LogsComponent, title: 'Logs', canActivate:[AuthGuard],data:{role:'User'},
        },
        {
          path: 'profile', component: ProfileComponent, title: 'Profile', canActivate:[AuthGuard],data:{role:'User'},
        },
        {
          path: 'apprequests', component: ApprequestsComponent, title: 'apprequests', canActivate:[AuthGuard],data:{role:'User'},
        },

        {
          path: 'requestdetails/:id', component: RequestdetailsComponent, title: 'requestdetails', canActivate:[AuthGuard],data:{role:'User'},
        },

        {
          path: 'about', component: AboutComponent, title: 'about', canActivate:[AuthGuard],data:{role:'User'},
        },

        {
          path: 'changePassword', component: ChangePasswordComponent, title: 'changePassword', canActivate:[AuthGuard],data:{role:'User'},
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
