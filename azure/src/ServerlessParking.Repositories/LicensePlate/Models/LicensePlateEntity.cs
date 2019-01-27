using Microsoft.Azure.Cosmos.Table;

namespace ServerlessParking.Repositories.LicensePlate.Models
{
    public class LicensePlateRegistrationEntity : TableEntity
    {
        public string Number { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string ContactPerson { get; set; }
    }
}
