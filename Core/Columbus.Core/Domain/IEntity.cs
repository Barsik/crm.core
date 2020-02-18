namespace Columbus.InnerSource.Core.Domain
{
    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        TKey Id { get; set; }
    }
}
