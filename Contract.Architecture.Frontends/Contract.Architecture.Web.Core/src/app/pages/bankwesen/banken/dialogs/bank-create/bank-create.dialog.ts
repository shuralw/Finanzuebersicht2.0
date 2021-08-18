import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { guidRegex, integerRegex } from 'src/app/helpers/regex.helper';
import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';

@Component({
  selector: 'app-bank-create',
  templateUrl: './bank-create.dialog.html',
  styleUrls: ['./bank-create.dialog.scss']
})
export class BankCreateDialog implements OnInit {

  bankCreateForm: FormGroup;

  constructor(
    private bankenCrudService: BankenCrudService,
    private formBuilder: FormBuilder,
    private router: Router,
    private dialogRef: MatDialogRef<BankCreateDialog>) {
  }

  async ngOnInit(): Promise<void> {
    this.bankCreateForm = this.formBuilder.group({
      name: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(256)]),
      eroeffnetAm: new FormControl(null, [Validators.required]),
      isPleite: new FormControl(false, [Validators.required]),
    });
  }

  async onCreateClicked(): Promise<void> {
    this.bankCreateForm.markAllAsTouched();
    if (!this.bankCreateForm.valid) {
      this.scrollToFirstInvalidControl();
      return;
    }

    const bankId = await this.bankenCrudService.createBank(this.bankCreateForm.getRawValue());
    this.dialogRef.close();
    await this.router.navigate(['/bankwesen/banken/detail', bankId]);
  }

  scrollToFirstInvalidControl(): void {
    const firstElementWithError = document.querySelector('.mat-form-field.ng-invalid');
    if (firstElementWithError) {
      firstElementWithError.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

}
