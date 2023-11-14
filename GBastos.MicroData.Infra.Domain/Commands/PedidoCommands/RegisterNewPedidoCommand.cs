using FluentValidation;
using GBastos.MicroData.Domain.Commands.Validations.Pedido;

namespace GBastos.MicroData.Domain.Commands.PedidoCommands
{
    public class RegisterNewPedidoCommand : PedidoCommand
    {
        public RegisterNewPedidoCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewPedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}