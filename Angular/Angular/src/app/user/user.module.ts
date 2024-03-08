import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingComponent, UserRoutingModule } from './user-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AbbreviateNumberPipe } from './abbreviate-number.pipe';

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
  exports:[AbbreviateNumberPipe]
})
export class UserModule { }
