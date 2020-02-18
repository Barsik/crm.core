using Columbus.InnerSource.Core.Crm.Domain.Repositories;
using Columbus.InnerSource.Core.Domain;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using Columbus.InnerSource.Core.Crm.Domain;
using Columbus.InnerSource.Core.Domain.Repositories;

namespace Columbus.InnerSource.Infrastructure.Microsoft.Crm.Repositories
{
    public class DynamicsCrmRepository<TEntity> : IDynamicsCrmRepository<TEntity>
        where TEntity : AggregateRoot, new()
    {
        private readonly IOrganizationService _organizationService;

        public DynamicsCrmRepository(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public TEntity Get(Guid id) => Get(id, new ColumnSet(false));

        public TEntity Get(Guid id, ColumnSet columnSet)
            => _organizationService.Retrieve(new TEntity().LogicalName, id, columnSet ?? new ColumnSet(false))?.ToEntity<TEntity>();

        public IPagedListResult<TEntity> Find(QueryExpression query)
        {
            if (!new TEntity().LogicalName.Equals(query?.EntityName, StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidCastException($"Can not convert query to {nameof(TEntity)} query");

            var response = _organizationService.RetrieveMultiple(query);
            var items = response.Entities.Select(e => e.ToEntity<TEntity>()).ToList();
            return new PagedListResult<TEntity>(items, response.TotalRecordCount);
        }

        public IPagedListResult<TEntity> Find(ISpecification specification)
        {
            var crmSpecification = specification as CrmSpecification;
            var queryExpression = (crmSpecification ??
                                   throw new InvalidCastException($"Can't cast specification to {nameof(CrmSpecification)}")).Query;

            return Find(queryExpression);
        }

        public Guid Create(TEntity entity) => _organizationService.Create(entity);

        public void Update(TEntity entity) => _organizationService.Update(entity);

        public void Delete(Guid id) => _organizationService.Delete(new TEntity().LogicalName, id);
    }
}
