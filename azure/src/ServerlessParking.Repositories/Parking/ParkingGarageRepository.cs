using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using ServerlessParking.Domain;
using ServerlessParking.Repositories.Builders;

namespace ServerlessParking.Repositories.Parking
{
    public class ParkingGarageRepository : IParkingGarageRepository
    {
        public ParkingGarageRepository(DocumentClient documentClient)
        {
            _documentClient = documentClient;
            CreateDatabaseAndCollection().GetAwaiter().GetResult();
        }

        public async Task<ParkingGarage> GetByNameAsync(string name)
        {
            ParkingGarage result;

            try
            {
                var parkingGarage = _documentClient.CreateDocumentQuery<ParkingGarage>(GetDocumentCollectionUri())
                    .Where(parking => parking.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    .AsEnumerable()
                    .FirstOrDefault();

                result = await Task.FromResult(parkingGarage);
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForDocumentQuery(nameof(name), name, e);
            }

            return result;
        }

        public async Task AddAsync(ParkingGarage parkingGarage)
        {

            try
            {
                await _documentClient.CreateDocumentAsync(GetDocumentCollectionUri(), parkingGarage, GetRequestOptions());
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForDocumentCreation(parkingGarage, e);
            }
        }

        public async Task UpdateAsync(ParkingGarage parkingGarage)
        {
            var documentUri = UriFactory.CreateDocumentUri(DatabaseId, CollectionId, parkingGarage.Id);

            try
            {
                await _documentClient.UpsertDocumentAsync(documentUri, parkingGarage, GetRequestOptions());
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForDocumentUpdate(documentUri, e);
            }
        }

        public async Task RemoveAsync(string parkingGarageId)
        {
            var documentUri = UriFactory.CreateDocumentUri(DatabaseId, CollectionId, parkingGarageId);

            try
            {
                await _documentClient.DeleteDocumentAsync(documentUri);
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForDocumentDelete(documentUri, e);
            }
        }

        private async Task CreateDatabaseAndCollection()
        {
            var database = new Database
            {
                Id = DatabaseId
            };
            var databaseUri = GetDatabaseUri();

            try
            {
                await _documentClient.CreateDatabaseIfNotExistsAsync(database);

                var collection = new DocumentCollection
                {
                    Id = CollectionId
                };
                collection.PartitionKey.Paths.Add("/owner");
                collection.IndexingPolicy.Automatic = true;
                collection.IndexingPolicy.IndexingMode = IndexingMode.Consistent;
                collection.IndexingPolicy.IncludedPaths.Clear();

                await _documentClient.CreateDocumentCollectionIfNotExistsAsync(
                    databaseUri,
                    collection,
                    new RequestOptions { OfferThroughput = 400 });
            }
            catch (Exception e)
            {
                throw RepositoryExceptionBuilder.CreateExceptionForCollectionCreation(databaseUri, CollectionId, e);
            }

        }

        private static Uri GetDatabaseUri()
        {
            return UriFactory.CreateDatabaseUri(DatabaseId);
        }

        private static Uri GetDocumentCollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
        }

        private static RequestOptions GetRequestOptions()
        {
           return new RequestOptions { PartitionKey = new PartitionKey(PartitionKeyValue) };
        }

        public const string DatabaseId = "serverless-cdb";
        public const string CollectionId = "ParkingGarages";
        private readonly DocumentClient _documentClient;
        private const string PartitionKeyValue = "xebia";
    }
}
