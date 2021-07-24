import { ScrollingModule } from '@angular/cdk/scrolling';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { ConfirmationDialog } from './confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from './confirmation-dialog/confirmation-dialog.service';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { SearchDropdownFormComponent } from './search-dropdown-form/search-dropdown-form.component';
import { SearchDropdownComponent } from './search-dropdown/search-dropdown.component';
import { TableFilterBarButtonComponent } from './table-filter-bar/table-filter-bar-button/table-filter-bar-button.component';
import { TableFilterBarDropdownMultipleComponent } from './table-filter-bar/table-filter-bar-dropdown-multiple/table-filter-bar-dropdown-multiple.component';
import { TableFilterBarIconComponent } from './table-filter-bar/table-filter-bar-icon/table-filter-bar-icon.component';
import { TableFilterBarInputComponent } from './table-filter-bar/table-filter-bar-input/table-filter-bar-input.component';
import { TableFilterBarSpacerComponent } from './table-filter-bar/table-filter-bar-spacer/table-filter-bar-spacer.component';
import { TableFilterBarTitleComponent } from './table-filter-bar/table-filter-bar-title/table-filter-bar-title.component';
import { RightDirective } from './table-filter-bar/table-filter-bar-right.directive';
import { TableFilterBarComponent } from './table-filter-bar/table-filter-bar.component';

@NgModule({
    declarations: [
        ConfirmationDialog,
        LoadingSpinnerComponent,
        SearchDropdownComponent,
        SearchDropdownFormComponent,
        TableFilterBarComponent,
        TableFilterBarButtonComponent,
        TableFilterBarDropdownMultipleComponent,
        TableFilterBarIconComponent,
        TableFilterBarInputComponent,
        TableFilterBarSpacerComponent,
        TableFilterBarTitleComponent,
        RightDirective,
    ],
    exports: [
        LoadingSpinnerComponent,
        SearchDropdownComponent,
        SearchDropdownFormComponent,
        TableFilterBarComponent,
        TableFilterBarButtonComponent,
        TableFilterBarDropdownMultipleComponent,
        TableFilterBarIconComponent,
        TableFilterBarInputComponent,
        TableFilterBarSpacerComponent,
        TableFilterBarTitleComponent,
        RightDirective,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ScrollingModule,
        MatButtonModule,
        MatDialogModule,
        MatFormFieldModule,
        MatIconModule,
        MatProgressSpinnerModule,
        MatSelectModule,
        NgxMatSelectSearchModule,
    ],
    providers: [
        ConfirmationDialogService,
    ]
})
export class UiComponentsModule { }
