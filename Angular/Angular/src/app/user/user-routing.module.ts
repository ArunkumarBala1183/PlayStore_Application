import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserHomeComponent } from './user-home/user-home.component';
import { Routes, RouterModule } from '@angular/router';
import { UserDownloadsComponent } from './user-downloads/user-downloads.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { DeveloperMyAppsComponent } from './developer-my-apps/developer-my-apps.component';
import { DeveloperNewAppComponent } from './developer-new-app/developer-new-app.component';
import { UserComponent } from './user.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { SpecificAppComponent } from './specific-app/specific-app.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { UserSearchComponent } from './user-search/user-search.component';
import { AbbreviateNumberPipe } from './abbreviate-number.pipe';
import { AuthGuard } from '../auth.guard';

const routes : Routes = [
  {
    path:'user', component : UserComponent,
    children:[
      
        {
          path:'home', component : UserHomeComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },
        {
          path:'downloads', component : UserDownloadsComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },
        {
          path:'about-us', component : AboutUsComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },
        {
          path:'my-apps', component : DeveloperMyAppsComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },
        {
          path:'new-app', component : DeveloperNewAppComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },
        {
          path:'user-profile', component : UserProfileComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },
        // {
        //   path: 'specificApp/:appId', component : SpecificAppComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        // },
        {
            path: 'specific-app', component : SpecificAppComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        },   
        {
          path:'reset-password', component : ResetPasswordComponent, canActivate:[AuthGuard],data:{role:['User','Developer']}
        }
    ]
  }
  
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class UserRoutingModule { }

export const UserRoutingComponent = [
    UserHomeComponent,
    UserDownloadsComponent,
    AboutUsComponent,
    DeveloperMyAppsComponent,
    DeveloperNewAppComponent,
    UserComponent,
    UserProfileComponent,
    UserSearchComponent,
    AbbreviateNumberPipe,
    SpecificAppComponent,
    ResetPasswordComponent,
]
