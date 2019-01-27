using ServerlessParking.Domain;

namespace ServerlessParking.Application.LicensePlate.Models
{
    public class LicensePlateRegistrationRequest
    {

        public LicensePlateRegistrationRequest(
            LicensePlateType registrationType,
            string licensePlateNumber)
        {
            RegistrationType = registrationType;
            LicensePlateNumber = licensePlateNumber;
        }

        public LicensePlateType RegistrationType { get; set; }

        public string LicensePlateNumber { get; set; }
    }
}
