﻿using GBastos.MicroData.Domain.Commands.Pedido;

namespace GBastos.MicroData.Domain.Commands.Validations.Pedido
{
    public class UpdatePedidoCommandValidation : PedidoValidation<UpdatePedidoCommand>
    {
        public UpdatePedidoCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}