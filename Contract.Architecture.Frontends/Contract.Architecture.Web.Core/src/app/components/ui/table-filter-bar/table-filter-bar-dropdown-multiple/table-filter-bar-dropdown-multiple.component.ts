import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinct, filter, tap } from 'rxjs/operators';
import { MultiDataSource } from './multi-data-source';

@Component({
  selector: 'app-table-filter-bar-dropdown-multiple',
  templateUrl: './table-filter-bar-dropdown-multiple.component.html',
  styleUrls: ['./table-filter-bar-dropdown-multiple.component.scss']
})
export class TableFilterBarDropdownMultipleComponent<T> implements AfterViewInit, OnDestroy {

  selectedDataItems: T[] = [];
  data: T[] = [];
  dataSource: MultiDataSource<T>;
  @Input('dataSource') set _dataSource(dataSource: MultiDataSource<T>) {
    if (dataSource) {
      this.dataSource = dataSource;
      this.updateDataSource();
    }
  }

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

  public filterCtrl: FormControl = new FormControl();

  protected onDestroy = new Subject<void>();

  @ViewChild('select') matSelect: MatSelect;
  scrollElement: any;

  dataSubscription: Subscription;
  filterSubscription: Subscription;

  constructor() {
  }

  getDisplayname(value: T): string {
    if (value == null) {
      return '';
    }

    if (this.displayExpr) {
      return value[this.displayExpr];
    } else {
      return JSON.stringify(value);
    }
  }

  onSelectedDataSourceItemChange(newSelectedDataSource: T[]): void {
    this.selectedDataItems = newSelectedDataSource;

    if (this.valueExpr) {
      this.values = this.selectedDataItems
        .map(selectedDataSourceItem => selectedDataSourceItem[this.valueExpr]);
      this.valuesChange.emit(this.values);
    }
  }

  updateDataSource(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
      this.dataSubscription = null;
    }

    if (this.filterSubscription) {
      this.filterSubscription.unsubscribe();
      this.filterSubscription = null;
    }

    if (this.dataSource && this.values && this.valueExpr) {
      this.dataSubscription = this.dataSource.data$.subscribe((data: T[]) => {
        const scrollTop = this.scrollElement?.scrollTop;
        this.selectedDataItems = this.values.map(value => data.find(dataItem => dataItem[this.valueExpr] === value));
        this.data = data.filter(dataItem => {
          return this.selectedDataItems
            .find(selectedDataItem => selectedDataItem[this.valueExpr] === dataItem[this.valueExpr]) == null;
        });

        if (scrollTop) {
          setTimeout(() => {
            setTimeout(() => {
              this.scrollElement.scrollTop = scrollTop;
            }, 0);
          }, 0);
        }
      });

      // listen for search field value changes
      this.filterSubscription = this.filterCtrl.valueChanges
        .pipe(
          filter((filterTerm) => this.dataSource.isNewFilterTerm(filterTerm)),
          tap(() => this.data = []),
          debounceTime(500),
        )
        .subscribe(value => this.dataSource.filter(value));
    }
  }

  ngAfterViewInit(): void {
    this.matSelect.openedChange
      .pipe(distinct())
      .subscribe((isOpen) => {
        if (isOpen) {
          this.scrollElement = this.matSelect.panel.nativeElement;
          this.scrollElement.addEventListener('scroll', event => {
            if (!this.dataSource.loading && event.target.scrollTop > event.target.scrollHeight - event.target.clientHeight - 48) {
              this.dataSource.loadNext();
            }
          });
        } else if (this.filterSubscription) {
          this.filterSubscription.unsubscribe();
          this.filterSubscription = null;
        }
      });
  }

  ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

}
