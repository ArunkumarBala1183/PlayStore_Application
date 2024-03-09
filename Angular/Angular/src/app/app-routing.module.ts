import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './home/homepage/homepage.component';
import { AdminComponent } from './admin/admin.component';
import { _getOptionScrollPosition } from '@angular/material/core';

const routes: Routes = [
  {
    path: 'admin',component:AdminComponent,
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: '', component: HomepageComponent
  },
  {
    path:'login',
    loadChildren:()=>import('./login/login.module').then(m=>m.LoginModule) 

  },
  {
    path:'user',
    loadChildren: () => import('./user/user.module').then(n => n.UserModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes , {scrollPositionRestoration : 'top'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }


