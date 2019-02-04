using System.Threading.Tasks;
using ServerlessParking.Repositories.Parking;
using ServerlessParking.Services.ParkingConfirmation.Builders;
using ServerlessParking.Services.ParkingConfirmation.Models;

namespace ServerlessParking.Services.ParkingConfirmation
{
    public class ParkingConfirmationService : IParkingConfirmationService
    {
        private readonly IParkingGarageRepository _repository;

        public ParkingConfirmationService(
            IParkingGarageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConfirmParkingResponse> ConfirmParkingAsync(
            ConfirmParkingRequest request,
            bool hasReservation)
        {
            var parkingGarage = await _repository.GetByNameAsync(request.ParkingGarageName);
            var occupySpaceResult = parkingGarage.OccupyParkingSpace(hasReservation);

            return ConfirmParkingResponseBuilder.Build(parkingGarage.Name, occupySpaceResult);
        }

    }
}
