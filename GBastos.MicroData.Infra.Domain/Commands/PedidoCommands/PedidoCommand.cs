using System;
using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Commands.PedidoCommands
{
    public abstract class PedidoCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public DateTime BirthDate { get; protected set; }
    }
}