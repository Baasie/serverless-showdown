using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using ServerlessParking.Application._DependencyInjection.Services;

namespace ServerlessParking.Application._DependencyInjection
{

    internal class InjectBindingProvider : IBindingProvider
    {
        private readonly ServiceProviderHolder _serviceProviderHolder;

        public InjectBindingProvider(ServiceProviderHolder serviceProviderHolder) =>
            _serviceProviderHolder = serviceProviderHolder;

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IBinding binding = new InjectBinding(_serviceProviderHolder, context.Parameter.ParameterType);
            return Task.FromResult(binding);
        }
    }

}
