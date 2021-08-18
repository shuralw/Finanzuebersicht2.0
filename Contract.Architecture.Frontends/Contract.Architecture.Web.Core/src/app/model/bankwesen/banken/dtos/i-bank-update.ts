import { ApiBankUpdate } from './api/api-bank-update';
import { IBankDetail } from './i-bank-detail';

export interface IBankUpdate {
    id: string;
    name: string;
    eroeffnetAm: Date;
    isPleite: boolean;
}

export abstract class BankUpdate {
    public static toApiBankUpdate(iBankUpdate: IBankUpdate): ApiBankUpdate {
        return {
            id: iBankUpdate.id,
            name: iBankUpdate.name,
            eroeffnetAm: iBankUpdate.eroeffnetAm,
            isPleite: iBankUpdate.isPleite,
        };
    }

    public static fromBankDetail(iBankDetail: IBankDetail): IBankUpdate {
        return {
            id: iBankDetail.id,
            name: iBankDetail.name,
            eroeffnetAm: iBankDetail.eroeffnetAm,
            isPleite: iBankDetail.isPleite,
        };
    }
}
