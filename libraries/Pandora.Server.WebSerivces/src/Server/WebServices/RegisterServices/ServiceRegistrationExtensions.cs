using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Pandora.Server.WebServices
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AutoregisterServices(this IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AutoregisterServices(services, assembly);
            }

            return services;
        }

        public static IServiceCollection AutoregisterServices(this IServiceCollection services, Assembly assembly)
        {
            var types = from type in assembly.GetTypes()
                        where type.IsClass && type.IsPublic
                        let attribute = type.GetCustomAttributes().OfType<ServiceAttribute>().FirstOrDefault()
                        where attribute != null
                        select new { ServiceType = type, ServiceInterface = attribute.ServiceInterface, Lifetime = attribute.ServiceLifetime };

            foreach (var type in types)
            {
                var serviceDescriptor = new ServiceDescriptor(type.ServiceInterface, type.ServiceType, type.Lifetime);
                services.Add(serviceDescriptor);
            }

            return services;
        }
    }
}
