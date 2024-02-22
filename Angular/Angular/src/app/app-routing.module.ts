import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login/login.component';
import { UserComponent } from './user/user.component';
import { UserHomeComponent } from './user/user-home/user-home.component';
import { UserDownloadsComponent } from './user/user-downloads/user-downloads.component';
import { DeveloperMyAppsComponent } from './user/developer-my-apps/developer-my-apps.component';
import { DeveloperNewAppComponent } from './user/developer-new-app/developer-new-app.component';
import { AboutUsComponent } from './user/about-us/about-us.component';

const routes: Routes = [
  {
    path: '', component: LoginComponent
  },
  {
    path: 'app',
    loadChildren: () => import('./application/application.module').then(m => m.ApplicationModule)
  },
  {
    path:'user',
    loadChildren: () => import('./user/user.module').then(n => n.UserModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
