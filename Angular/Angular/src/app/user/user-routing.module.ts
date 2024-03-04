import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule, Routes } from '@angular/router';
import { UploadComponent } from './upload/upload.component';
import { AuthGuard } from '../auth.guard';

const routes:Routes=[
  {
    path:'', component:UploadComponent , canActivate:[AuthGuard] , data:{role:'User'}
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class UserRoutingModule { }
