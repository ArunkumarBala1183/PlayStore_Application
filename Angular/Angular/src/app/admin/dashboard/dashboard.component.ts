import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit{

  ngOnInit(): void {
    console.log("came here");
    this.createChart();
  }

  createChart(){
    const dates = this.generateDates(); // Function to generate dates
    const ctx = document.getElementById('appDownloadsChart') as HTMLCanvasElement;
    const myChart = new Chart(ctx, {
      type: 'bar',
      data: {
        // labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        labels:dates,
        datasets: [{
          label: 'No of apps downloaded',
          data: [12, 193, 360, 5, 59, 300,100],
          backgroundColor: [
            'rgba(0, 0, 0, 1)',
            'rgba(0, 0, 0, 1)',
            'rgba(0, 0, 0, 1)',
            'rgba(0, 0, 0, 1)',
            'rgba(0, 0, 0, 1)',
            'rgba(0, 0, 0, 1)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 0
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }

  // generateDates(): string[] {
  //   const daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  //   const today = new Date();
  //   const dates = [];
  //   for (let i = 0; i < 7; i++) {
  //     const day = new Date(today);
  //     day.setDate(today.getDate() - today.getDay() + i);
  //     dates.push(daysOfWeek[i] + ' (' + day.toLocaleDateString() + ')');
  //   }
  //   return dates;
  // }

  generateDates(): string[] {
    const today = new Date();
    const dates = [];
    for (let i = 6; i >= 0; i--) {
      const day = new Date(today);
      day.setDate(today.getDate() - i);
      const formattedDate = day.toLocaleDateString('en-GB'); // Format: DD/MM/YYYY
      dates.push(formattedDate);
    }
    return dates;
  }
  

  }
  

