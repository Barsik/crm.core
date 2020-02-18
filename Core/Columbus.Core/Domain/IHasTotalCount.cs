namespace Columbus.InnerSource.Core.Domain
{
    /// <summary>
    /// Если предусмотрено поле, овечающее за общее количество элементов
    /// </summary>
    public interface IHasTotalCount
    {
        long TotalCount { get; }
    }
}
