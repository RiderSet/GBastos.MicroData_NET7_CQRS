using AutoMapper;
using FluentValidation.Results;
using GBastos.MicroData.Application.EventSourcedNormalizers;
using GBastos.MicroData.Application.Interfaces;
using GBastos.MicroData.Application.ViewModels;
using GBastos.MicroData.Domain.Commands.Item;
using GBastos.MicroData.Domain.Interfaces;
using GBastos.MicroData.Infra.Data.Repository.EventSourcing;
using NetDevPack.Mediator;

namespace GBastos.MicroData.Application.Services
{
    public class ItemAppService : IItemAppService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public ItemAppService(IMapper mapper,
                                  IItemRepository itemRepository,
                                  IMediatorHandler mediator,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _mediator = mediator;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<IEnumerable<ItemViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ItemViewModel>>(await _itemRepository.GetAll());
        }

        public async Task<PedidoViewModel> GetById(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _itemRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(PedidoViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewItemCommand>(customerViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(ItemViewModel itemViewModel)
        {
            var updateCommand = _mapper.Map<UpdateItemCommand>(itemViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveItemCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IList<ItemHistoryData>> GetAllHistory(Guid id)
        {
            return ItemHistory.ToJavaScriptItemHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
