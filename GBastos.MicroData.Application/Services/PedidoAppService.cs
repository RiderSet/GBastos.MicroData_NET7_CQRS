using AutoMapper;
using FluentValidation.Results;
using GBastos.MicroData.Application.EventSourcedNormalizers;
using GBastos.MicroData.Application.ViewModels;
using GBastos.MicroData.Domain.Interfaces;
using GBastos.MicroData.Infra.Data.Repository.EventSourcing;
using NetDevPack.Mediator;

namespace GBastos.MicroData.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _pedidoRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public PedidoAppService(IMapper mapper,
                                  IPedidoRepository pedidoRepository,
                                  IMediatorHandler mediator,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _mediator = mediator;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<IEnumerable<PedidoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.GetAll());
        }

        public async Task<PedidoViewModel> GetById(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(PedidoViewModel pedidoViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewPedidoCommand>(pedidoViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(PedidoViewModel pedidoViewModel)
        {
            var updateCommand = _mapper.Map<UpdatePedidoCommand>(pedidoViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemovePedidoCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IList<PedidoHistoryData>> GetAllHistory(Guid id)
        {
            return PedidoHistory.ToJavaScriptPedidoHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
