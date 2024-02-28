import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-requestdetails',
  templateUrl: './requestdetails.component.html',
  styleUrls: ['./requestdetails.component.scss']
})
export class RequestdetailsComponent implements OnInit {
  [x: string]: any;

  appdetails: any[] = [];


  constructor(private primengConfig: PrimeNGConfig) { }

  appidShow: boolean = false;

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.fetchAppDetails();
  }

  fetchAppDetails() {
    this.appdetails = [
      { appId: '8b2301e8-4809-4e38-9cfa-9c08b75f775c',
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

}
