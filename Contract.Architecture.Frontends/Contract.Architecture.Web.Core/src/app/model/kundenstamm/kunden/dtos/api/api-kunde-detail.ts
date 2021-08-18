import { ApiBank } from 'src/app/model/bankwesen/banken/dtos/api/api-bank';

export interface ApiKundeDetail {
    id: string;
    name: string;
    balance: number;
    bank: ApiBank;
}
