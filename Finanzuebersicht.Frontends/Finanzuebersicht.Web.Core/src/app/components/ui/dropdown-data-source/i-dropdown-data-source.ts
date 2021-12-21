import { Observable } from 'rxjs';

export interface IDropdownDataSource<T> {
    data$: Observable<T[]>;
    loading$: Observable<boolean>;
    loadNext(): void;
    filter(filterTerm: string): void;
}
