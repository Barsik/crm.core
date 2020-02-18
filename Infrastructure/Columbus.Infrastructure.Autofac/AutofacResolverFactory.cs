using Autofac;
using Autofac.Extensions.DependencyInjection;
using Columbus.InnerSource.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Columbus.InnerSource.Infrastructure.Autofac
{
    public class AutofacResolverFactory : IResolverFactory
    {
        private readonly ContainerBuilder _containerBuilder;

        public AutofacResolverFactory()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public void Register(IServiceCollection services)
        {
            _containerBuilder.Populate(services);
        }

        public IResolver CreateResolver()
        {
            var container = _containerBuilder.Build();
            return new AutofacResolver(container);
        }
    }
}
