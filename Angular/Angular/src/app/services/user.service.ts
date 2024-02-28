import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  
  AllAppsInfo = [
    {
      appId : '1' , appName : 'Instagram', appLogo: 'https://play-lh.googleusercontent.com/VRMWkE5p3CkWhJs6nv-9ZsLAs1QOg5ob1_3qg-rckwYW7yp1fMrYZqnEFpk0IoVP4LM=w480-h960-rw', appCategory : 'Social', appAverageRating : 4, appDownloads : 1000000, appDescription : 'Instagram to chat, watch feeds, Video and Audio calls',
       appScreenshots : ['https://play-lh.googleusercontent.com/fRvdBTcc5b7pMwXkSEa5-Jm47ZfTt2lc8buw_wbFgF5lkj3GuLyu2B3b4zf7mKXhW3E=w5120-h2880-rw', 'https://play-lh.googleusercontent.com/W7J_rhJYWt65XQHaZ7N_6Nptu0wC6n4k9WX59qg46KRpe9b5I1LarJqZ7L-Uu9okgA=w1052-h592-rw', 'https://play-lh.googleusercontent.com/sn_2xT5NCjg-Km4XiZMAOM6xb4LxDqC_9sd5TENCjbU9D4aXVNrendOmIzHFyQo_kahz=w1052-h592-rw'
      ]
    },
    {
      appId : '2' , appName : 'Whatsapp', appLogo: 'https://play-lh.googleusercontent.com/bYtqbOcTYOlgc6gqZ2rwb8lptHuwlNE75zYJu6Bn076-hTmvd96HH-6v7S0YUAAJXoJN=w480-h960-rw', appCategory : 'Entertainment', appAverageRating : 3 , appDownloads : 2000000, appDescription : 'Whatsapp to chat, video and audio call with high Quality Whatsapp to chat, video and audio call with high Whatsapp to chat, video and audio call with high QualityQuality  Whatsapp to chat, video and audio call with high Quality'
      ,appScreenshots : ['https://play-lh.googleusercontent.com/Ck5x7vPWfgXoLvkGqVs5INzV3dzHMYYy4Jr6YVpXDTR-00p_V_kpGABtfXCp9qx10cs=w1052-h592-rw', 'https://play-lh.googleusercontent.com/8InPqYGQ-28qwt_mLmm6R3VzbMcf3ZSJNUxO_OJosyLRqPHeStZFtjKskgDvHkanfRUJ=w5120-h2880-rw', 'https://play-lh.googleusercontent.com/tNuMAclO_TrRn5RbiSo2iU2ySljFaHjCIWoMUSoemUcl4FjTyVO0PpJZL_zTrYf7v_4=w1052-h592-rw'
    ]
    }
  ];

  DownloadedAppInfo = [
    {
      appId : '1', appName : 'Instagram', appLogo: '../../../assets/Images/instagram.png', appCategory : 'Social', appAverageRating : '4', appDownloads : '1000000', appDescription : 'Instagram to chat, watch feeds, Video and Audio calls'
    }
  ]

  DeveloperAppInfo = [
    {
      appId : '2', appName : 'Whatsapp', appLogo: '../../../assets/Images/whatsapp.png', appCategory : 'Entertainment', appAverageRating : '1', appDownloads : '2000000', appDescription : 'Whatsapp to chat, video and audio call with high Quality Whatsapp to chat, video and audio call with high Whatsapp to chat, video and audio call with high QualityQuality  Whatsapp to chat, video and audio call with high Quality'
    }
  ]
}
