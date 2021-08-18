import { ApiBank } from './api/api-bank';

export interface IBank {
    id: string;
    name: string;
    eroeffnetAm: Date;
    isPleite: boolean;
}

export class Bank {
    public static fromApiBank(apiBank: ApiBank): IBank {
        return {
            id: apiBank.id,
            name: apiBank.name,
            eroeffnetAm: apiBank.eroeffnetAm,
            isPleite: apiBank.isPleite,
        };
    }
}
