import {ParkingGarage, UUID} from "./parking-garage";
import {ParkingGarageRepositoryAdapter} from "../../infra/adapters/parking-garage-repository-adapter";

export class EmployeeService {

    public static async occupyParkingSpace(parkingSpaceId: UUID): Promise<ParkingGarage> {
        let parkingGarage = await ParkingGarageRepositoryAdapter.findParkingGarage(parkingSpaceId.id);
        parkingGarage.occupyParkingSpace();
        return await ParkingGarageRepositoryAdapter.saveParkingGarage(parkingGarage);
    }
}
