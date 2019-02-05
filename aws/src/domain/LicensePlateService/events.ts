import {TinyType} from "tiny-types";


export class NoLicensePlateMatched extends TinyType {

    type: string = 'NoLicensePlateMatched';

    constructor(public readonly number: string) {
        super();
    }
}

export class AppointmentLicensePlateMatched extends TinyType {

    type: string = 'AppointmentLicensePlateMatched';

    constructor(public readonly number: string) {
        super();
    }
}

export class EmployeeLicensePlateMatched extends TinyType {

    type: string = 'EmployeeLicensePlateMatched';

    constructor(public readonly number: string) {
        super();
    }
}