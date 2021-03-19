import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-table-filter-bar-dropdown-multiple',
  templateUrl: './table-filter-bar-dropdown-multiple.component.html',
  styleUrls: ['./table-filter-bar-dropdown-multiple.component.scss']
})
export class TableFilterBarDropdownMultipleComponent<T> implements OnInit, OnDestroy {

  dataSource: T[];
  @Input('dataSource') set _dataSource(dataSource: T[]) {
    if (dataSource) {
      this.dataSource = dataSource;
      this.updateDataSource();
    }
  }
  selectedDataSourceItems: T[];

  values: any[];
  @Input('values') set _values(values: any[]) {
    this.values = values;
    this.updateDataSource();
  }
  @Output() valuesChange = new EventEmitter<any>();

  valueExpr: any;
  @Input('valueExpr') set _valueExpr(valueExpr: any) {
    this.valueExpr = valueExpr;
    this.updateDataSource();
  }

  displayExpr: any;
  @Input('displayExpr') set _displayExpr(displayExpr: any) {
    this.displayExpr = displayExpr;
    this.updateDataSource();
  }

  @Input() label: string;

  // ----------- TAKEN FROM EXAMPLE -----------

  /** control for the MatSelect filter keyword */
  public filterCtrl: FormControl = new FormControl();

  /** list of dataSource filtered by search keyword */
  public filteredDataSource: ReplaySubject<T[]> = new ReplaySubject<T[]>(1);

  /** Subject that emits when the component has been destroyed. */
  protected onDestroy = new Subject<void>();

  constructor() { }

  getDisplayname(value: T): string {
    if (this.displayExpr) {
      return value[this.displayExpr];
    } else {
      return JSON.stringify(value);
    }
  }

  updateDataSource(): void {
    if (this.dataSource && this.values && this.valueExpr) {
      for (let i = 0; i < this.values.length; i++) {
        const value = this.values[i];
        this.selectedDataSourceItems[i] = this.dataSource.find(item => item[this.valueExpr] === value);
      }
    }

    if (this.dataSource && this.displayExpr) {
      this.dataSource = this.dataSource.sort((a, b) => a[this.displayExpr].localeCompare(b[this.displayExpr]));
    }

    this.filterDataSource();
  }

  onSelectedDataSourceItemChange(newSelectedDataSource: T[]): void {
    this.selectedDataSourceItems = newSelectedDataSource;

    if (this.valueExpr) {
      this.values = this.selectedDataSourceItems
        .map(selectedDataSourceItem => selectedDataSourceItem[this.valueExpr]);
      this.valuesChange.emit(this.values);
    }
  }

  // ----------- TAKEN FROM EXAMPLE -----------

  ngOnInit(): void {
    // listen for search field value changes
    this.filterCtrl.valueChanges
      .pipe(takeUntil(this.onDestroy))
      .subscribe(() => {
        this.filterDataSource();
      });
  }

  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  protected filterDataSource(): void {
    if (!this.dataSource || !this.displayExpr) {
      return;
    }

    // get the search keyword
    let search = this.filterCtrl.value;
    if (!search) {
      this.filteredDataSource.next(this.dataSource.slice());
      return;
    } else {
      search = search.toLowerCase();
    }

    // filter the dataSource
    this.filteredDataSource.next(
      this.dataSource.filter(item => item[this.displayExpr].toLowerCase().indexOf(search) > -1)
    );
  }
}
