import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-table-filter-bar-title',
  templateUrl: './table-filter-bar-title.component.html',
  styleUrls: ['./table-filter-bar-title.component.scss']
})
export class TableFilterBarTitleComponent {

  @Input() title: string;

  constructor() { }

}
