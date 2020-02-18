namespace Columbus.InnerSource.Core.Domain
{
    /// <summary>
    /// Интерфейс доменной сущности
    /// </summary>
    /// <typeparam name="TKey">Тип ключа доменной сущности</typeparam>
    public interface IAggregateRoot<TKey> : IEntity<TKey>, IHasExtraProperties
    {
    }
}
