using System;
using System.Collections.Generic;

namespace Columbus.InnerSource.Core.Configuration
{
    /// <summary>
    /// Интерфейс DI-контейнера
    /// </summary>
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type serviceType);

        IEnumerable<Type> GetRegisteredServices();

        string PrintRegisteredServices();

        bool HasRegistrationFor<T>()
            where T : class;
    }
}
