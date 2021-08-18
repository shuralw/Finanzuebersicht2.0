import { IBankListItem } from 'src/app/model/bankwesen/banken/dtos/i-bank-list-item';
import { BankenCrudService } from 'src/app/model/bankwesen/banken/banken-crud.service';
import { DropdownPaginationDataSource } from 'src/app/components/ui/dropdown-data-source/dropdown-pagination-data-source';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { guidRegex, integerRegex } from 'src/app/helpers/regex.helper';
import { KundenCrudService } from 'src/app/model/kundenstamm/kunden/kunden-crud.service';

@Component({
  selector: 'app-kunde-create',
  templateUrl: './kunde-create.dialog.html',
  styleUrls: ['./kunde-create.dialog.scss']
})
export class KundeCreateDialog implements OnInit {

  kundeCreateForm: FormGroup;

  bankDataSource: DropdownPaginationDataSource<IBankListItem>;

  constructor(
    private kundenCrudService: KundenCrudService,
    private bankenCrudService: BankenCrudService,
    private formBuilder: FormBuilder,
    private router: Router,
    private dialogRef: MatDialogRef<KundeCreateDialog>) {
  }

  async ngOnInit(): Promise<void> {
    this.kundeCreateForm = this.formBuilder.group({
      name: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(256)]),
      balance: new FormControl(null, [Validators.required, Validators.pattern(integerRegex)]),
      bankId: new FormControl(null, [Validators.required]),
    });

    this.bankDataSource = new DropdownPaginationDataSource(
      (options) => this.bankenCrudService.getPagedBanken(options),
      'name');
  }

  async onCreateClicked(): Promise<void> {
    this.kundeCreateForm.markAllAsTouched();
    if (!this.kundeCreateForm.valid) {
      this.scrollToFirstInvalidControl();
      return;
    }

    const kundeId = await this.kundenCrudService.createKunde(this.kundeCreateForm.getRawValue());
    this.dialogRef.close();
    await this.router.navigate(['/kundenstamm/kunden/detail', kundeId]);
  }

  scrollToFirstInvalidControl(): void {
    const firstElementWithError = document.querySelector('.mat-form-field.ng-invalid');
    if (firstElementWithError) {
      firstElementWithError.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

}
