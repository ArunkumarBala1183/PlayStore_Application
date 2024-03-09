import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SearchUser } from '../interface/search-user';
import { AllAppsInfo, AppReviewsInfo, CategoryInfo, DeveloperAppInfo, DownloadedAppsInfo, SpecificAppInfo, userInfo } from '../interface/user';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl : string = environment.apiBaseAddress
  
  constructor(private http : HttpClient) 
  { }

  getAllUsers()
  {
    return this.http.get(this.baseUrl + "Users/GetAllUsers" , {observe : "response"});
  }

  getUserDetails(searchDetails : SearchUser)
  {
    return this.http.post(this.baseUrl + "Users/SearchUserDetails" , searchDetails, {observe : "response"});

  }

  getAllApps()
  {
     return this.http.get<AllAppsInfo[]>(this.baseUrl + "AppInfo/GetAllApps");
  }

  getAppsById(appId:Guid)
  {
    const url = `${this.baseUrl}AppInfo/GetAppById?appId=${appId}`;
    return this.http.get<SpecificAppInfo[]>(url);
  }
  getUserData(userId:Guid)
  {
    const url=`${this.baseUrl}AppInfo/GetUserDetails?userId=${userId}`;
    return this.http.get<userInfo>(url);
  }


  getDeveloperApps(userId : Guid)
  {
  return this.http.get<DeveloperAppInfo[]>(`${this.baseUrl}AppInfo/DeveloperMyAppDetails?userId=${userId}`);
  } 

  getReviews(appId:Guid)
  {
    return this.http.get<AppReviewsInfo[]>(`${this.baseUrl}AppInfo/ReviewDetails?appId=${appId}`);
  }

  postReview(formData : any)
  {
    const url = (`${this.baseUrl}AppInfo/AddReview`);
    return this.http.post(url,formData);
  }
  
  getDownloadedApps(userId : Guid)
  {
    console.log(userId)
    return this.http.get<DownloadedAppsInfo[]>(`${this.baseUrl}AppInfo/DownloadsDetails?userId=${userId}`);
  }

  PostAppFile(appId:Guid,userId:Guid)
  {
    const url=(`${this.baseUrl}AppInfo/DownloadFile?AppId=${appId} &UserId=${userId}`);
    const data = {appId,userId};
    console.log(data);
    return this.http.post(url,data,{
      responseType: 'blob'});     
  }

  getCategory()
  {
    return this.http.get<CategoryInfo[]>(this.baseUrl + "AppInfo/GetCategory");
  }

  postApplication(formData : FormData) 
  {
    // const headers = new HttpHeaders({
    //   'Content-Type': 'application/json',
    // });
    const url = (`${this.baseUrl}AppInfo/AppDetails`);
    console.log(formData);    
    // console.log(formData.value);    
    return this.http.post(url , formData);
  }

}

