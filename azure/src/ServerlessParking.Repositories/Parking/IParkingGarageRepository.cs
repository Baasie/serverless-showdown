using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Repositories.Parking
{
    public interface IParkingGarageRepository
    {
        Task<ParkingGarage> GetByNameAsync(string name);

        Task AddAsync(ParkingGarage parkingGarage);

        Task UpdateAsync(ParkingGarage parkingGarage);

        Task RemoveAsync(string parkingGarageId);
    }
}
