import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'min',
})
export class MinPipe implements PipeTransform {
  transform(values: number[]): string {
    return Math.min(...values).toString();
  }
}
