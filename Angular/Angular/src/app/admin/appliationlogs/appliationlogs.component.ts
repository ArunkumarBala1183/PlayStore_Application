import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-appliationlogs',
  templateUrl: './appliationlogs.component.html',
  styleUrls: ['./appliationlogs.component.scss']
})
export class AppliationlogsComponent {

  displayedColumns: string[] = ['message', 'level' , 'timeStamp','exception','userId','location'];
  dataSource = new MatTableDataSource<any>();
}
