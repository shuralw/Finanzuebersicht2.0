import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DropdownPaginationDataSource } from 'src/app/components/ui/dropdown-data-source/dropdown-pagination-data-source';
import { integerRegex } from 'src/app/helpers/regex.helper';
import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';
import { IBank } from 'src/app/model/bankwesen/banken/dtos/i-bank';
import { KundeUpdate } from 'src/app/model/kundenstamm/kunden/dtos/i-kunde-update';
import { KundenCrudService } from 'src/app/model/kundenstamm/kunden/kunden-crud.service';

@Component({
  selector: 'app-kunde-update',
  templateUrl: './kunde-update.dialog.html',
  styleUrls: ['./kunde-update.dialog.scss']
})
export class KundeUpdateDialog implements OnInit {

  kundeUpdateForm: FormGroup;

  bankDataSource: DropdownPaginationDataSource<IBank>;
  selectedBank: IBank;

  constructor(
    private kundenCrudService: KundenCrudService,
    private bankenCrudService: BankenCrudService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<KundeUpdateDialog>,
    @Inject(MAT_DIALOG_DATA) public kundeId: string) {
  }

  async ngOnInit(): Promise<void> {
    const kundeDetail = await this.kundenCrudService.getKundeDetail(this.kundeId);

    this.kundeUpdateForm = this.formBuilder.group({
      id: new FormControl({ value: '', disabled: true }, [Validators.required]),
      name: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(256)]),
      balance: new FormControl(null, [Validators.required, Validators.pattern(integerRegex)]),
      bankId: new FormControl(null, [Validators.required]),
    });

    this.kundeUpdateForm.patchValue(KundeUpdate.fromKundeDetail(kundeDetail));

    this.selectedBank = kundeDetail.bank;
    this.bankDataSource = new DropdownPaginationDataSource(
      (options) => this.bankenCrudService.getPagedBanken(options),
      'name');
  }

  async onUpdateClicked(): Promise<void> {
    this.kundeUpdateForm.markAllAsTouched();
    if (!this.kundeUpdateForm.valid) {
      this.scrollToFirstInvalidControl();
      return;
    }

    const kundeUpdate = this.kundeUpdateForm.getRawValue();
    await this.kundenCrudService.updateKunde(kundeUpdate);
    this.dialogRef.close(true);
  }

  scrollToFirstInvalidControl(): void {
    const firstElementWithError = document.querySelector('.mat-form-field.ng-invalid');
    if (firstElementWithError) {
      firstElementWithError.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

}
