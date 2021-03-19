import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-table-filter-bar-dropdown',
  templateUrl: './table-filter-bar-dropdown.component.html',
  styleUrls: ['./table-filter-bar-dropdown.component.scss']
})
export class TableFilterBarDropdownComponent<T> implements OnInit, OnDestroy {

  dataSource: T[];
  @Input('dataSource') set _dataSource(dataSource: T[]) {
    if (dataSource) {
      this.dataSource = dataSource;
      this.updateDataSource();
    }
  }
  selectedDataSourceItem: T;

  value: any;
  @Input('value') set _value(value: any) {
    this.value = value;
    this.updateDataSource();
  }
  @Output() valueChange = new EventEmitter<any>();

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
    if (this.dataSource && this.value && this.valueExpr) {
      this.selectedDataSourceItem = this.dataSource.find(item => item[this.valueExpr] === this.value);
    }

    if (this.dataSource && this.displayExpr) {
      this.dataSource = this.dataSource.sort((a, b) => a[this.displayExpr].localeCompare(b[this.displayExpr]));
    }

    this.filterDataSource();
  }

  onSelectedDataSourceItemChange(newSelectedDataSource: T): void {
    this.selectedDataSourceItem = newSelectedDataSource;

    if (this.valueExpr) {
      this.value = this.selectedDataSourceItem[this.valueExpr];
      this.valueChange.emit(this.value);
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
