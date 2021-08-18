import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';
import { DropdownPaginationDataSource } from 'src/app/components/ui/dropdown-data-source/dropdown-pagination-data-source';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { KundenCrudService } from 'src/app/model/kundenstamm/kunden/kunden-crud.service';
import { IKundeListItem } from 'src/app/model/kundenstamm/kunden/dtos/i-kunde-list-item';
import { PaginationDataSource } from 'src/app/services/backend/pagination/pagination.data-source';
import { KundeCreateDialog } from './dialogs/kunde-create/kunde-create.dialog';

@Component({
  selector: 'app-kunden',
  templateUrl: './kunden.page.html',
  styleUrls: ['./kunden.page.scss']
})
export class KundenPage implements AfterViewInit {

  // FilterBar
  filterTerm = '';

  bankSelectedValues = [];
  bankDataSource = new DropdownPaginationDataSource(
    (options) => this.bankenCrudService.getPagedBanken(options),
    'name');

  // Table
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  public kundenDataSource: PaginationDataSource<IKundeListItem>;
  public kundenGridColumns: string[] = [
    'name',
    'balance',
    'bank',
    'detail',
  ];

  constructor(
    private bankenCrudService: BankenCrudService,
    private kundenCrudService: KundenCrudService,
    private matDialog: MatDialog) {

    this.kundenDataSource = new PaginationDataSource<IKundeListItem>(
      (options) => this.kundenCrudService.getPagedKunden(options),
      () => [
        {
          filterField: 'name',
          containsFilters: [this.filterTerm]
        },
        {
          filterField: 'bankId',
          equalsFilters: this.bankSelectedValues
        },
      ]);
  }

  async ngAfterViewInit(): Promise<void> {
    this.kundenDataSource.initialize(this.paginator, this.sort);
  }

  openCreateDialog(): void {
    this.matDialog.open(KundeCreateDialog, {
      maxHeight: '90vh'
    });
  }

}
