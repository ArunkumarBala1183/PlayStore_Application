import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PrimeNGConfig } from 'primeng/api';
import { Category } from 'src/app/interface/category';
import { AppInfoService } from 'src/app/services/app-info.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],

})
export class CategoryComponent implements OnInit {

  categoryName: string = '';
  categoryDetails!: Category
  categorydata: any;

  sno: number = 0


  constructor(
    private primengConfig: PrimeNGConfig,
    private categoryService: CategoryService,
    private toast: ToastrService
  ) { }

  ngOnInit(): void {
    // Initialize PrimeNG configuration
    this.primengConfig.ripple = true;

    // Fetch your data from an API or any other source and assign it to the categories array
    this.fetchCategories();
  }

  onSubmit(form: NgForm) {

    if (form.valid) {
      this.categoryService.addCategory(form.value).subscribe(
        {
          next: response => {
            this.fetchCategories();
            if (response.status == HttpStatusCode.AlreadyReported) {
              this.toast.warning("Already Reported")
            }
            else {
              this.toast.success("Added Successfully")
            }
          },
          error: error => {
            console.log(error)
            this.toast.error("Server Unavailable")
          },
          complete:()=>
          {
            form.reset();
          }
        }
      )
    }
    else {
      this.toast.warning("Please enter the category")
    }
  }

  fetchCategories() {
    this.categoryService.getCategory().subscribe(
      {
        next: response => {
         
          this.categorydata = response.body
         
        },
        error: error => {
          console.log(error)
        }
      }
    )
  }

  // fetchCategories() {
  //   this.categoryService.getCategory().subscribe({
  //     next: (response) => {
  //       console.log(response.body);
  //       this.categorydata = response.body;
  //       if (this.categorydata.length > 0) {
  //         this.categoryIdCounter = this.categorydata[this.categorydata.length - 1].categoryId;
  //       }
  //       console.log(this.categorydata);
  //     },
  //     error: (error) => {
  //       console.log(error);
  //     },
  //   });
  // }

}

