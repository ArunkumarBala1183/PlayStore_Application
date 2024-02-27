import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { LoginComponent } from './login/login/login.component';
import { AdminComponent } from './admin/admin.component';
import { UsersComponent } from './admin/users/users.component';
import { MyDownloadsComponent } from './admin/my-downloads/my-downloads.component';
import { CategoryComponent } from './admin/category/category.component';
import { LogsComponent } from './admin/logs/logs.component';
import { ProfileComponent } from './admin/profile/profile.component';

const routes: Routes = [
  {
    path: '', component: LoginComponent
  },
  {
    path: 'admin',component:AdminComponent,
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
    
  },
  // {
  //   path: 'app',
  //   loadChildren: () => import('./application/application.module').then(m => m.ApplicationModule)
  // }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
