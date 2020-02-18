using System;
using System.Linq.Expressions;
using Columbus.InnerSource.Core.Domain;
using Columbus.InnerSource.Core.Domain.Repositories;
using Microsoft.Xrm.Sdk.Query;

namespace Columbus.InnerSource.Core.Crm.Domain.Repositories
{
    /// <summary>
    /// Расширенный интерфейс репозитория CRM
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IDynamicsCrmRepository<TEntity> : ICrmRepository<TEntity, Guid>
        where TEntity : IEntity<Guid>
    {
        /// <summary>
        /// Получить сущность по ее идентфикатору с указанием набора столбцов
        /// </summary>
        /// <param name="id">идентифкатор</param>
        /// <param name="columnSet">столбцы</param>
        /// <returns></returns>
        TEntity Get(Guid id, ColumnSet columnSet);
        IPagedListResult<TEntity> Find(QueryExpression query);
    }
}
