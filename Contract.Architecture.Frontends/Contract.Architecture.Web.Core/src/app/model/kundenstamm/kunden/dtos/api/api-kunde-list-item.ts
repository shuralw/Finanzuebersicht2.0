import { ApiBank } from 'src/app/model/bankwesen/banken/dtos/api/api-bank';

export interface ApiKundeListItem {
    id: string;
    name: string;
    balance: number;
    bank: ApiBank;
}
