using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Events
{
    public class PedidoRemovedEvent : Event
    {
        public PedidoRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}