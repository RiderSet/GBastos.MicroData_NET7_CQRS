using FluentValidation;
using GBastos.MicroData.Domain.Commands.Validations.Item;

namespace GBastos.MicroData.Domain.Commands.ItemCommands
{
    public class RemoveItemCommand : ItemCommand
    {
        public RemoveItemCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}