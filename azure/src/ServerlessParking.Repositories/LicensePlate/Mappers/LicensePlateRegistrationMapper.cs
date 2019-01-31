using ServerlessParking.Domain;
using ServerlessParking.Repositories.LicensePlate.Models;
using System;

namespace ServerlessParking.Repositories.LicensePlate.Mappers
{
    public static class LicensePlateRegistrationMapper
    {
        public static LicensePlateRegistration ToLicensePlateRegistration(LicensePlateRegistrationEntity entity)
        {
            return new LicensePlateRegistration
            {
                ContactPerson = entity.ContactPerson,
                Name = entity.Name,
                Number = entity.RowKey,
                Type = Enum.TryParse(entity.PartitionKey, out LicensePlateType type) ? type : LicensePlateType.Unknown
            };
        }

        public static LicensePlateRegistrationEntity ToLicensePlateRegistrationEntity(LicensePlateRegistration registration)
        {
            return new LicensePlateRegistrationEntity
            {
                ContactPerson = registration.ContactPerson,
                Name = registration.Name,
                PartitionKey = registration.Type.ToString("G"),
                RowKey = registration.Number
            };
        }
    }
}
