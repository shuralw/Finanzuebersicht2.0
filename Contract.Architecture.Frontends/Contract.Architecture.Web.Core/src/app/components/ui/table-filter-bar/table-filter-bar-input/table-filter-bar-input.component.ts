import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-table-filter-bar-input',
  templateUrl: './table-filter-bar-input.component.html',
  styleUrls: ['./table-filter-bar-input.component.scss']
})
export class TableFilterBarInputComponent {

  @Input() placeholder: string;

  @Input() value = '';
  @Output() valueChange = new EventEmitter<string>();

  constructor() { }

}
