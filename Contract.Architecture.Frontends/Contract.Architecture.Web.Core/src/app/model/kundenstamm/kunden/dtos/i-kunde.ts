import { ApiKunde } from './api/api-kunde';

export interface IKunde {
    id: string;
    name: string;
    balance: number;
    bankId: string;
}

export class Kunde {
    public static fromApiKunde(apiKunde: ApiKunde): IKunde {
        if (apiKunde == null) {
            return null;
        }

        return {
            id: apiKunde.id,
            name: apiKunde.name,
            balance: apiKunde.balance,
            bankId: apiKunde.bankId,
        };
    }
}
