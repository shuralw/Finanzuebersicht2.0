import { DataSource } from '@angular/cdk/collections';
import { EventEmitter } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BehaviorSubject, merge, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { IPaginationFilterItem, IPaginationOptions } from 'src/app/services/backend/pagination/i-pagination-options';
import { IPagedResult } from './i-paged-result';

export class PaginationDataSource<T> implements DataSource<T> {

    private paginationSubject = new BehaviorSubject<T[]>([]);
    private paginationTotalCountSubject = new BehaviorSubject<number>(0);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private updateTriggered = new EventEmitter<void>();

    public totalCount$ = this.paginationTotalCountSubject.asObservable();
    public loading$ = this.loadingSubject.asObservable();

    constructor(
        private paginationGetter: (paginationOptions: IPaginationOptions) => Promise<IPagedResult<T>>,
        private filterResolve: () => IPaginationFilterItem[]) {
    }

    public initialize(
        matPaginator: MatPaginator,
        matSort: MatSort): void {

        merge(
            matSort.sortChange,
            this.updateTriggered.asObservable())
            .pipe(
                tap(() => matPaginator.pageIndex = 0),
            ).subscribe();

        merge(
            matSort.sortChange,
            matPaginator.page,
            this.updateTriggered.asObservable())
            .pipe(
                tap(() => {
                    const options: IPaginationOptions = {
                        limit: matPaginator.pageSize,
                        offset: matPaginator.pageIndex * matPaginator.pageSize,
                        sortField: matSort.active,
                        sortDirection: matSort.direction,
                        filters: this.filterResolve()
                    };
                    this.loadData(options);
                })
            ).subscribe();

        this.triggerUpdate();
    }

    triggerUpdate(): void {
        this.updateTriggered.emit();
    }

    connect(): Observable<T[]> {
        return this.paginationSubject.asObservable();
    }

    disconnect(): void {
        this.paginationSubject.complete();
        this.loadingSubject.complete();
    }

    loadData(paginationOptions: IPaginationOptions): void {
        this.loadingSubject.next(true);

        void this.paginationGetter(paginationOptions)
            .then(paginationPagedResult => {
                this.paginationSubject.next(paginationPagedResult.data);
                this.paginationTotalCountSubject.next(paginationPagedResult.totalCount);
            })
            .catch(() => this.paginationSubject.next([]))
            .finally(() => this.loadingSubject.next(false));
    }
}
