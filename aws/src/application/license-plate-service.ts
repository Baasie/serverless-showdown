import {LicensePlateRepository} from "../adapter/repository/license-plate-repository";
import {LicensePlateRegistration} from "../domain/license-plate-registration";

export class LicensePlateService {

    public static async findMatchingLicensePlate(license: string): Promise<LicensePlateRegistration> {
        console.log("findMatchingLicensePlate");
        return LicensePlateRepository.findLicensePlate(license);
    }
}