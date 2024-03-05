import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingComponent, UserRoutingModule } from './user-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    UserRoutingComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule
  ],
})
export class UserModule { }
