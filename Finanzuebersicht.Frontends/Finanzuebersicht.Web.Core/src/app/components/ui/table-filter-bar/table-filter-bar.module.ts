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
import { TableFilterBarButtonComponent } from './table-filter-bar-button/table-filter-bar-button.component';
import { TableFilterBarDropdownComponent } from './table-filter-bar-dropdown/table-filter-bar-dropdown.component';
import { TableFilterBarIconComponent } from './table-filter-bar-icon/table-filter-bar-icon.component';
import { TableFilterBarInputComponent } from './table-filter-bar-input/table-filter-bar-input.component';
import { TableFilterBarSpacerComponent } from './table-filter-bar-spacer/table-filter-bar-spacer.component';
import { TableFilterBarTitleComponent } from './table-filter-bar-title/table-filter-bar-title.component';
import { TableFilterBarComponent } from './table-filter-bar.component';

@NgModule({
    declarations: [
        TableFilterBarComponent,
        TableFilterBarButtonComponent,
        TableFilterBarDropdownComponent,
        TableFilterBarIconComponent,
        TableFilterBarInputComponent,
        TableFilterBarSpacerComponent,
        TableFilterBarTitleComponent,
    ],
    exports: [
        TableFilterBarComponent,
        TableFilterBarButtonComponent,
        TableFilterBarDropdownComponent,
        TableFilterBarIconComponent,
        TableFilterBarInputComponent,
        TableFilterBarSpacerComponent,
        TableFilterBarTitleComponent,
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
    ]
})
export class TableFilterBarModule { }
