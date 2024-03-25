using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pandora.Server.WebServices
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TransientAttribute : ServiceAttribute
    {
        public TransientAttribute() : base(null, ServiceLifetime.Transient)
        { }

        public TransientAttribute(Type type) : base(type, ServiceLifetime.Transient)
        { }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TransientAttribute<TInterface> : TransientAttribute
    {
        public TransientAttribute() : base(typeof(TInterface))
        { }
    }
}
