using MediatR;

namespace GBastos.MicroData.Domain.Events
{
    public class PedidoEventHandler :
        INotificationHandler<PedidoRegisteredEvent>,
        INotificationHandler<PedidoUpdatedEvent>,
        INotificationHandler<PedidoRemovedEvent>
    {
        public Task Handle(PedidoUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(PedidoRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(PedidoRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}