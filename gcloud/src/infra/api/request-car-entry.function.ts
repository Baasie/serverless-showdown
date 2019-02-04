import {LicensePlate} from "./entities/license-plate";
import {Request, Response} from "express";

export function handle(request: Request, response: Response) {
    let licensePlate: LicensePlate;
    try {
        licensePlate = LicensePlate.fromJSON(request.body);
    } catch (error) {
        console.log("error: " + error);
        return response.status(400).send(error);
    }

    return response.send(licensePlate.number);
}