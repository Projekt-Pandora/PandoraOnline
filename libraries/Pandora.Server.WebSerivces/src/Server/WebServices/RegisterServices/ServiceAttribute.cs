using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pandora.Server.WebServices
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public abstract class ServiceAttribute : Attribute
    {
        protected ServiceAttribute(Type serviceInterface, ServiceLifetime serviceLifetime)
        {
            ServiceInterface = serviceInterface;
            ServiceLifetime = serviceLifetime;
        }

        public Type ServiceInterface { get; }

        public ServiceLifetime ServiceLifetime { get; }
    }
}
