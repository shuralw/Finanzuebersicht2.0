import { Observable, of, ReplaySubject } from 'rxjs';
import { IDropdownDataSource } from './i-dropdown-data-source';

export class DropdownDataSource<T> implements IDropdownDataSource<T> {

    private dataSubject = new ReplaySubject<T[]>(1);
    public data$ = this.dataSubject.asObservable();

    filterTerm = '';

    public loading$: Observable<boolean> = of(false);

    constructor(
        private data: T[],
        private filterField: string) {
        this.filter(this.filterTerm);
    }

    public filter(filterTerm: string): void {
        this.filterTerm = filterTerm;
        this.dataSubject.next(
            this.data.filter(dataItem =>
                filterTerm.trim().length === 0 ||
                (dataItem[this.filterField] as string).includes(filterTerm)));
    }

    public loadNext(): void {
        this.filter(this.filterTerm);
    }

}
