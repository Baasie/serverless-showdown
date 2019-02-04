import {ensure, isDefined, Serialised, TinyType} from 'tiny-types';

export class LicensePlate extends TinyType {
    static fromJSON = (o: Serialised<LicensePlate>) => new LicensePlate(
        o.number as string
    );

    constructor(public readonly number: string) {
        super();
        ensure('Number', number, isDefined());
    }
}