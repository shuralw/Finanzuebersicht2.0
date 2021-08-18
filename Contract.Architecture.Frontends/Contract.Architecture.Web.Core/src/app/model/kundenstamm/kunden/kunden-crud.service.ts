import { Injectable } from '@angular/core';
import { BackendCoreService } from 'src/app/services/backend/backend-core.service';
import { IPagedResult } from 'src/app/services/backend/pagination/i-paged-result';
import { IPaginationOptions, toPaginationParams } from 'src/app/services/backend/pagination/i-pagination-options';
import { ApiKundeDetail } from './dtos/api/api-kunde-detail';
import { ApiKundeListItem } from './dtos/api/api-kunde-list-item';
import { KundeCreate, IKundeCreate } from './dtos/i-kunde-create';
import { KundeDetail, IKundeDetail } from './dtos/i-kunde-detail';
import { KundeListItem, IKundeListItem } from './dtos/i-kunde-list-item';
import { KundeUpdate, IKundeUpdate } from './dtos/i-kunde-update';

@Injectable()
export class KundenCrudService {

    constructor(private backendCoreService: BackendCoreService) { }

    public async getPagedKunden(paginationOptions: IPaginationOptions): Promise<IPagedResult<IKundeListItem>> {
        const url = '/api/kundenstamm/kunden?' + toPaginationParams(paginationOptions);
        const kundenResult = await this.backendCoreService.get<IPagedResult<ApiKundeListItem>>(url);

        kundenResult.data = kundenResult.data
            .map(apiKundeListItem => KundeListItem.fromApiKundeListItem(apiKundeListItem));
        return kundenResult;
    }

    public async getKundeDetail(kundeId: string): Promise<IKundeDetail> {
        const apiKundeDetail = await this.backendCoreService.get<ApiKundeDetail>('/api/kundenstamm/kunden/' + kundeId);

        const kundeDetail = KundeDetail.fromApiKundeDetail(apiKundeDetail);
        return kundeDetail;
    }

    public async createKunde(kundeCreate: IKundeCreate): Promise<string> {
        const options = {
            body: KundeCreate.toApiKundeCreate(kundeCreate)
        };

        const result = await this.backendCoreService.post<{ data: string }>('/api/kundenstamm/kunden', options);

        const newKundeId = result.data;
        return newKundeId;
    }

    public async updateKunde(kundeUpdate: IKundeUpdate): Promise<void> {
        const options = {
            body: KundeUpdate.toApiKundeUpdate(kundeUpdate)
        };

        await this.backendCoreService.put('/api/kundenstamm/kunden', options);
    }

    public async deleteKunde(kundeId: string): Promise<void> {
        await this.backendCoreService.delete('/api/kundenstamm/kunden/' + kundeId);
    }

}
