import { Bank, IBank } from 'src/app/model/bankwesen/banken/dtos/i-bank';
import { ApiKundeDetail } from './api/api-kunde-detail';

export interface IKundeDetail {
    id: string;
    name: string;
    balance: number;
    bank: IBank;
}

export abstract class KundeDetail {
    public static fromApiKundeDetail(apiKundeDetail: ApiKundeDetail): IKundeDetail {
        return {
            id: apiKundeDetail.id,
            name: apiKundeDetail.name,
            balance: apiKundeDetail.balance,
            bank: Bank.fromApiBank(apiKundeDetail.bank),
        };
    }
}
