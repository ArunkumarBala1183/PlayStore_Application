import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SearchUser } from '../interface/search-user';
import { AllAppsInfo, AppReviewsInfo, CategoryInfo, DeveloperAppInfo, DownloadedAppsInfo, SpecificAppInfo, UserProfileInfo } from '../interface/user';
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

  getAppsById(appId:Guid , userId : Guid)
  {
    const data = {appId : appId , userId : userId}
    const url = `${this.baseUrl}AppInfo/GetAppById?appId=${appId}&userId=${userId}`;
    return this.http.get<SpecificAppInfo[]>(url);
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
    const url = (`${this.baseUrl}AppInfo/AppDetails`);
    console.log(formData);      
    return this.http.post(url , formData);
  }


  postPassword(userId : Guid , password : string)
  {
    const url = (`${this.baseUrl}Login/changePassword`);
    const data = {userId:userId ,password:password}
    return this.http.patch(url , data);
  }
}

