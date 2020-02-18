using System.Collections.Generic;

namespace Columbus.InnerSource.Core.Domain
{
    public class ListResult<T> : IListResult<T>
    {
        public IReadOnlyList<T> Items { get; }

        public ListResult(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
