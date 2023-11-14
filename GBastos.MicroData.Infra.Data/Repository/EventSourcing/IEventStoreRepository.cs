using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GBastos.MicroData.Domain.Core.Events;

namespace GBastos.MicroData.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}