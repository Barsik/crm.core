using System;
using System.Linq.Expressions;
using Columbus.InnerSource.Core.Domain;
using Columbus.InnerSource.Core.Domain.Repositories;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Columbus.InnerSource.Core.Crm.Domain.Repositories
{
    public interface IOrganizationCrmRepository
    {
        AggregateRoot Get(Guid id, string entityName, ColumnSet columnSet = default);
        TEntity Get<TEntity>(Guid id, ColumnSet columnSet=default) where TEntity : AggregateRoot;
        TEntity Get<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] attributes) where TEntity : AggregateRoot;
        TEntity Retrieve<TEntity>(Guid id, ColumnSet columnSet = default, params (string name, QueryBase value)[] relationships) where TEntity : AggregateRoot;
        TEntity GetWithAllColumns<TEntity>(Guid id) where TEntity : AggregateRoot;
        IPagedListResult<TEntity> Find<TEntity>(QueryExpression query) where TEntity : AggregateRoot;
        IPagedListResult<TEntity> FindAll<TEntity>(QueryExpression query) where TEntity : AggregateRoot;
        IPagedListResult<TEntity> FindAll<TEntity>(params Expression<Func<TEntity, object>>[] attributes) where TEntity : AggregateRoot;
        IPagedListResult<TEntity> FindAll<TEntity>(ColumnSet columnSet) where TEntity : AggregateRoot;
        TEntity First<TEntity>(QueryExpression query) where TEntity : AggregateRoot;
        TEntity First<TEntity>(params Expression<Func<TEntity, object>>[] attributes) where TEntity : AggregateRoot;
        TEntity First<TEntity>(ColumnSet columnSet) where TEntity : AggregateRoot;
        Guid Create<TEntity>(TEntity entity) where TEntity : AggregateRoot;
        void Update<TEntity>(TEntity entity) where TEntity : AggregateRoot;
        void Delete<TEntity>(Guid id) where TEntity : AggregateRoot;
    }
}
