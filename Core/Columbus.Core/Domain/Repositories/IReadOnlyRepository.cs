namespace Columbus.InnerSource.Core.Domain.Repositories
{
    /// <summary>
    /// Репозиторий только на чтение, т.е. который только возвращает данные
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Получить сущность по ее идентфикатору
        /// </summary>
        /// <param name="id">Идентфифкатор</param>
        /// <returns></returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Найти записи по спецификации
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <returns>Результат с поддержкой постраничного представления</returns>
        IPagedListResult<TEntity> Find(ISpecification specification);
    }
}
