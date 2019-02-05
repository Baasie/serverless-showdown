import {LicensePlateRepositoryAdapter} from "../../infra/adapters/license-plate-repository-adapter";
import {LicensePlateRegistration} from "./license-plate-registration";

export class LicensePlateService {

    public static async findMatchingLicensePlate(license: string): Promise<LicensePlateRegistration> {
        console.log("findMatchingLicensePlate");
        return LicensePlateRepositoryAdapter.findLicensePlate(license);
    }
}