import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
 
})
export class CategoryComponent implements OnInit {

  categories: any[] = [];
  

  constructor(private primengConfig: PrimeNGConfig) {}

  ngOnInit(): void {
    // Initialize PrimeNG configuration
    this.primengConfig.ripple = true;

    // Fetch your data from an API or any other source and assign it to the categories array
    this.fetchCategories();
  }
    
  fetchCategories()  {
      this.categories = [
        { id: 1, name: 'Social' },
        { id: 2, name: 'Entertainment' },
        { id: 3, name: 'Education' },
        { id: 4, name: 'Business' },
        { id: 5, name: 'News' },
        { id: 6, name: 'Shopping' },
        { id: 7, name: 'Tools' },
        { id: 8, name: 'Editing' },
        { id: 9, name: 'Lifestyle' }
        
        // Add more categories as needed
    ];
    }
}

