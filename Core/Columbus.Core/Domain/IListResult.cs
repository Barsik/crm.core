using System.Collections.Generic;

namespace Columbus.InnerSource.Core.Domain
{
    public interface IListResult<out T>
    {
        IReadOnlyList<T> Items { get; }
    }
}
