using FluentValidation.Results;
using GBastos.MicroData.Application.EventSourcedNormalizers;
using GBastos.MicroData.Application.ViewModels;

namespace GBastos.MicroData.Application.Interfaces
{
    public interface IItemAppService : IDisposable
    {
        Task<IEnumerable<ItemViewModel>> GetAll();
        Task<ItemViewModel> GetById(Guid id);
        
        Task<ValidationResult> Register(ItemViewModel itemViewModel);
        Task<ValidationResult> Update(ItemViewModel itemViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<ItemHistoryData>> GetAllHistory(Guid id);
    }
}
