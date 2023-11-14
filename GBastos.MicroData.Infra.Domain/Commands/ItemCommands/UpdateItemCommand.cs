using GBastos.MicroData.Domain.Commands.Validations.Item;

namespace GBastos.MicroData.Domain.Commands.ItemCommands
{
    public class UpdateItemCommand : ItemCommand
    {
        public UpdateItemCommand(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}