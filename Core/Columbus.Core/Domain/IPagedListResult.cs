namespace Columbus.InnerSource.Core.Domain
{
    public interface IPagedListResult<out T> : IListResult<T>, IHasTotalCount
    {

    }
}
