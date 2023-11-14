using FluentValidation.Results;
using GBastos.MicroData.Domain.Events;
using GBastos.MicroData.Domain.Interfaces;
using MediatR;
using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Commands.PedidoCommands
{
    public class PedidoCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewPedidoCommand, ValidationResult>,
        IRequestHandler<UpdatePedidoCommand, ValidationResult>,
        IRequestHandler<RemovePedidoCommand, ValidationResult>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<ValidationResult> Handle(RegisterNewPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedido = new Pedido(Guid.NewGuid(), message.Name, message.Email, message.BirthDate);

            if (await _pedidoRepository.GetByEmail(pedido.Email) != null)
            {
                AddError("The pedido e-mail has already been taken.");
                return ValidationResult;
            }

            pedido.AddDomainEvent(new PedidoRegisteredEvent(pedido.Id, pedido.Name, pedido.Email, pedido.BirthDate));

            _pedidoRepository.Add(pedido);

            return await Commit(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdatePedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedido = new Pedido(message.Id, message.Name, message.Email, message.BirthDate);
            var existingPedido = await _pedidoRepository.GetByEmail(pedido.Email);

            if (existingPedido != null && existingPedido.Id != pedido.Id)
            {
                if (!existingPedido.Equals(pedido))
                {
                    AddError("The pedido e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            pedido.AddDomainEvent(new PedidoUpdatedEvent(pedido.Id, pedido.Name, pedido.Email, pedido.BirthDate));

            _pedidoRepository.Update(pedido);

            return await Commit(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemovePedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedido = await _pedidoRepository.GetById(message.Id);

            if (pedido is null)
            {
                AddError("The pedido doesn't exists.");
                return ValidationResult;
            }

            pedido.AddDomainEvent(new PedidoRemovedEvent(message.Id));

            _pedidoRepository.Remove(pedido);

            return await Commit(_pedidoRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _pedidoRepository.Dispose();
        }
    }
}