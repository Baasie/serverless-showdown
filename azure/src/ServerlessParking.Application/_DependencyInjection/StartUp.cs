using System;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServerlessParking.Application._DependencyInjection;
using ServerlessParking.Application._DependencyInjection.Config;
using ServerlessParking.Repositories.LicensePlate;
using ServerlessParking.Repositories.Parking;
using ServerlessParking.Services.LicensePlate;
using ServerlessParking.Services.ParkingConfirmation;

[assembly: WebJobsStartup(typeof(Startup))]
namespace ServerlessParking.Application._DependencyInjection
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder) =>
            builder.AddDependencyInjection<ServiceProviderBuilder>();
    }

    internal class ServiceProviderBuilder : IServiceProviderBuilder
    {
        private readonly ILoggerFactory _loggerFactory;

        public ServiceProviderBuilder(ILoggerFactory loggerFactory) =>
            _loggerFactory = loggerFactory;

        public IServiceProvider Build()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILicensePlateRegistrationRepository>(
                _ => new LicensePlateRegistrationRepository(GetCloudTableClient()));
            services.AddSingleton<ILicensePlateRegistrationService, LicensePlateRegistrationService>();
            services.AddSingleton<IParkingGarageRepository>(
                _ => new ParkingGarageRepository(GetDocumentClient()));
            services.AddSingleton<IParkingConfirmationService, ParkingConfirmationService>();
            // Important: We need to call CreateFunctionUserCategory, otherwise our log entries might be filtered out.
            services.AddSingleton<ILogger>(_ => _loggerFactory.CreateLogger(LogCategories.CreateFunctionUserCategory("Common")));

            return services.BuildServiceProvider();
        }

        private static DocumentClient GetDocumentClient()
        {
            var documentClient = new DocumentClient(
                new Uri(Environment.GetEnvironmentVariable("CosmosDB:Uri")),
                Environment.GetEnvironmentVariable("CosmosDB:Key"));
            return documentClient;
        }

        private static CloudTableClient GetCloudTableClient()
        {
            var credentials = new StorageCredentials(
                Environment.GetEnvironmentVariable("TableStorage:AccountName"),
                Environment.GetEnvironmentVariable("TableStorage:Key"));
            var storageUri = new StorageUri(new Uri(Environment.GetEnvironmentVariable("TableStorage:Uri")));
            var cloudTableClient = new CloudTableClient(storageUri, credentials);

            return cloudTableClient;
        }
    }
}
