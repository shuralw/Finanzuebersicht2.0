import { ApiBankListItem } from './api/api-bank-list-item';

export interface IBankListItem {
    id: string;
    name: string;
    eroeffnetAm: Date;
    isPleite: boolean;
}

export abstract class BankListItem {
    public static fromApiBankListItem(apiBankListItem: ApiBankListItem): IBankListItem {
        return {
            id: apiBankListItem.id,
            name: apiBankListItem.name,
            eroeffnetAm: apiBankListItem.eroeffnetAm,
            isPleite: apiBankListItem.isPleite,
        };
    }
}
