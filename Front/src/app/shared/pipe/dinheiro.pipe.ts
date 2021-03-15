import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Pipe({
  name: 'dinheiro'
})
export class DinheiroPipe implements PipeTransform {

  transform(value: number, currencyCode: string = 'BRL', symbolDisplay: boolean = true, digits?: string): string {
    if (!value) {
      if (symbolDisplay) {
        return 'R$ 0,00';
      } else {
        return '0,00';
      }
    }

    let currencyPipe: CurrencyPipe = new CurrencyPipe('pt-BR');
    let newValue: string = currencyPipe.transform(value, currencyCode, symbolDisplay, digits);

    return newValue;
  }
}
