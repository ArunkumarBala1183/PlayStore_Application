import { Component } from '@angular/core';

@Component({
  selector: 'app-my-downloads',
  templateUrl: './my-downloads.component.html',
  styleUrls: ['./my-downloads.component.scss']
})
export class MyDownloadsComponent {

  apps = [
  
    { name: 'Instagram', rating: '4.3', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/1409/1409946.png' },
    { name: 'Whatsapp', rating: '4.5', category: 'Social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/3536/3536445.png' },
    { name: 'Twitter', rating: '2.1', category: 'social', imageUrl: 'https://cdn-icons-png.flaticon.com/128/3256/3256013.png' },
  ]

}
