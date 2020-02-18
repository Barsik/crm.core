using System.Collections.Generic;

namespace Columbus.InnerSource.Core.Domain
{
    public class PagedListResult<T> : ListResult<T>, IPagedListResult<T>
    {
        public PagedListResult(IReadOnlyList<T> items, long totalCount) : base(items)
        {
            TotalCount = totalCount;
        }

        public long TotalCount { get; }
    }
}
