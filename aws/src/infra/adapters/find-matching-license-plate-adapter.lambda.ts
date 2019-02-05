import {Callback, Context} from "aws-lambda";
import {LicensePlateService} from "../../domain/LicensePlateService/license-plate-service";
import {LicensePlateType} from "../../domain/LicensePlateService/license-plate-registration";
import {AppointmentLicensePlateMatched, EmployeeLicensePlateMatched, NoLicensePlateMatched} from "../../domain/LicensePlateService/events";

export function handle(event, context: Context, callback: Callback) {
    let license: string = event.license;
    console.log("find matching license plate for: "+license);
    LicensePlateService.findMatchingLicensePlate(license).then(
        licensePlate => {
            switch(licensePlate.type) {
                case LicensePlateType.APPOINTMENT: {
                    return callback(null, new AppointmentLicensePlateMatched(license).toJSON());
                }
                case LicensePlateType.EMPLOYEE: {
                    return callback(null, new EmployeeLicensePlateMatched(license).toJSON());
                }
                default: {
                    return callback(null, new NoLicensePlateMatched(license).toJSON());
                }
            }
        },
        error => {
            console.log("error occurred: "+error);
            return callback(null, "No Licenseplate Found");
        }
    )

}