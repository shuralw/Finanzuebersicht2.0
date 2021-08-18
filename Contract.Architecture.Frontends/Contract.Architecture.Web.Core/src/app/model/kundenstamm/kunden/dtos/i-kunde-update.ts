import { ApiKundeUpdate } from './api/api-kunde-update';
import { IKundeDetail } from './i-kunde-detail';

export interface IKundeUpdate {
    id: string;
    name: string;
    balance: number;
    bankId: string;
}

export abstract class KundeUpdate {
    public static toApiKundeUpdate(iKundeUpdate: IKundeUpdate): ApiKundeUpdate {
        return {
            id: iKundeUpdate.id,
            name: iKundeUpdate.name,
            balance: iKundeUpdate.balance,
            bankId: iKundeUpdate.bankId,
        };
    }

    public static fromKundeDetail(iKundeDetail: IKundeDetail): IKundeUpdate {
        return {
            id: iKundeDetail.id,
            name: iKundeDetail.name,
            balance: iKundeDetail.balance,
            bankId: iKundeDetail.bank.id,
        };
    }
}
