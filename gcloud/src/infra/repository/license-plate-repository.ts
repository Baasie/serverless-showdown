const {Datastore} = require('@google-cloud/datastore');
import {LicensePlateRegistration, LicensePlateType} from "../../domain/license-plate-registration";


async function resolveTableName(): Promise<string> {
    if (!process.env.LICENSE_PLATE_TABLE_NAME) {
        return Promise.reject('env property LICENSE_PLATE_TABLE_NAME not found.');
    }
    return process.env.LICENSE_PLATE_TABLE_NAME.toString();
}

export class LicensePlateRepository {

    public static async findLicensePlate(license: string): Promise<LicensePlateRegistration> {


    }

}