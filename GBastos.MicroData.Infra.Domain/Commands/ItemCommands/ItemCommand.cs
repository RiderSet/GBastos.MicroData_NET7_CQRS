using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Commands.ItemCommands
{
    public abstract class ItemCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public DateTime BirthDate { get; protected set; }
    }
}