import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'abbreviateNumber'
})
export class AbbreviateNumberPipe implements PipeTransform {

  transform(totalDownloads : number): string
  {
    if(totalDownloads >= 1000)
    {
      const suffixes = ['', 'k', 'M', 'B', 'T'];

      // This part converts the totalDownloads to a string by concatenating an empty string 
      // math.floor -> calculates the sets of three digits are there in the number.
      const suffixNum = Math.floor(('' + totalDownloads).length / 3); 

      // parseFloat - stops parsing as soon as it encounters a character that is not a valid part of a number.
      // toPrecision - to limit 3 significant figures and result in exponential.
      // example : Input -> 123456.789 output(toPrecision(3)) -> "1.23e+5" then output(parseFloat) -> "123000 "
      const shortValue = parseFloat((suffixNum !== 0 ? (totalDownloads / Math.pow(1000, suffixNum)) : totalDownloads).toPrecision(3));
      return shortValue + suffixes[suffixNum];
    } 
    else 
    {
      return totalDownloads.toString();
    }
  }

}
