import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { Subject, Subscription } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { IDropdownDataSource } from '../../dropdown-data-source/i-dropdown-data-source';

@Component({
  selector: 'app-table-filter-bar-dropdown',
  templateUrl: './table-filter-bar-dropdown.component.html',
  styleUrls: ['./table-filter-bar-dropdown.component.scss']
})
export class TableFilterBarDropdownComponent<T> implements AfterViewInit, OnDestroy {

  selectedDataItems: T[] = [];
  data: T[] = [];
  dataSource: IDropdownDataSource<T>;
  @Input('dataSource') set _dataSource(dataSource: IDropdownDataSource<T>) {
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

  idExpr: any;
  @Input('idExpr') set _idExpr(idExpr: any) {
    this.idExpr = idExpr;
    this.updateDataSource();
  }

  displayExpr: any;
  @Input('displayExpr') set _displayExpr(displayExpr: any) {
    this.displayExpr = displayExpr;
    this.updateDataSource();
  }

  @Input() label: string;

  floatingRight: boolean;
  @Input('floatingRight') set _floatingRight(floatingRight: boolean) {
    this.floatingRight = floatingRight;
    if (floatingRight) {
      this.el.nativeElement.style.float = 'right';
    }
  }

  public filterCtrl: FormControl = new FormControl();

  protected onDestroy = new Subject<void>();

  @ViewChild('select') matSelect: MatSelect;
  scrollElement: any;

  dataSubscription: Subscription;
  filterSubscription: Subscription;

  constructor(public el: ElementRef) {
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

    if (this.idExpr) {
      this.values = this.selectedDataItems
        .map(selectedDataSourceItem => selectedDataSourceItem[this.idExpr]);
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

    if (this.dataSource && this.values && this.idExpr) {
      this.dataSubscription = this.dataSource.data$.subscribe((data: T[]) => {
        const scrollTop = this.scrollElement?.scrollTop;
        this.selectedDataItems = this.values.map(value => data.find(dataItem => dataItem[this.idExpr] === value));
        this.data = data.filter(dataItem => {
          return this.selectedDataItems
            .find(selectedDataItem => selectedDataItem[this.idExpr] === dataItem[this.idExpr]) == null;
        });

        if (scrollTop) {
          setTimeout(() => {
            setTimeout(() => {
              this.scrollElement.scrollTop = scrollTop;
            }, 0);
          }, 0);
        }
      });
    }
  }

  ngAfterViewInit(): void {
    this.matSelect.openedChange
      .pipe(distinctUntilChanged())
      .subscribe((isOpen) => {
        if (isOpen) {
          this.dataSource.filter('');
          this.filterSubscription = this.filterCtrl.valueChanges
            .subscribe(value => this.dataSource.filter(value));

          this.scrollElement = this.matSelect.panel.nativeElement;
          this.scrollElement.addEventListener('scroll', event => {
            if (event.target.scrollTop > event.target.scrollHeight - event.target.clientHeight - 48) {
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
