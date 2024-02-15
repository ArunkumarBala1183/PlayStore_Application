import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppCreateComponent } from './app-create/app-create.component';
import { AppListComponent } from './app-list/app-list.component';
import { AppViewComponent } from './app-view/app-view.component';

const routes: Routes = [
  {
    path: '', component: AppListComponent
  },
  {
    path: 'create', component: AppCreateComponent
  },
  {
    path: 'view', component: AppViewComponent
  },
  {
    path: 'edit/:id', component: AppCreateComponent
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationRoutingModule { }
