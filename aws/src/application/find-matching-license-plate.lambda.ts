import {APIGatewayEvent, Callback, Context} from "aws-lambda";
import {LicensePlateService} from "./license-plate-service";

export function handle(event, context: Context, callback: Callback) {
    let license: string = event.license;
    console.log("find matching license plate for: "+license);
    LicensePlateService.findMatchingLicensePlate(license).then(
        licensePlate => {
            console.log("Matched license plate: "+licensePlate.toString());
            return callback(null, licensePlate.toJSON());
        },
        error => {
            console.log("error occurred: "+error);
            return callback(null, "No Licenseplate Found");
        }
    )

}