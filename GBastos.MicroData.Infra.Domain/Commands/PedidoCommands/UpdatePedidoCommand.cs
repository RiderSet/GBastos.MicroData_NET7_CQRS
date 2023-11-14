using FluentValidation;
using GBastos.MicroData.Domain.Commands.Validations.Pedido;

namespace GBastos.MicroData.Domain.Commands.PedidoCommands
{
    public class UpdatePedidoCommand : PedidoCommand
    {
        public UpdatePedidoCommand(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdatePedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}