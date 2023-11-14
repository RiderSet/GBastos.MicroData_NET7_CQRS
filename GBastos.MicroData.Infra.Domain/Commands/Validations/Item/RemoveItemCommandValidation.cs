using GBastos.MicroData.Domain.Commands.ItemCommands;

namespace GBastos.MicroData.Domain.Commands.Validations.Item
{
    public class RemoveItemCommandValidation : ItemValidation<RemoveItemCommand>
    {
        public RemoveItemCommandValidation()
        {
            ValidateId();
        }
    }
}