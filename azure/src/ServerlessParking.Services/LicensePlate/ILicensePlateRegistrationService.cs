using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Services.LicensePlate
{
    public interface ILicensePlateRegistrationService
    {
        Task<LicensePlateRegistration> GetRegistrationForAppointmentAsync(string licensePlateNumber);

        Task<LicensePlateRegistration> GetRegistrationForEmployeeAsync(string licensePlateNumber);
    }
}
