using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Columbus.InnerSource.Core.Crm.Domain;
using Columbus.InnerSource.Core.Crm.Domain.Repositories;
using Columbus.InnerSource.Core.Domain;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace Columbus.InnerSource.Infrastructure.Microsoft.Crm.Repositories
{
    public class OrganizationCrmRepository : IOrganizationCrmRepository
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationCrmRepository(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public Guid Create<TEntity>(TEntity entity) where TEntity : AggregateRoot => _organizationService.Create(entity);

        public void Update<TEntity>(TEntity entity) where TEntity : AggregateRoot => _organizationService.Update(entity);

        public void Delete<TEntity>(Guid id) where TEntity : AggregateRoot => _organizationService.Delete(Activator.CreateInstance<TEntity>().LogicalName, id);

        public AggregateRoot Get(Guid id, string entityName, ColumnSet columnSet = default)
        {
            return _organizationService.Retrieve(entityName, id, columnSet).ToEntity<AggregateRoot>();
        }

        public TEntity Get<TEntity>(Guid id, ColumnSet columnSet = default) where TEntity : AggregateRoot
        {
            return _organizationService.Retrieve(Activator.CreateInstance<TEntity>().LogicalName, id, columnSet).ToEntity<TEntity>();
        }

        public TEntity Get<TEntity>(Guid id, params Expression<Func<TEntity, object>>[] attributes) where TEntity : AggregateRoot
        {
            return Get<TEntity>(id, GetColumnSet(attributes));
        }

        public TEntity Retrieve<TEntity>(Guid id, ColumnSet columnSet = default,
            params (string name, QueryBase value)[] relationships) where TEntity : AggregateRoot
        {
            var request = new RetrieveRequest
            {
                ColumnSet = columnSet ?? new ColumnSet(),
                Target = new EntityReference(Activator.CreateInstance<TEntity>().LogicalName, id)
            };

            if (relationships != null)
            {
                var relatedEntity = new RelationshipQueryCollection();
                relatedEntity.AddRange(relationships.ToDictionary(
                    k => new Relationship(k.name), 
                    v => v.value));
                request.RelatedEntitiesQuery = relatedEntity;
            }

            var response = _organizationService.Execute(request) as RetrieveResponse;

            return response?.Entity.ToEntity<TEntity>();
        }

        public TEntity GetWithAllColumns<TEntity>(Guid id) where TEntity : AggregateRoot
        {
            return Get<TEntity>(id, new ColumnSet(true));
        }

        public IPagedListResult<TEntity> Find<TEntity>(QueryExpression query) where TEntity : AggregateRoot
        {
            var response = _organizationService.RetrieveMultiple(query);
            var items = response.Entities.Select(e => e.ToEntity<TEntity>()).ToList();
            return new PagedListResult<TEntity>(items, response.TotalRecordCount);
        }

        public IPagedListResult<TEntity> FindAll<TEntity>(QueryExpression query) where TEntity : AggregateRoot
        {
            var result = new List<TEntity>();
            EntityCollection entityCollection;
            query.PageInfo = new PagingInfo { Count = 5000, PageNumber = 1 };
            do
            {
                entityCollection = _organizationService.RetrieveMultiple(query);
                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = entityCollection.PagingCookie;
                result.AddRange(entityCollection.Entities.Select(e => e.ToEntity<TEntity>()));
            }
            while (entityCollection.MoreRecords);

            return new PagedListResult<TEntity>(result, entityCollection.TotalRecordCount);
        }

        public IPagedListResult<TEntity> FindAll<TEntity>(params Expression<Func<TEntity, object>>[] attributes) where TEntity : AggregateRoot
        {
            return FindAll<TEntity>(GetColumnSet(attributes));
        }

        public IPagedListResult<TEntity> FindAll<TEntity>(ColumnSet columnSet) where TEntity : AggregateRoot
        {
            var query = new QueryExpression(Activator.CreateInstance<TEntity>().LogicalName)
            {
                ColumnSet = columnSet
            };
            return FindAll<TEntity>(query);
        }

        public TEntity First<TEntity>(QueryExpression query) where TEntity : AggregateRoot
        {
            query.PageInfo = new PagingInfo { Count = 1, PageNumber = 1 };
            var entityCollection = _organizationService.RetrieveMultiple(query);
            return entityCollection.Entities.Count == 1 ? entityCollection.Entities[0].ToEntity<TEntity>() : null;
        }
        public TEntity First<TEntity>(params Expression<Func<TEntity, object>>[] attributes) where TEntity : AggregateRoot
        {
            return First<TEntity>(GetColumnSet(attributes));
        }
        public TEntity First<TEntity>(ColumnSet columnSet) where TEntity : AggregateRoot
        {
            var query = new QueryExpression(Activator.CreateInstance<TEntity>().LogicalName)
            {
                ColumnSet = columnSet
            };
            return First<TEntity>(query);
        }

        private string GetAttributeLogicalName<T, U>(Expression<Func<T, U>> lambda)
        {
            var body = lambda.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)lambda.Body;
                body = ubody.Operand as MemberExpression;
            }

            var attributelogicalName = body.Member.GetCustomAttributes(false)
                .Where(x => x is AttributeLogicalNameAttribute)
                .FirstOrDefault() as AttributeLogicalNameAttribute;

            if (attributelogicalName == null)
                return body.Member.Name;
            return attributelogicalName.LogicalName;
        }

        private ColumnSet GetColumnSet<T>(params Expression<Func<T, object>>[] attributes)
        {
            if (attributes == null)
            {
                return new ColumnSet();
            }
            return new ColumnSet(attributes.Select(a => GetAttributeLogicalName(a).ToLower()).ToArray());
        }
    }
}
