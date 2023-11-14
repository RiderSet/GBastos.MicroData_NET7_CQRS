using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GBastos.MicroData.Domain.Events.Item
{
    public class ItemEventHandler :
        INotificationHandler<ItemRegisteredEvent>,
        INotificationHandler<ItemUpdatedEvent>,
        INotificationHandler<ItemRemovedEvent>
    {
        public Task Handle(ItemUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(ItemRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(ItemRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}