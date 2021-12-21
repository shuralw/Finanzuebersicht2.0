import { BehaviorSubject, ReplaySubject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { IPagedResult } from 'src/app/services/backend/pagination/i-paged-result';
import { IPaginationOptions } from 'src/app/services/backend/pagination/i-pagination-options';
import { IDropdownDataSource } from './i-dropdown-data-source';

export class DropdownPaginationDataSource<T> implements IDropdownDataSource<T> {

    private data: T[] = [];
    private dataSubject = new ReplaySubject<T[]>(1);
    public data$ = this.dataSubject.asObservable();

    private pageSize = 10;
    private pageIndex = -1;
    private filterTerm = '';
    private filterTermSubject = new BehaviorSubject<string>(this.filterTerm);

    private done = false;

    private loading = false;
    private loadingSubject = new BehaviorSubject<boolean>(this.loading);
    public loading$ = this.loadingSubject.asObservable();

    constructor(
        private paginationGetter: (paginationOptions: IPaginationOptions) => Promise<IPagedResult<T>>,
        private filterField: string,
        private debounceTimeInMs: number = 500) {
        this.dataSubject.next([]);
        this.loadNext();

        this.filterTermSubject
            .asObservable()
            .pipe(debounceTime(this.debounceTimeInMs))
            .subscribe((filterTerm) => {
                if (filterTerm !== this.filterTerm) {
                    this.filterTerm = filterTerm;

                    this.pageIndex = -1;
                    this.done = false;

                    this.loadNext(true);
                } else {
                    this.loadingSubject.next(false);
                }
            });
    }

    public filter(filterTerm: string): void {
        if (filterTerm !== this.filterTerm) {
            this.loadingSubject.next(true);
        }
        this.filterTermSubject.next(filterTerm);
    }

    public loadNext(reload: boolean = false): void {
        if (!this.done && !this.loading) {
            this.pageIndex++;
            this.loadData(reload);
        }
    }

    private loadData(reload: boolean = false): void {
        this.loading = true;
        this.loadingSubject.next(true);

        void this.paginationGetter({
            limit: this.pageSize,
            offset: this.pageIndex * this.pageSize,
            filters: [
                {
                    filterField: this.filterField,
                    containsFilters: [this.filterTerm]
                }
            ],
            sortField: this.filterField,
            sortDirection: 'asc'
        })
            .then(paginationPagedResult => {
                if (reload) {
                    this.data = [];
                }
                this.data = this.data.concat(paginationPagedResult.data);
                if (paginationPagedResult.offset + paginationPagedResult.limit >= paginationPagedResult.totalCount) {
                    this.done = true;
                }
            })
            .finally(() => {
                this.dataSubject.next(this.data);
                this.loading = false;
                this.loadingSubject.next(false);
            });
    }
}
