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
  public values: any[] = [];
  itemsPerPage = 5;
  filteredLogs: AppLogs[] = [];
  maxDate: string | undefined;



  filterDetails: LogFilter = {
    appName: '',
    downloadedDate: '',
    fromDate: '',
    userName: ''
  }

  appLogDetails!: AppLogs[]

  isLogFound: boolean = false

  errorMessage: string = ''



  ngOnInit(): void {

    this.filterDetails.downloadedDate = new Date().toISOString().split('T')[0]
    this.maxDate = new Date().toISOString().split('T')[0]

    this.getAppLogs()
  }



  // Function to update filteredLogs based on pagination
  updateFilteredLogs() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = Math.min(startIndex + this.itemsPerPage, this.appLogDetails.length);
    this.filteredLogs = this.appLogDetails.slice(startIndex, endIndex);
  }

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
    return (this.currentPage - 1) * this.itemsPerPage+1;

  }

  currentPageEndIndex(): number {
    const endIndex = this.currentPage * this.itemsPerPage;
    return endIndex > this.values.length ? this.values.length : endIndex;

  }

  constructor(private service: AppInfoService, private toastr: ToastrService) {

  }



  getAppLogs() {
    this.service.getAppLogs(this.filterDetails)
      .subscribe({
        next: response => {
          this.isLogFound = true
          this.appLogDetails = response.body as AppLogs[]

          // Convert dates format before assigning
          this.appLogDetails.forEach(log => {
            log.downloadedDate = this.convertDateFormat(log.downloadedDate);
          });

           // Update values array and filteredLogs array
           this.updateFilteredLogs();
          this.values = this.appLogDetails.slice();// Make a copy to avoid reference issues
          
        // Set currentPage to 1 after updating data
          if(this.values.length <= this.itemsPerPage)
          {
            this.currentPage = 1
          }
        },
        error: error => {
          this.isLogFound = false
          this.errorMessage = error.error.message
        }
      })
  }

  convertDateFormat(dateString: string): string {
    const parts = dateString.split('-'); // Assuming dateString is in yyyy-mm-dd format
    if (parts.length === 3) {
      const [year, month, day] = parts;
      return `${day}/${month}/${year}`;
    }
    return dateString; // Return as it is if unable to parse
  }



}