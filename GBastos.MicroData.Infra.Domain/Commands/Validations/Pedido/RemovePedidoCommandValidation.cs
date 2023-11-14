using GBastos.MicroData.Domain.Commands.PedidoCommands;

namespace GBastos.MicroData.Domain.Commands.Validations.Pedido
{
    public class RemovePedidoCommandValidation : PedidoValidation<RemovePedidoCommand>
    {
        public RemovePedidoCommandValidation()
        {
            ValidateId();
        }
    }
}