using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Application.LicensePlate.Models;
using ServerlessParking.Application._DependencyInjection;
using ServerlessParking.Domain;
using ServerlessParking.Services.LicensePlate;


namespace ServerlessParking.Application.LicensePlate
{
    public static class GetLicensePlateRegistration
    {
        [FunctionName(nameof(GetLicensePlateRegistration))]
        public static async Task<LicensePlateRegistration> Run(
            [ActivityTrigger] LicensePlateRegistrationRequest request,
            [Inject] ILicensePlateRegistrationService licensePlateRegistrationService,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(GetLicensePlateRegistration)} for number: {request.LicensePlateNumber}.");

            LicensePlateRegistration licensePlateResult = null;

            if (request.RegistrationType == LicensePlateType.Appointment)
            {
                licensePlateResult = await licensePlateRegistrationService
                        .GetRegistrationForAppointmentAsync(request.LicensePlateNumber);
            }
            else if (request.RegistrationType == LicensePlateType.Employee)
            {
                licensePlateResult = await licensePlateRegistrationService
                    .GetRegistrationForEmployeeAsync(request.LicensePlateNumber);
            }

            return licensePlateResult;
        }
    }
}
