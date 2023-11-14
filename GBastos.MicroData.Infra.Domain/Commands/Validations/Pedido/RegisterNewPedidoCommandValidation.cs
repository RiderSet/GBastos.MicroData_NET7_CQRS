using GBastos.MicroData.Domain.Commands.PedidoCommands;

namespace GBastos.MicroData.Domain.Commands.Validations.Pedido
{
    public class RegisterNewPedidoCommandValidation : PedidoValidation<RegisterNewPedidoCommand>
    {
        public RegisterNewPedidoCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}