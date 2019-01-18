import {APIGatewayEvent, Callback, Context} from "aws-lambda";
import {EmployeeService} from "./employee-service";

export function handle(event: APIGatewayEvent, context: Context, callback: Callback) {

    EmployeeService.occupyParkingSpace(JSON.parse(event.body as string).parkingGarageId).then(
        parkingGarage => {
            return callback(null, true);
        },
        error => {
            return callback(null, false);
        }
    )

}