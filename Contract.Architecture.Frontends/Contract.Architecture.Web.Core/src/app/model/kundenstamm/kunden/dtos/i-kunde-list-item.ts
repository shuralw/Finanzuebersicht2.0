import { Bank, IBank } from 'src/app/model/bankwesen/banken/dtos/i-bank';
import { ApiKundeListItem } from './api/api-kunde-list-item';

export interface IKundeListItem {
    id: string;
    name: string;
    balance: number;
    bank: IBank;
}

export abstract class KundeListItem {
    public static fromApiKundeListItem(apiKundeListItem: ApiKundeListItem): IKundeListItem {
        return {
            id: apiKundeListItem.id,
            name: apiKundeListItem.name,
            balance: apiKundeListItem.balance,
            bank: Bank.fromApiBank(apiKundeListItem.bank),
        };
    }
}
