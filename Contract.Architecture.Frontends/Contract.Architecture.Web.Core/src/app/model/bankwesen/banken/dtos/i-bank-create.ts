import { ApiBankCreate } from './api/api-bank-create';

export interface IBankCreate {
    name: string;
    eroeffnetAm: Date;
    isPleite: boolean;
}

export abstract class BankCreate {
    public static toApiBankCreate(iBankCreate: IBankCreate): ApiBankCreate {
        return {
            name: iBankCreate.name,
            eroeffnetAm: iBankCreate.eroeffnetAm,
            isPleite: iBankCreate.isPleite,
        };
    }
}
