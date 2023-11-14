using GBastos.MicroData.Domain.Commands.Pedido;

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