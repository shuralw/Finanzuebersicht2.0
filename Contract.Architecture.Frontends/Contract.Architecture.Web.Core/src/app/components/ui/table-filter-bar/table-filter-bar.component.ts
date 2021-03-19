import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TableFilterBarDropdownItem } from './table-filter-bar-dropdown-item';

@Component({
  selector: 'app-table-filter-bar',
  templateUrl: './table-filter-bar.component.html',
  styleUrls: ['./table-filter-bar.component.scss']
})
export class TableFilterBarComponent implements OnInit {

  @Input() filterItems: TableFilterBarDropdownItem[];

  @Input() filterItemsValues: any[][];
  @Output() filterItemsValuesChange = new EventEmitter<any[]>();

  currentFilterTerm = '';
  @Output() filterTermChange = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  onFilterItemValuesChange(filterItemValues: any, index: number): void {
    this.filterItemsValues[index] = filterItemValues;
    this.filterItemsValuesChange.emit(this.filterItemsValues);
  }

  onFilterTermChange(event: any) {
    const filterTerm = event.target.value;

    if (this.currentFilterTerm != filterTerm) {
      this.currentFilterTerm = filterTerm;
      this.filterTermChange.emit(filterTerm);
    }
  }

}
