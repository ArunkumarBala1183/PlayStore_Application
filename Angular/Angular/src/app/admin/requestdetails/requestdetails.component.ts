import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { ToastrService } from 'ngx-toastr';
import { PrimeNGConfig } from 'primeng/api';
import { ProcessRequest } from 'src/app/interface/process-request';
import { RequestDetails } from 'src/app/interface/request-details';
import { AppInfoService } from 'src/app/services/app-info.service';
import { AppRequestsService } from 'src/app/services/app-requests.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-requestdetails',
  templateUrl: './requestdetails.component.html',
  styleUrls: ['./requestdetails.component.scss']
})
export class RequestdetailsComponent implements OnInit {
  [x: string]: any;

  appdetails: any[] = [];

  requestDetails: RequestDetails[] = []

  appId!: string

  processRequestDetails: ProcessRequest | undefined

  constructor(private router: ActivatedRoute, private primengConfig: PrimeNGConfig, private service: AppRequestsService, private appDataService: AppInfoService , private toastr : ToastrService , private route : Router,private userservice:UserService) { }

  appidShow: boolean = false;

  ngOnInit(): void {

    // this.router.params.subscribe(response => {
    //   this.appId = response["id"]
    // });
  //  this.appId=this.userservice.getAppId()
  this.appId=this.userservice.getAppId();
    this.primengConfig.ripple = true;
    this.fetchAppDetails();
    this.getRequestedDetails();
  }

  fetchAppDetails() {
    this.appdetails = [
      {
        appId: '8b2301e8-4809-4e38-9cfa-9c08b75f775c',
        Appname: 'Whatsapp',
        description: 'Messaging App',
        logo: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png',
        status: 'Pending',
        categoryName: 'social',
        publisherName: 'Arun',
        name: 'Arun',
        emailId: 'arun@gmail.com',
        mobileNumber: '8667863441',
        appImages: ['https://play-lh.googleusercontent.com/tNuMAclO_TrRn5RbiSo2iU2ySljFaHjCIWoMUSoemUcl4FjTyVO0PpJZL_zTrYf7v_4=w5120-h2880-rw',
          'https://play-lh.googleusercontent.com/ijfSGQUCqeCmCQX0w_HjdSWkiYZoFk5JZ5CsxmGI-qT1VPT8V3wGohMBpWZOAp2o7A=w5120-h2880-rw',
          'https://play-lh.googleusercontent.com/8InPqYGQ-28qwt_mLmm6R3VzbMcf3ZSJNUxO_OJosyLRqPHeStZFtjKskgDvHkanfRUJ=w5120-h2880-rw'


        ]
      }
    ]
  }

  getRequestedDetails() {
    this.service.getRequestedDetails(this.appId)
      .subscribe({
        next: response => {
          this.requestDetails.push(response.body as RequestDetails)
        },
        error: error => {
          console.log(error)
        }
      })
  }

  downloadAppData(appId: string) {
    this.appDataService.getAppData(appId)
      .subscribe((data: any) => {
        const blob = new Blob([data]);
        const url = window.URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.setAttribute('type', 'hidden');
        anchor.href = url;
        anchor.download = new Date().toISOString() + ".zip";
        anchor.click()
        anchor.remove()
      });
  }

  processRequest(isApproved: boolean) {

    this.processRequestDetails = {
      appId: this.requestDetails[0].appId,
      approve: isApproved
    };

    this.service.processRequest(this.processRequestDetails)
      .subscribe({
        next: response => {
          if(response.status == HttpStatusCode.Created)
          {
            this.toastr.success("Approved" , "Playstore Application");
            this.route.navigate(["admin/apps"]) 
          }
          else
          {
            this.toastr.error("Denied" , "Playstore Application"); 
            this.route.navigate(["admin/app-requests"])
          }

         
        },
        error: error => {
          console.log(error)
        }
      })

  }
}
