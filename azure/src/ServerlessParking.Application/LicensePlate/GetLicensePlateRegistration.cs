using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServerlessParking.Application._DependencyInjection;
using ServerlessParking.Domain;
using ServerlessParking.Services.LicensePlate;


namespace ServerlessParking.Application.LicensePlate
{
    public static class GetLicensePlateRegistration
    {
        [FunctionName(nameof(GetLicensePlateRegistration))]
        public static async Task<LicensePlateRegistration> Run(
            [ActivityTrigger] string licensePlateNumber,
            [Inject] ILicensePlateRegistrationService licensePlateRegistrationService,
            ILogger logger)
        {
            logger.LogInformation($"Started {nameof(GetLicensePlateRegistration)} for number: {licensePlateNumber}.");

            var licensePlateResult = await licensePlateRegistrationService.GetRegistrationForAppointmentAsync(licensePlateNumber) ??
                                 await licensePlateRegistrationService.GetRegistrationForEmployeeAsync(licensePlateNumber);

            return licensePlateResult;
        }
    }
}
