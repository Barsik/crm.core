using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Columbus.InnerSource.Core.Domain;
using Microsoft.Xrm.Sdk;

namespace Columbus.InnerSource.Core.Crm.Domain
{
    [DataContract(Name = "Entity", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts")]
    public abstract class AggregateRoot : Entity, IAggregateRoot<Guid>
    {
        protected KeyValuePair<string, object>[] DeltaAttributes;
        public Dictionary<string, object> ExtraProperties { get; private set; }

        protected AggregateRoot(string entityLogicalName) : base(entityLogicalName)
        {
            ExtraProperties = new Dictionary<string, object>();
        }

        protected T? GetOptionSetValue<T>(string attributeName) where T : struct, IComparable, IConvertible, IFormattable
        {
            var optionSet = GetAttributeValue<OptionSetValue>(attributeName);
            if (optionSet != null)
            {
                return (T)Enum.ToObject(typeof(T), optionSet.Value);
            }

            return null;
        }

        protected void SetOptionSetValue<T>(string attributeName, T value)
        {
            SetAttributeValue(attributeName, value != null ? new OptionSetValue((int)(object)value) : null);
        }

        protected decimal? GetMoneyValue(string attributeName)
        {
            var money = GetAttributeValue<Money>(attributeName);
            return money?.Value;
        }

        protected void SetMoneyValue(string attributeName, decimal? value)
        {
            SetAttributeValue(attributeName, value.HasValue ? new Money(value.Value) : null);
        }

        protected override IEnumerable<TEntity> GetRelatedEntities<TEntity>(string relationshipSchemaName, EntityRole? primaryEntityRole)
        {
            return base.GetRelatedEntities<TEntity>(relationshipSchemaName, primaryEntityRole);
        }

        protected IEnumerable<T> GetEntityCollection<T>(string attributeName) where T : Entity
        {
            var collection = GetAttributeValue<EntityCollection>(attributeName);
            return collection?.Entities?.Select(x => x.ToEntity<T>());
        }

        protected void SetEntityCollection<T>(string attributeName, IEnumerable<T> entities) where T : Entity
        {
            SetAttributeValue(attributeName,
                entities != null ? new EntityCollection(new List<Entity>(entities)) : null);
        }

        public void TagForDelta()
        {
            DeltaAttributes = new KeyValuePair<string, object>[Attributes.Count];
            Attributes.CopyTo(DeltaAttributes, 0);
        }

        public void PerformDelta()
        {
            if (DeltaAttributes == null) return;
            var guid = Id;

            foreach (var prev in DeltaAttributes)
            {
                if (Attributes.ContainsKey(prev.Key) && object.Equals(Attributes[prev.Key], prev.Value))
                {
                    Attributes.Remove(prev.Key);
                }
            }

            if (guid != Guid.Empty) Id = guid;
        }

        public void Merge<T>(T entity, bool overwrite = true) where T : Entity
        {
            var guid = Id;

            foreach (var attribute in entity.Attributes)
            {
                if (!overwrite && Attributes.ContainsKey(attribute.Key))
                {
                    continue;
                }
                Attributes[attribute.Key] = attribute.Value;
            }

            if (guid != Guid.Empty) Id = guid;
        }

        //TODO Create Entity diff method
    }
}
