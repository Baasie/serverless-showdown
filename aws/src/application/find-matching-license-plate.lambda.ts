import {Callback, Context} from "aws-lambda";
import {LicensePlateService} from "./license-plate-service";
import {LicensePlateType} from "../domain/license-plate-registration";
import {AppointmentLicensePlateMatched, EmployeeLicensePlateMatched, NoLicensePlateMatched} from "../domain/events";

export function handle(event, context: Context, callback: Callback) {
    let license: string = event.license;
    console.log("find matching license plate for: "+license);
    LicensePlateService.findMatchingLicensePlate(license).then(
        licensePlate => {
            switch(licensePlate.type) {
                case LicensePlateType.APPOINTMENT: {
                    return callback(null, new AppointmentLicensePlateMatched().toJSON());
                }
                case LicensePlateType.EMPLOYEE: {
                    return callback(null, new EmployeeLicensePlateMatched().toJSON());
                }
                default: {
                    return callback(null, new NoLicensePlateMatched().toJSON());
                }
            }
        },
        error => {
            console.log("error occurred: "+error);
            return callback(null, "No Licenseplate Found");
        }
    )

}