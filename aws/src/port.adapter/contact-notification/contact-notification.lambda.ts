import {APIGatewayEvent, Callback, Context} from "aws-lambda";

export function handle(event: APIGatewayEvent, context: Context, callback: Callback) {


    return callback(null, "done");
}