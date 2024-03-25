using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pandora.Server.WebServices
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SingletonAttribute : ServiceAttribute
    {
        public SingletonAttribute() : base(null, ServiceLifetime.Singleton)
        { }

        public SingletonAttribute(Type type) : base(type, ServiceLifetime.Singleton)
        { }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SingletonAttribute<TInterface> : SingletonAttribute
    {
        public SingletonAttribute() : base(typeof(TInterface))
        { }
    }
}
