import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogComponent } from './components/dialog/dialog.component';
import { AdminModule } from '../admin/admin.module';



@NgModule({
  declarations: [
    DialogComponent,
  ],
  imports: [
    CommonModule
  ],
})
export class SharedModule { }
