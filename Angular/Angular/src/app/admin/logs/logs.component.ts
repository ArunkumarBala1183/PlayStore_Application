import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AppLogs } from 'src/app/interface/app-logs';
import { LogFilter } from 'src/app/interface/log-filter';
import { AppInfoService } from 'src/app/services/app-info.service';
 
 
 
 
@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.scss']
})
export class LogsComponent implements OnInit {
  currentPage = 1;
  public values:any[] = [];
  itemsPerPage = 6;
  filteredLogs: AppLogs[] = [];
  maxDate:string | undefined;
 
 
 
  filterDetails : LogFilter = {
    appName : '',
    downloadedDate : '',
    fromDate : '',
    userName : ''
  }
 
  appLogDetails! : AppLogs[]
 
  isLogFound : boolean = false
 
  errorMessage : string = ''
 
 
 
  ngOnInit(): void {
    this.filterDetails.downloadedDate = new Date().toISOString().split('T')[0];
     this.maxDate = new Date().toISOString().split('T')[0];
    this.getAppLogs()
    console.log(this.currentPage)
    console.log(this.itemsPerPage)
    console.log(this.values)
    // this.applyFilters();
  }
 
    // Function to update filteredLogs based on pagination
    updateFilteredLogs() {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = Math.min(startIndex + this.itemsPerPage, this.appLogDetails.length);
      this.filteredLogs = this.appLogDetails.slice(startIndex, endIndex);
    }
 
 
 
 
  // // Pagination variables
  // pageSize = 5; // Initial page size
  // pageIndex = 0; // Initial page index
  // pagedUsers: any[] = []; // Data for the current page
 
  getPages(): number[] {
    const pageCount = Math.ceil(this.values.length / this.itemsPerPage);
    return Array.from({ length: pageCount }, (_, i) => i + 1);
  }
 
  goToPage(page: number): void {
 
    if (page >= 1 && page <= this.getPages().length) {
      this.currentPage = page;    
       
    }
    this.getAppLogs();
  }
 
  currentPageStartIndex(): number {
    return (this.currentPage - 1) * this.itemsPerPage;
   
  }
 
  currentPageEndIndex(): number {
    const endIndex = this.currentPage * this.itemsPerPage;
    return endIndex > this.values.length ? this.values.length : endIndex;
 
  }
 
  constructor(private service : AppInfoService , private toastr : ToastrService) {
   
  }
 
 
 
  getAppLogs()
  {
    this.service.getAppLogs(this.filterDetails)
    .subscribe({
      next : response => {
        this.isLogFound = true
        this.appLogDetails = response.body as AppLogs[]
        this.values = this.appLogDetails;        
        this.updateFilteredLogs();
        this.currentPage = 1; // Call function to update filteredLogs
      },
      error : error => {
        this.isLogFound = false
        this.errorMessage = error.error.message
      }
    })
  }
 
 
 
 
 
}