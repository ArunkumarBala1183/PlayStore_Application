import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Category } from '../interface/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  baseurl : string = environment.apiBaseAddress + "Category"
  constructor(private http : HttpClient) 
  { }
        
  addCategory(newCategoryName : Category)
  {
    console.log(newCategoryName)
    return this.http.post(this.baseurl + "/AddCategory", newCategoryName, {observe:"response"});
  }

  getCategory()
  {
    return this.http.get(this.baseurl + "/GetAllCategories", {observe:"response"});
  }
 

}
