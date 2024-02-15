import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login/login.component';

const routes: Routes = [
  {
    path: '', component: LoginComponent
  },
  {
    path: 'app',
    loadChildren: () => import('./application/application.module').then(m => m.ApplicationModule)
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
