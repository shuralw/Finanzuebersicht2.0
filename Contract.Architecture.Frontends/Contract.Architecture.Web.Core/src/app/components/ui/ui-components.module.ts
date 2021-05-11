import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { ConfirmationDialog } from './confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from './confirmation-dialog/confirmation-dialog.service';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { SearchDropdownFormComponent } from './search-dropdown-form/search-dropdown-form.component';
import { SearchDropdownComponent } from './search-dropdown/search-dropdown.component';
import { TableFilterBarDropdownMultipleComponent } from './table-filter-bar/table-filter-bar-dropdown-multiple/table-filter-bar-dropdown-multiple.component';
import { TableFilterBarDropdownComponent } from './table-filter-bar/table-filter-bar-dropdown/table-filter-bar-dropdown.component';
import { TableFilterBarComponent } from './table-filter-bar/table-filter-bar.component';

@NgModule({
    declarations: [
        ConfirmationDialog,
        LoadingSpinnerComponent,
        SearchDropdownComponent,
        SearchDropdownFormComponent,
        TableFilterBarComponent,
        TableFilterBarDropdownComponent,
        TableFilterBarDropdownMultipleComponent,
    ],
    exports: [
        LoadingSpinnerComponent,
        SearchDropdownComponent,
        SearchDropdownFormComponent,
        TableFilterBarComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatButtonModule,
        MatDialogModule,
        MatSelectModule,
        MatIconModule,
        NgxMatSelectSearchModule,
    ],
    providers: [
        ConfirmationDialogService,
    ]
})
export class UiComponentsModule { }
