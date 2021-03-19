import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { SearchDropdownComponent } from './search-dropdown/search-dropdown.component';

@NgModule({
    declarations: [
        LoadingSpinnerComponent,
        SearchDropdownComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatSelectModule,
        NgxMatSelectSearchModule,
    ],
    exports: [
        LoadingSpinnerComponent,
        SearchDropdownComponent,
    ]
})
export class UiComponentsModule { }
