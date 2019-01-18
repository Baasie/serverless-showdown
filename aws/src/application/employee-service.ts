import {ParkingGarage, UUID} from "../domain/parking-garage";
import {ParkingGarageRepository} from "../port.adapter/repository/parking-garage-repository";

export class EmployeeService {

    public static async occupyParkingSpace(parkingSpaceId: UUID): Promise<ParkingGarage> {
        let parkingGarage = await ParkingGarageRepository.findParkingGarage(parkingSpaceId.id);
        parkingGarage.occupyParkingSpace();
        return await ParkingGarageRepository.saveParkingGarage(parkingGarage);
    }
}
