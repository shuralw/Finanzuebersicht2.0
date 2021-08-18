import { Injectable } from '@angular/core';
import { BackendCoreService } from 'src/app/services/backend/backend-core.service';
import { IPagedResult } from 'src/app/services/backend/pagination/i-paged-result';
import { IPaginationOptions, toPaginationParams } from 'src/app/services/backend/pagination/i-pagination-options';
import { ApiBankDetail } from './dtos/api/api-bank-detail';
import { ApiBankListItem } from './dtos/api/api-bank-list-item';
import { BankCreate, IBankCreate } from './dtos/i-bank-create';
import { BankDetail, IBankDetail } from './dtos/i-bank-detail';
import { BankListItem, IBankListItem } from './dtos/i-bank-list-item';
import { BankUpdate, IBankUpdate } from './dtos/i-bank-update';

@Injectable()
export class BankenCrudService {

    constructor(private backendCoreService: BackendCoreService) { }

    public async getPagedBanken(paginationOptions: IPaginationOptions): Promise<IPagedResult<IBankListItem>> {
        const url = '/api/bankwesen/banken?' + toPaginationParams(paginationOptions);
        const bankenResult = await this.backendCoreService.get<IPagedResult<ApiBankListItem>>(url);

        bankenResult.data = bankenResult.data
            .map(apiBankListItem => BankListItem.fromApiBankListItem(apiBankListItem));
        return bankenResult;
    }

    public async getBankDetail(bankId: string): Promise<IBankDetail> {
        const apiBankDetail = await this.backendCoreService.get<ApiBankDetail>('/api/bankwesen/banken/' + bankId);

        const bankDetail = BankDetail.fromApiBankDetail(apiBankDetail);
        return bankDetail;
    }

    public async createBank(bankCreate: IBankCreate): Promise<string> {
        const options = {
            body: BankCreate.toApiBankCreate(bankCreate)
        };

        const result = await this.backendCoreService.post<{ data: string }>('/api/bankwesen/banken', options);

        const newBankId = result.data;
        return newBankId;
    }

    public async updateBank(bankUpdate: IBankUpdate): Promise<void> {
        const options = {
            body: BankUpdate.toApiBankUpdate(bankUpdate)
        };

        await this.backendCoreService.put('/api/bankwesen/banken', options);
    }

    public async deleteBank(bankId: string): Promise<void> {
        await this.backendCoreService.delete('/api/bankwesen/banken/' + bankId);
    }

}
