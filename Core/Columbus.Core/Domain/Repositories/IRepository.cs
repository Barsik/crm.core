namespace Columbus.InnerSource.Core.Domain.Repositories
{
    /// <summary>
    /// Общий интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип идентификатора</typeparam>
    public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        TKey Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}
