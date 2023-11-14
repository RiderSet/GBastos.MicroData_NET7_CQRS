using FluentValidation.Results;
using GBastos.MicroData.Application.EventSourcedNormalizers;
using GBastos.MicroData.Application.ViewModels;

namespace GBastos.MicroData.Application.Interfaces
{
    public interface IPedidoAppService : IDisposable
    {
        Task<IEnumerable<PedidoViewModel>> GetAll();
        Task<PedidoViewModel> GetById(Guid id);
        
        Task<ValidationResult> Register(PedidoViewModel PedidoViewModel);
        Task<ValidationResult> Update(PedidoViewModel PedidoViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<PedidoHistoryData>> GetAllHistory(Guid id);
    }
}
