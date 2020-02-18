using Microsoft.Extensions.DependencyInjection;

namespace Columbus.InnerSource.Core.Configuration
{
    /// <summary>
    /// Фабрика для DI-контейнера <seealso cref="IResolver"/>
    /// </summary>
    public interface IResolverFactory
    {
        void Register(IServiceCollection services);
        IResolver CreateResolver();
    }
}
