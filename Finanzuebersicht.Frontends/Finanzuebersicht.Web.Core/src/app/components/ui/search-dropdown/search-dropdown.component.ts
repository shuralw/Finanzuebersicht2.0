import { AfterViewInit, Component, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { Subject, Subscription } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { distinctByField } from 'src/app/helpers/distinct.helper';
import { IDropdownDataSource } from '../dropdown-data-source/i-dropdown-data-source';

@Component({
  selector: 'app-search-dropdown',
  templateUrl: './search-dropdown.component.html',
  styleUrls: ['./search-dropdown.component.scss']
})
export class SearchDropdownComponent<T> implements AfterViewInit, OnDestroy {

  @Input() formGroupInstance: FormGroup;
  @Input() formControlNameInstance: string;

  @Input() label: string;
  @Input() required = false;

  selectedItem: T;
  @Input('initialItem') set _initialItem(initialItem: T) {
    if (initialItem) {
      this.selectedItem = initialItem;
      this.updateDataSource();
    }
  }

  data: T[];
  dataSource: IDropdownDataSource<T>;
  @Input('dataSource') set _dataSource(dataSource: IDropdownDataSource<T>) {
    if (dataSource) {
      this.dataSource = dataSource;
      this.updateDataSource();
    }
  }

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

  public filterCtrl: FormControl = new FormControl();

  @ViewChild(MatSelect) matSelect: MatSelect;
  scrollElement: any;

  dataSubscription: Subscription;
  valueChangeSubscription: Subscription;
  filterSubscription: Subscription;
  protected onDestroy = new Subject<void>();

  constructor() { }

  getDisplayname(value: T): string {
    if (this.displayExpr) {
      return value[this.displayExpr];
    } else {
      return JSON.stringify(value);
    }
  }

  getId(value: T): string {
    if (this.idExpr) {
      return value[this.idExpr];
    } else {
      return JSON.stringify(value);
    }
  }

  updateDataSource(): void {
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
      this.dataSubscription = null;
    }

    if (this.valueChangeSubscription) {
      this.valueChangeSubscription.unsubscribe();
      this.valueChangeSubscription = null;
    }

    if (this.filterSubscription) {
      this.filterSubscription.unsubscribe();
      this.filterSubscription = null;
    }

    if (this.dataSource && this.formGroupInstance && this.formControlNameInstance) {
      this.dataSubscription = this.dataSource.data$.subscribe((data: T[]) => {
        const scrollTop = this.scrollElement?.scrollTop;
        if (this.selectedItem) {
          this.data = distinctByField([this.selectedItem].concat(data), this.idExpr);
        } else {
          this.data = distinctByField(data, this.idExpr);
        }

        if (scrollTop) {
          setTimeout(() => {
            setTimeout(() => {
              this.scrollElement.scrollTop = scrollTop;
            }, 0);
          }, 0);
        }
      });

      this.valueChangeSubscription = this.formGroupInstance.controls[this.formControlNameInstance].valueChanges
        .subscribe((value) => {
          this.selectedItem = this.data.find(dataItem => dataItem[this.idExpr] === value);
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
