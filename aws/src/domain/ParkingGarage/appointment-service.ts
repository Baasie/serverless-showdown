import {ParkingGarage, UUID} from "./parking-garage";
import {ParkingGarageRepositoryAdapter} from "../../infra/adapters/parking-garage-repository-adapter";

export class AppointmentService {
    public static async occupyParkingSpace(parkingSpaceId: UUID): Promise<ParkingGarage> {
        let parkingGarage = await ParkingGarageRepositoryAdapter.findParkingGarage(parkingSpaceId.id);
        parkingGarage.occupyParkingSpace();
        // TODO we have not allocated versioning yet, not needed for example for now
        return await ParkingGarageRepositoryAdapter.saveParkingGarage(parkingGarage);
    }
}