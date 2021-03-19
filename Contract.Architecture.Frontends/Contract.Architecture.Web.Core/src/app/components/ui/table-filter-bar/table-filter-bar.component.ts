import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TableFilterBarDropdownItem } from './table-filter-bar-dropdown-item';

@Component({
  selector: 'app-table-filter-bar',
  templateUrl: './table-filter-bar.component.html',
  styleUrls: ['./table-filter-bar.component.scss']
})
export class TableFilterBarComponent implements OnInit {

  @Input() filterItems: TableFilterBarDropdownItem[];
  
  @Input() value: any[];
  @Output() valueChange = new EventEmitter<any[]>();

  constructor() { }

  ngOnInit(): void {
  }

  onValueChange(value: any, index: number): void {
    this.value[index] = value;
    this.valueChange.emit(this.value);
  }

}
