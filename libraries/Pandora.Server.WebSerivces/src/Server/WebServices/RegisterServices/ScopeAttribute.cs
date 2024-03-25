using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pandora.Server.WebServices
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ScopeAttribute : ServiceAttribute
    {
        public ScopeAttribute() : base(null, ServiceLifetime.Scoped)
        { }

        public ScopeAttribute(Type type) : base(type, ServiceLifetime.Scoped)
        { }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ScopeAttribute<TInterface> : ScopeAttribute
    {
        public ScopeAttribute() : base(typeof(TInterface))
        { }
    }
}
