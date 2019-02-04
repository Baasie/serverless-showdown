using System.Threading.Tasks;
using ServerlessParking.Domain;
using ServerlessParking.Repositories.LicensePlate;

namespace ServerlessParking.Services.LicensePlate
{
    public class LicensePlateRegistrationService : ILicensePlateRegistrationService
    {
        private readonly ILicensePlateRegistrationRepository _repository;

        public LicensePlateRegistrationService(
            ILicensePlateRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LicensePlateRegistration> GetRegistrationForAppointmentAsync(string licensePlateNumber)
        {
            var licensePlate = await _repository.GetByTypeAndNumberAsync(
                LicensePlateType.Appointment.ToString("G"), 
                licensePlateNumber);

            return licensePlate;
        }

        public async Task<LicensePlateRegistration> GetRegistrationForEmployeeAsync(string licensePlateNumber)
        {
            var licensePlate = await _repository.GetByTypeAndNumberAsync(
                LicensePlateType.Employee.ToString("G"),
                licensePlateNumber);

            return licensePlate;
        }
    }
}
