using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using ServerlessParking.Domain;
using ServerlessParking.Repositories.Builders;
using ServerlessParking.Repositories.LicensePlate.Mappers;
using ServerlessParking.Repositories.LicensePlate.Models;

namespace ServerlessParking.Repositories.LicensePlate
{
    public class LicensePlateRegistrationRepository : ILicensePlateRegistrationRepository
    {
        public LicensePlateRegistrationRepository(CloudTableClient cloudTableClient)
        {
            _cloudTableClient = cloudTableClient;
        }

        public async Task<LicensePlateRegistration> GetByTypeAndNumberAsync(
            string registrationType, 
            string licensePlateNumber)
        {
            var table = GetCloudTable();
            LicensePlateRegistration result = null;

            var retrieveOperation = TableOperation.Retrieve<LicensePlateRegistrationEntity>(
                registrationType, 
                licensePlateNumber);

            try
            {
                var retrievedResult = await table.ExecuteAsync(retrieveOperation);
                if (retrievedResult.Result != null)
                {
                    result = LicensePlateRegistrationMapper.ToLicensePlateRegistration(
                        (LicensePlateRegistrationEntity)retrievedResult.Result);
                }
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForTableOperation(
                    registrationType, licensePlateNumber, table, retrieveOperation, e);
            }

            return result;
        }

        public Task AddAsync(LicensePlateRegistration licensePlateRegistration)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(LicensePlateRegistration licenplate)
        {
            throw new NotImplementedException();
        }

        private CloudTable GetCloudTable()
        {
            var cloudTable = _cloudTableClient.GetTableReference(TableName);

            try
            {
                cloudTable.CreateIfNotExists();
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForTableCreation(cloudTable, e);
            }

            return cloudTable;
        }

        private readonly CloudTableClient _cloudTableClient;
        public const string TableName = "LicensePlateRegistrations";
        private const string PartitionKey = "PartitionKey";
    }
}
