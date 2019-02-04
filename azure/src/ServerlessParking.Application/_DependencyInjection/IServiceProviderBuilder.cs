using System;

namespace ServerlessParking.Application._DependencyInjection
{
    public interface IServiceProviderBuilder
    {
        /// <summary>
        /// Creates an instance of an <see cref="IServiceProvider"/>.
        /// </summary>
        /// <returns></returns>
        IServiceProvider Build();
    }
}
