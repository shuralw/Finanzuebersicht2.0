import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[appRight]'
})
export class RightDirective {

  @HostBinding('style.float')
  backgroundColor = 'right';

  constructor() { }

}
