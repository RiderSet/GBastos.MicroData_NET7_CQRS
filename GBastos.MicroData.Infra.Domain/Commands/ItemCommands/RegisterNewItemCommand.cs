using GBastos.MicroData.Domain.Commands.Validations.Item;

namespace GBastos.MicroData.Domain.Commands.ItemCommands
{
    public class RegisterNewItemCommand : ItemCommand
    {
        public RegisterNewItemCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}