import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';
import { IBankListItem } from 'src/app/model/bankwesen/banken/dtos/i-bank-list-item';
import { PaginationDataSource } from 'src/app/services/backend/pagination/pagination.data-source';
import { BankCreateDialog } from './dialogs/bank-create/bank-create.dialog';

@Component({
  selector: 'app-banken',
  templateUrl: './banken.page.html',
  styleUrls: ['./banken.page.scss']
})
export class BankenPage implements AfterViewInit {

  // FilterBar
  filterTerm = '';

  // Table
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  public bankenDataSource: PaginationDataSource<IBankListItem>;
  public bankenGridColumns: string[] = [
    'name',
    'eroeffnetAm',
    'isPleite',
    'detail',
  ];

  constructor(
    private bankenCrudService: BankenCrudService,
    private matDialog: MatDialog) {

    this.bankenDataSource = new PaginationDataSource<IBankListItem>(
      (options) => this.bankenCrudService.getPagedBanken(options),
      () => [
        {
          filterField: 'name',
          containsFilters: [this.filterTerm]
        },
      ]);
  }

  async ngAfterViewInit(): Promise<void> {
    this.bankenDataSource.initialize(this.paginator, this.sort);
  }

  openCreateDialog(): void {
    this.matDialog.open(BankCreateDialog, {
      maxHeight: '90vh'
    });
  }

}
