import { Kunde, IKunde } from 'src/app/model/kundenstamm/kunden/dtos/i-kunde';
import { ApiBankDetail } from './api/api-bank-detail';

export interface IBankDetail {
    id: string;
    name: string;
    eroeffnetAm: Date;
    isPleite: boolean;
    kunden: IKunde[];
}

export abstract class BankDetail {
    public static fromApiBankDetail(apiBankDetail: ApiBankDetail): IBankDetail {
        return {
            id: apiBankDetail.id,
            name: apiBankDetail.name,
            eroeffnetAm: apiBankDetail.eroeffnetAm,
            isPleite: apiBankDetail.isPleite,
            kunden: apiBankDetail.kunden

                .map(apiKunde => Kunde.fromApiKunde(apiKunde)),
        };
    }
}
