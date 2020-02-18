using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;

namespace Columbus.InnerSource.Infrastructure.Microsoft.Crm.Extensions
{
    public static class EntityExtensions
    {
        public static string GetAttributeLogicalName<T, U>(Expression<Func<T, U>> lambda)
        {
            MemberExpression body = lambda.Body as MemberExpression;

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
    }
}
