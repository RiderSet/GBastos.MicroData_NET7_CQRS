using FluentValidation.Results;
using GBastos.MicroData.Domain.Events.Item;
using GBastos.MicroData.Domain.Interfaces;
using GBastos.MicroData.Domain.Models;
using MediatR;
using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Commands.ItemCommands
{
    public class ItemCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewItemCommand, ValidationResult>,
        IRequestHandler<UpdateItemCommand, ValidationResult>,
        IRequestHandler<RemoveItemCommand, ValidationResult>
    {
        private readonly IItemRepository _itemRepository;

        public ItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ValidationResult> Handle(RegisterNewItemCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var item = new Item(Guid.NewGuid(), message.Name, message.Email, message.BirthDate);

            if (await _itemRepository.GetByEmail(item.Email) != null)
            {
                AddError("The Item e-mail has already been taken.");
                return ValidationResult;
            }

            item.AddDomainEvent(new ItemRegisteredEvent(item.Id, item.Name, item.Email, item.BirthDate));

            _itemRepository.Add(item);

            return await Commit(_itemRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateItemCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var item = new Item(message.Id, message.Name, message.Email, message.BirthDate);
            var existingItem = await _itemRepository.GetByEmail(item.Email);

            if (existingItem != null && existingItem.Id != item.Id)
            {
                if (!existingItem.Equals(item))
                {
                    AddError("The Item e-mail has already been taken.");
                    return ValidationResult;
                }
            }

            item.AddDomainEvent(new ItemUpdatedEvent(item.Id, item.Name, item.Email, item.BirthDate));

            _itemRepository.Update(item);

            return await Commit(_itemRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveItemCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var item = await _itemRepository.GetById(message.Id);

            if (item is null)
            {
                AddError("The Item doesn't exists.");
                return ValidationResult;
            }

            item.AddDomainEvent(new ItemRemovedEvent(message.Id));

            _itemRepository.Remove(item);

            return await Commit(_itemRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _itemRepository.Dispose();
        }
    }
}