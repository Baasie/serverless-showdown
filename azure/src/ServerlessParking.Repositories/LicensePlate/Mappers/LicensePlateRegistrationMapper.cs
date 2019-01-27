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
                Number = entity.Number,
                Type = Enum.TryParse(entity.Type, out LicensePlateType type) ? type : LicensePlateType.Unknown
            };
        }

        public static LicensePlateRegistrationEntity ToLicensePlateRegistrationEntity(LicensePlateRegistration registration)
        {
            return new LicensePlateRegistrationEntity
            {
                ContactPerson = registration.ContactPerson,
                Name = registration.Name,
                Number = registration.Number,
                Type = registration.Type.ToString("G"),
                PartitionKey = registration.Type.ToString("G"),
                RowKey = registration.Number
            };
        }
    }
}
