using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Domain.Common
{
    public abstract class Entity
    {
        private readonly List<object> _domainEvents = new();

        public IReadOnlyCollection<object> DomainEvents => _domainEvents;

        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
