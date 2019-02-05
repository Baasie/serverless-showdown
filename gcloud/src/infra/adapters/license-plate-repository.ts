const {Datastore} = require('google-cloud__datastore');
import {LicensePlateRegistration, LicensePlateType} from "../../domain/license-plate-registration";

const datastore = new Datastore();

async function resolveTableName(): Promise<string> {
    if (!process.env.LICENSE_PLATE_TABLE_NAME) {
        return Promise.reject('env property LICENSE_PLATE_TABLE_NAME not found.');
    }
    return process.env.LICENSE_PLATE_TABLE_NAME.toString();
}

export class LicensePlateRepository {

    public static async findLicensePlate(license: string): Promise<LicensePlateRegistration> {
        const query = datastore.createQuery('License').filter("license", "=", license);

        const [licensePlateRegistrations] = await datastore.runQuery(query);
        if (licensePlateRegistrations.length > 0) {
            return Promise.resolve(LicensePlateRegistration.fromJSON(licensePlateRegistrations.pop() as any));
        }
        return Promise.resolve(new LicensePlateRegistration(license, LicensePlateType.UNKNOWN));

    }

}