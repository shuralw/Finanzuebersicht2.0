import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-table-filter-bar-button',
  templateUrl: './table-filter-bar-button.component.html',
  styleUrls: ['./table-filter-bar-button.component.scss']
})
export class TableFilterBarButtonComponent {

  @Input() matIconName: string;
  @Input() text: string;

  @Output() clicked = new EventEmitter<void>();

  constructor() { }

  getAriaLabel(): string {
    return this.matIconName || this.text;
  }

}
