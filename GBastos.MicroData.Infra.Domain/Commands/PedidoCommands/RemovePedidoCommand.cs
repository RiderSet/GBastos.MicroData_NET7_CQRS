using FluentValidation;
using GBastos.MicroData.Domain.Commands.Validations.Pedido;

namespace GBastos.MicroData.Domain.Commands.PedidoCommands
{
    public class RemovePedidoCommand : PedidoCommand
    {
        public RemovePedidoCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemovePedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}