import { ApiKunde } from "src/app/model/kundenstamm/kunden/dtos/api/api-kunde";

export interface ApiBankListItem {
    id: string;
    name: string;
    eroeffnetAm: Date;
    isPleite: boolean;
    kunde: ApiKunde;
}
