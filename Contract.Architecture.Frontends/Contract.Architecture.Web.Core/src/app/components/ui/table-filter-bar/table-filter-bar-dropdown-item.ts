import { MultiDataSource } from 'src/app/components/ui/table-filter-bar-new/table-filter-bar-dropdown-multiple/multi-data-source';

export interface TableFilterBarDropdownItem {
    dataName: string;
    dataSource: MultiDataSource<any>;
    valueExpr: string;
    displayExpr: string;
}
