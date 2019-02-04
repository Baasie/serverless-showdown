using System;
using Microsoft.Azure.WebJobs.Description;

namespace ServerlessParking.Application._DependencyInjection
{
    /// <summary>
    /// Attribute used to inject a dependency into the function completes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    [Binding]
    public sealed class InjectAttribute : Attribute
    {
    }
}
