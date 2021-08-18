import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationDialogService } from 'src/app/components/ui/confirmation-dialog/confirmation-dialog.service';
import { KundenCrudService } from 'src/app/model/kundenstamm/kunden/kunden-crud.service';
import { IKundeDetail } from 'src/app/model/kundenstamm/kunden/dtos/i-kunde-detail';
import { KundeUpdateDialog } from '../../dialogs/kunde-update/kunde-update.dialog';

@Component({
  selector: 'app-kunde-detail',
  templateUrl: './kunde-detail.page.html',
  styleUrls: ['./kunde-detail.page.scss']
})
export class KundeDetailPage implements OnInit {

  kundeId: string;
  kunde: IKundeDetail;

  constructor(
    private kundenCrudService: KundenCrudService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private matDialog: MatDialog,
    private confirmationDialogService: ConfirmationDialogService) {
  }

  async ngOnInit(): Promise<void> {
    this.activatedRoute.params.subscribe((params) => {
      if (params.id) {
        this.kundeId = params.id;
        this.loadKunde().then().catch((error) => {
          console.error(error);
        });
      }
    });
  }

  async onUpdateClicked(): Promise<void> {
    const dialog = this.matDialog.open(KundeUpdateDialog, {
        data: this.kundeId,
        minWidth: '320px',
    });

    if (await dialog.afterClosed().toPromise()) {
      await this.loadKunde();
    }
  }

  async onDeleteClicked(): Promise<void> {
    if (await this.confirmationDialogService.askForConfirmation('Wollen Sie wirklich Kunde \'' + this.kunde.name + '\' löschen?')) {
        await this.kundenCrudService.deleteKunde(this.kunde.id);
        await this.router.navigate(['/kundenstamm/kunden']);
    }
  }

  private async loadKunde(): Promise<void> {
    this.kunde = null;
    this.kunde = await this.kundenCrudService.getKundeDetail(this.kundeId);
  }

}
