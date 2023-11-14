using GBastos.MicroData.Domain.Commands.ItemCommands;

namespace GBastos.MicroData.Domain.Commands.Validations.Item
{
    public class RegisterNewItemCommandValidation : ItemValidation<RegisterNewItemCommand>
    {
        public RegisterNewItemCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}