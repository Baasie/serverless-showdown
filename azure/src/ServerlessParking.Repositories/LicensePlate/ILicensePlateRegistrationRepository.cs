using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Repositories.LicensePlate
{
    public interface ILicensePlateRegistrationRepository
    {
        Task<LicensePlateRegistration> GetByTypeAndNumberAsync(string registrationType, string number);

        Task AddAsync(LicensePlateRegistration licensePlateRegistration);

        Task RemoveAsync(LicensePlateRegistration licenplate);
    }
}
