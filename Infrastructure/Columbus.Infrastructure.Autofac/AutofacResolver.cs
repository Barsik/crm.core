using Autofac;
using Autofac.Core;
using Columbus.InnerSource.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Columbus.InnerSource.Infrastructure.Autofac
{
    internal class AutofacResolver : IResolver
    {
        private readonly IComponentContext _componentContext;

        public AutofacResolver(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        public T Resolve<T>()
        {
            return _componentContext.Resolve<T>();
        }

        public object Resolve(Type serviceType)
        {
            return _componentContext.Resolve(serviceType);
        }

        public IEnumerable<Type> GetRegisteredServices()
        {
            return _componentContext.ComponentRegistry.Registrations
                .SelectMany(x => x.Services)
                .OfType<TypedService>()
                .Where(x => !x.ServiceType.Name.StartsWith("Autofac"))
                .Select(x => x.ServiceType);
        }

        public string PrintRegisteredServices()
        {
            return string.Join(", ", this.GetRegisteredServices());
        }

        public bool HasRegistrationFor<T>()
            where T : class
        {
            var serviceType = typeof(T);
            return GetRegisteredServices().Any(t => serviceType == t);
        }
    }
}
