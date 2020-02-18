namespace Columbus.InnerSource.Core.Domain.Repositories
{
    /// <summary>
    /// Стандартный интерфейс репозитория CRM-системы
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности репозитория</typeparam>
    /// <typeparam name="TKey">Тип первичного ключа для сущности системы</typeparam>
    public interface ICrmRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
    }
}
