import { ApiKundeCreate } from './api/api-kunde-create';

export interface IKundeCreate {
    name: string;
    balance: number;
    bankId: string;
}

export abstract class KundeCreate {
    public static toApiKundeCreate(iKundeCreate: IKundeCreate): ApiKundeCreate {
        return {
            name: iKundeCreate.name,
            balance: iKundeCreate.balance,
            bankId: iKundeCreate.bankId,
        };
    }
}
