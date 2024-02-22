import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'abbreviateNumber'
})
export class AbbreviateNumberPipe implements PipeTransform {

  transform(averageRating : number): string
  {
    if(averageRating >= 1000)
    {
      const suffixes = ['', 'k', 'M', 'B', 'T'];
      const suffixNum = Math.floor(('' + averageRating).length / 3);
      const shortValue = parseFloat((suffixNum !== 0 ? (averageRating / Math.pow(1000, suffixNum)) : averageRating).toPrecision(3));
      return shortValue + suffixes[suffixNum];
    } 
    else 
    {
      return averageRating.toString();
    }
  }

}
