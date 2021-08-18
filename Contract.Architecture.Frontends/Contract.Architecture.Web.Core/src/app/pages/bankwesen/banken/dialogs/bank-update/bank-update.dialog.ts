import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { guidRegex, integerRegex } from 'src/app/helpers/regex.helper';
import { BankUpdate } from 'src/app/model/bankwesen/banken/dtos/i-bank-update';
import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';

@Component({
  selector: 'app-bank-update',
  templateUrl: './bank-update.dialog.html',
  styleUrls: ['./bank-update.dialog.scss']
})
export class BankUpdateDialog implements OnInit {

  bankUpdateForm: FormGroup;

  constructor(
    private bankenCrudService: BankenCrudService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<BankUpdateDialog>,
    @Inject(MAT_DIALOG_DATA) public bankId: string) {
  }

  async ngOnInit(): Promise<void> {
    const bankDetail = await this.bankenCrudService.getBankDetail(this.bankId);

    this.bankUpdateForm = this.formBuilder.group({
      id: new FormControl({ value: '', disabled: true }, [Validators.required]),
      name: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(256)]),
      eroeffnetAm: new FormControl(null, [Validators.required]),
      isPleite: new FormControl(false, [Validators.required]),
    });

    this.bankUpdateForm.patchValue(BankUpdate.fromBankDetail(bankDetail));
  }

  async onUpdateClicked(): Promise<void> {
    this.bankUpdateForm.markAllAsTouched();
    if (!this.bankUpdateForm.valid) {
      this.scrollToFirstInvalidControl();
      return;
    }

    const bankUpdate = this.bankUpdateForm.getRawValue();
    await this.bankenCrudService.updateBank(bankUpdate);
    this.dialogRef.close(true);
  }

  scrollToFirstInvalidControl(): void {
    const firstElementWithError = document.querySelector('.mat-form-field.ng-invalid');
    if (firstElementWithError) {
      firstElementWithError.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

}
