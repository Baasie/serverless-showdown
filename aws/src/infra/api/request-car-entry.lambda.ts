import {APIGatewayEvent, Callback, Context} from "aws-lambda";
import {LicensePlate} from "./entities/license-plate";
import {Response} from './support/response';

export function handle(event: APIGatewayEvent, context: Context, callback: Callback) {

    let licensePlate: LicensePlate;
    try {
        licensePlate = LicensePlate.fromJSON(JSON.parse(event.body as string));
    } catch (error) {
        console.log("error: "+error);
        return callback(null, Response.BAD_REQUEST(error));
    }

    return callback(null, Response.OK(licensePlate.number));
}