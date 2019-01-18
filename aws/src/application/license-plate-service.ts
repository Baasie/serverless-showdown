import {LicensePlateRepository} from "../port.adapter/repository/license-plate-repository";
import {LicensePlate} from "../domain/license-plate";

export class LicensePlateService {

    public static async findMatchingLicensePlate(license: string): Promise<LicensePlate> {
        console.log("findMatchingLicensePlate");
        return LicensePlateRepository.findLicensePlate(license);
    }
}