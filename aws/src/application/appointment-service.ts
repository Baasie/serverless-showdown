import {ParkingGarage, UUID} from "../domain/parking-garage";
import {ParkingGarageRepository} from "../adapter/repository/parking-garage-repository";

export class AppointmentService {
    public static async occupyParkingSpace(parkingSpaceId: UUID): Promise<ParkingGarage> {
        let parkingGarage = await ParkingGarageRepository.findParkingGarage(parkingSpaceId.id);
        parkingGarage.occupyParkingSpace();
        // TODO we have not allocated versioning yet, not needed for example for now
        return await ParkingGarageRepository.saveParkingGarage(parkingGarage);
    }
}