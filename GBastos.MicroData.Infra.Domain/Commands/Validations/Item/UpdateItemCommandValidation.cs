using GBastos.MicroData.Domain.Commands.ItemCommands;

namespace GBastos.MicroData.Domain.Commands.Validations.Item
{
    public class UpdateItemCommandValidation : ItemValidation<UpdateItemCommand>
    {
        public UpdateItemCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}