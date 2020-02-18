using Columbus.InnerSource.Core.Domain;
using Microsoft.Xrm.Sdk.Query;

namespace Columbus.InnerSource.Core.Crm.Domain
{
    public class CrmSpecification : ISpecification
    {
        public QueryExpression Query { get; }
        public CrmSpecification(QueryExpression query)
        {
            Query = query;
        }
    }
}
