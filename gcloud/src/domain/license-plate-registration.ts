import {Serialised, TinyType} from "tiny-types";

export class LicensePlateRegistration extends TinyType {
    public static fromJSON = (o: Serialised<LicensePlateRegistration>) => new LicensePlateRegistration(
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