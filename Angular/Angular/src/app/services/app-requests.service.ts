import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ProcessRequest } from '../interface/process-request';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class AppRequestsService{

  private baseUrl : string = environment.apiBaseAddress + "Request"

  constructor(private http : HttpClient) { }

  getAppRequests()
  {
      return this.http.get(this.baseUrl + "/AppRequests" , {observe : 'response'})
  }

  getRequestedDetails(appId : string)
  {
    return this.http.get(this.baseUrl + "/GetRequestedAppDetails/" + appId , {observe : "response"});
  }

  processRequest(requestDetails : ProcessRequest)
  {
     return this.http.post(this.baseUrl + "/PublishApp" , requestDetails , {observe : 'response'});
  }
}
