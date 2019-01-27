using Microsoft.Azure.WebJobs.Host.Config;

namespace ServerlessParking.Application._DependencyInjection.Config
{
    internal class InjectConfiguration : IExtensionConfigProvider
    {
        public readonly InjectBindingProvider InjectBindingProvider;

        public InjectConfiguration(InjectBindingProvider injectBindingProvider) =>
            InjectBindingProvider = injectBindingProvider;

        public void Initialize(ExtensionConfigContext context) => context
            .AddBindingRule<InjectAttribute>()
            .Bind(InjectBindingProvider);
    }
}
