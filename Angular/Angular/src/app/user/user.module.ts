import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingComponent, UserRoutingModule } from './user-routing.module';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    UserRoutingComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    MatIconModule
  ],
  // exports:[
  //   UserHomeComponent,
  //   UserDownloadsComponent,
  //   AboutUsComponent,
  //   DeveloperMyAppsComponent,
  //   DeveloperNewAppComponent,
  //   UserComponent,
  // ]
})
export class UserModule { }
