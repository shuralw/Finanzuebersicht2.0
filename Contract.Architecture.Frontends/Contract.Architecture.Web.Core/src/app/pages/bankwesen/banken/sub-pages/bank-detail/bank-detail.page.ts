import { IKunde } from 'src/app/model/kundenstamm/kunden/dtos/i-kunde';
import { MatTableDataSource } from '@angular/material/table';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationDialogService } from 'src/app/components/ui/confirmation-dialog/confirmation-dialog.service';
import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';
import { IBankDetail } from 'src/app/model/bankwesen/banken/dtos/i-bank-detail';
import { BankUpdateDialog } from '../../dialogs/bank-update/bank-update.dialog';

@Component({
  selector: 'app-bank-detail',
  templateUrl: './bank-detail.page.html',
  styleUrls: ['./bank-detail.page.scss']
})
export class BankDetailPage implements OnInit {

  public kundenTableDataSource = new MatTableDataSource<IKunde>([]);
  public kundenGridColumns: string[] = [
    'name',
    'detail',
  ];

  bankId: string;
  bank: IBankDetail;

  constructor(
    private bankenCrudService: BankenCrudService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private matDialog: MatDialog,
    private confirmationDialogService: ConfirmationDialogService) {
  }

  async ngOnInit(): Promise<void> {
    this.activatedRoute.params.subscribe((params) => {
      if (params.id) {
        this.bankId = params.id;
        this.loadBank().then().catch((error) => {
          console.error(error);
        });
      }
    });
  }

  async onUpdateClicked(): Promise<void> {
    const dialog = this.matDialog.open(BankUpdateDialog, {
        data: this.bankId,
        minWidth: '320px',
    });

    if (await dialog.afterClosed().toPromise()) {
      await this.loadBank();
    }
  }

  async onDeleteClicked(): Promise<void> {
    if (await this.confirmationDialogService.askForConfirmation('Wollen Sie wirklich Bank \'' + this.bank.name + '\' löschen?')) {
        await this.bankenCrudService.deleteBank(this.bank.id);
        await this.router.navigate(['/bankwesen/banken']);
    }
  }

  private async loadBank(): Promise<void> {
    this.bank = null;
    this.bank = await this.bankenCrudService.getBankDetail(this.bankId);

    this.kundenTableDataSource.data = this.bank.kunden;
  }

}
