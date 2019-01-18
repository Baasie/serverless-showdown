import {Serialised, TinyType} from "tiny-types";

export class LicensePlate extends TinyType {
    public static fromJSON = (o: Serialised<LicensePlate>) => new LicensePlate(
        o.license as string,
        LicensePlateType[o.type as string]
    );

    constructor(public readonly license: string, public readonly type: LicensePlateType) {
        super();
    }
}

export enum LicensePlateType {
    EMPLOYEE,
    APPOINTMENT,
    UNKNOWN
}