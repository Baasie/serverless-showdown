import {APIGatewayEvent, Callback, Context} from "aws-lambda";
import {AppointmentService} from "./appointment-service";

export function handle(event, context: Context, callback: Callback) {

    AppointmentService.occupyParkingSpace(JSON.parse(event.body as string).parkingGarageId).then(
        parkingGarage => {
            return callback(null, true);
        },
        error => {
            return callback(null, false);
        }
    )

}