import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingComponent, UserRoutingModule } from './user-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AbbreviateNumberPipe } from './abbreviate-number.pipe';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    UserRoutingComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule
  ],
  exports:[AbbreviateNumberPipe]
})
export class UserModule { }
