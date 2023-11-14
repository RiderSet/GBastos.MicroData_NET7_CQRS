using System;
using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Events.Item
{
    public class ItemRemovedEvent : Event
    {
        public ItemRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}