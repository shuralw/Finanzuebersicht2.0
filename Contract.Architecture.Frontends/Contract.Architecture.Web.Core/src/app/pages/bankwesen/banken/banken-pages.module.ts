import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { UiComponentsModule } from 'src/app/components/ui/ui-components.module';
import { BankenModule } from 'src/app/model/bankwesen/banken/banken.module';
import { BankCreateDialog } from './dialogs/bank-create/bank-create.dialog';
import { BankDetailPage } from './sub-pages/bank-detail/bank-detail.page';
import { BankUpdateDialog } from './dialogs/bank-update/bank-update.dialog';
import { BankenPagesRouting } from './banken-pages.routing';
import { BankenPage } from './banken.page';

@NgModule({
  declarations: [
    BankenPage,
    BankCreateDialog,
    BankDetailPage,
    BankUpdateDialog,
  ],
  imports: [
    // Routing Modules
    BankenPagesRouting,

    // Model Modules
    BankenModule,

    // UI Components
    UiComponentsModule,

    // Angular Material Modules
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDialogModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,

    // Misc Modules
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class BankenPagesModule { }
